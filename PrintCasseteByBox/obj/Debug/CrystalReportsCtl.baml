   M S B A M L   `   `   `  ����  CrystalReportsV1(    "CrystalReportsV1.CrystalReportsCtl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=2.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      
�������$��RightToLeft=�5   6     � 	Resources  5       �� 
TargetType �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader    DataGridContentCellCentering*  $System.Windows.Controls.DataGridCell �� 
TargetType��%A  '��       '�� �     '��     '  �    &	  U    �� 5   
   " ��6   .�5      ��  � ��$ 20(�6)   66   �� 5       � ��$
 Bold"�6+   6:   �� 5       � ��$	 111��6&   64   �� 5       � ��$ Center=�62   6C   �� 5       � ��$ 3q�6'   63   5      �� 5   
   " ��6   .�5      ��  � ��$ 20(�6)   66   �� 5       � ��$	 320��6)   67   �� 5       � ��$	 250��6*   68   �� 5       � ��$
 Bold"�6+   6:   �� 5       � ��$	 Top=�62   6@   �� 5       � ��$ 3q�6'   63   5      �� 5   
   " ��6   .�5      ��  � ��$ 16q�6'   64   �� 5       � ��$	 126��6)   67   �� 5        � ��$ 20(�6)   66   �� 5!       � ��$
 Bold"�6+   6:   5#      �� 5$   
   "  6   .�5%      ��  � ��$ Stretch=�64   �� 5&       � ��$ Center=�6;   5'      �� 5(   
   "  65   .�5)      ��  � �� 5*      �� 5+      "  6&   .R�5,      � #��z��6   .+�5-      �� $��Center=�6/   5.      5/      51      52      53      .��54      � $	��Top=�6   $��15q�6#    � RowDefinitions 55   
   �� 56      $
��Auto �6   6,   �� 57      $
��Auto �6   6,   �� 58      $
��Auto �6   6,   .+�5:   
   �� -   5         ��Name$ 	lblHeader��5:      $��22(�6'   $
��Bold"�65    �� TextAlignment$ Center=�6H   $��הפקת דוחות��6_   $��0��6q   $
��Auto��6~   $��	UnderLine{�6�   $��11q�6�   �� 5<   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5=      �� -   5        	 ��Name$	 lb��5=      
 �� DisplayMemberPath$)
 #U_CRYSTAL_REPORT_USER.U_HEBREW_NAME��6l   $
��Left=�6�   ��6K   �� *
ReportsA+6�   �� 5>      -   5        $		 lb2��5>      $)
 #U_CRYSTAL_REPORT_USER.U_HEBREW_NAME��6m   $��Right=�6�   ��6L   �� *
ReportsB+6�   5@      �� 5B   
   $��2��6   $ 
Horizontal=�6"   $��Center=�6;   .+�5D      �� -   5        $��55��5D   3   $��הפעל��6?   $	��128��6N   �� 5E      -   5        $��55��5E   +   $��
יציאה��67   $	��128��6G   5H      5L      5M      