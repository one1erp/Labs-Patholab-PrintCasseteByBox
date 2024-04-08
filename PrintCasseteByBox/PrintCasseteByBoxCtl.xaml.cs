using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LSExtensionWindowLib;
using LSSERVICEPROVIDERLib;
using Patholab_Common;

using Patholab_DAL_V1;
using Patholab_XmlService;
using Binding = System.Windows.Data.Binding;
using DataGrid = System.Windows.Controls.DataGrid;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using ListBox = System.Windows.Controls.ListBox;
using MessageBox = System.Windows.MessageBox;
using MessageBoxOptions = System.Windows.MessageBoxOptions;


//using MessageBox = System.Windows.Controls.MessageBox;


namespace PrintCasseteByBox
{
    /// <summary>
    /// Interaction logic for CrystalReportsCtl.xaml
    /// </summary>
    public partial class PrintCasseteByBoxCtl : System.Windows.Controls.UserControl
    {
        public PrintCasseteByBoxCtl
            (INautilusServiceProvider sp, INautilusProcessXML xmlProcessor, INautilusDBConnection _ntlsCon,
             IExtensionWindowSite2 _ntlsSite, INautilusUser _ntlsUser)
        {
            InitializeComponent();


            this._ntlsCon = _ntlsCon;
            this._ntlsSite = _ntlsSite;

            this._sp = sp;

            this.DataContext = this;
        }

        private INautilusServiceProvider _sp;

        private INautilusDBConnection _ntlsCon;
        private IExtensionWindowSite2 _ntlsSite;

        private Timer _timerFocus;
        public bool DEBUG;
        private DataLayer dal;


        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            zLang.English();
        }
        
        List<string> printedContainers = new List<string>();
        private void txtContainer_keyDown(object sender, KeyEventArgs e)
        {
            txtmsg.Text = string.Empty;

            if (e.Key != Key.Enter) return;

            var input = txtContainer.Text.Trim();
            if (printedContainers.Contains(input))
            {
                txtContainer.Text = string.Empty;
                txtmsg.Text = "אין אפשרות להדפסה חוזרת לאותה ציידנית";
                txtContainer.Focus();
                return;
            }

            var exists = dal.FindBy<U_CONTAINER>(c => c.NAME == txtContainer.Text.Trim()).FirstOrDefault() != null;
            if (exists)
            {
                btnPrint.Focus();

            }
            else
            {
                txtContainer.Text = string.Empty;
                txtmsg.Text = "ציידנית לא קיימת";
                txtContainer.Focus();
            }
        }

        private void FirstFocus()
        {
            //First focus because nautius's bag
            _timerFocus = new Timer { Interval = 10000 };
            _timerFocus.Tick += timerFocus_Tick;
            txtContainer.Focus();
        }

        private void timerFocus_Tick(object sender, EventArgs e)
        {
            txtContainer.Focus();
            _timerFocus.Stop();

        }

        public void InitilaizeData()
        {
            dal = new DataLayer();
            dal.Connect(_ntlsCon);
            FirstFocus();
            setComboBoxPrinter();
        }

        private void setComboBoxPrinter()
        {
            try
            {
                PHRASE_HEADER header = dal.FindBy<PHRASE_HEADER>(ph => ph.NAME.Equals("Vega Printer", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (header != null)
                {
                    comboBoxPrinter.ItemsSource = header.PHRASE_ENTRY;
                    comboBoxPrinter.DisplayMemberPath = "PHRASE_NAME";
                }

                PHRASE_ENTRY entry = header.PHRASE_ENTRY.Where(pe => pe.PHRASE_INFO != null && pe.PHRASE_INFO.Equals("Print Cassette", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (entry != null) comboBoxPrinter.SelectedItem = entry;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on finding Vega Printer phrase." + Environment.NewLine + ex.Message);
            }

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            var dg = MessageBox.Show("?האם אתה בטוח שברצונך לצאת", Constants.MboxCaption, MessageBoxButton.YesNo,
                                     MessageBoxImage.Question);
            if (dg == MessageBoxResult.Yes)
            {

                _ntlsSite.CloseWindow();
            }
        }

        public void Close()
        {
            if (dal != null) dal.Close();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBoxPrinter.SelectedItem != null)
                {
                    var input = txtContainer.Text.Trim();

                    var uContainer = dal.FindBy<U_CONTAINER>(c => c.NAME == input).FirstOrDefault();
                    if (uContainer != null)
                    {
                        var container = uContainer.U_CONTAINER_ID;

                        var samples = dal.FindBy<SDG>(d => d.SDG_USER.U_CONTAINER_ID == container).Select(x => x.SAMPLEs);
                        var blocks =
                            samples.SelectMany(
                                s =>
                                s.SelectMany(
                                    x => x.ALIQUOTs.Where(al => al.STATUS != "X" && al.ALIQUOT_USER.U_GLASS_TYPE == "B")));

                        foreach (ALIQUOT aliquot in blocks)
                        {
                            string eventToFire = (comboBoxPrinter.SelectedItem as PHRASE_ENTRY).PHRASE_INFO;
                            FireEventXmlHandler fireEvent = new FireEventXmlHandler(_sp, "PrintCasseteByBox");
                            fireEvent.CreateFireEventXml("ALIQUOT", aliquot.ALIQUOT_ID, eventToFire);
                            bool s = fireEvent.ProcssXml();

                            if (!s)
                            {
                                Logger.WriteLogFile(fireEvent.ErrorResponse);


                                MessageBox.Show(string.Format("Aliquot ID: {0}{1}Can't print cassette more than once.", aliquot.ALIQUOT_ID, Environment.NewLine));
                            }
                        }
                        printedContainers.Add(uContainer.NAME);
                        txtContainer.Text = string.Empty;
                        txtContainer.Focus();
                    }
                    else
                    {
                        txtContainer.Text = string.Empty;
                        txtmsg.Text = "ציידנית לא קיימת";
                        txtContainer.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a printer.");
                }
            }

            catch (Exception exception)
            {
                Logger.WriteLogFile(exception);
                MessageBox.Show("Error");
            }
        }

        public class Constants
        {
            public static string MboxCaption = "הדפסת קסטות";
        }
    }
}
