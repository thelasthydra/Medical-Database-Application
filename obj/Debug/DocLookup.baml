   M S B A M L   `   `   `  ����U  QMedicalDatabaseApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:MedicalDatabaseApplicationMedicalDatabaseApplication  5   	   *    $MedicalDatabaseApplication.DocLookup   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:MedicalDatabaseApplication     5   	     � Title$  
Doc Lookup��5   	   $	��350��6   $	��750��6)    � WindowStartupLocation$ CenterOwner=�65    � Icon$! Resources/logo_Fyz_icon.ico��6Y   .��5	      �  � RowDefinitions 5
   
   �� 5      $��40 �6   �� 5      $��* �6   �� 5      $��50 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ btnReset��5      $��Clear Search��6#   $	��125��5      $��25��6   $y�Visible=�6)   6>   �� 5   
    �� Orientation$ 
Horizontal=�6   $��2��6.   $��0��6>   $��Center=�6K   $��Center=�6h   .+�5      �� -   5         ��Name$ 	txtSearch��5      $��Enter Doctor Last Name��6)    � ToolTip$ Enter Doctor's Last Name��6G   $��25��5      $	��150��6!   $��2q�6-   
�������$��Center=�5      $��Center=�69   5   3   �� 5      -   5        $ 	btnSearch��5      $��Search��6(   $��75��5       $��25��6    $��2q�6,   67   5!      &   System.Windows.Controls.DataGrid  5#   
   -   5        	  Name$	 dgOutput��5#      
   SelectionMode$
 Single=�5$         AutoGenerateColumns .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6H   $��Stretch=�5%      $��Stretch=�63   $��5q�6O      
IsReadOnly . $��1��6l   $��3��6y      Columns 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn   Header$ 
First Name��6E   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *	FirstName+6[     5)      $ 	Last Name��6E    6%   �� *
LastName+6Z     5*      $ Address��6E    6%   �� *	Address+6X     5+      $ 
Home Phone��6E    6%   �� *	HomePhone+6[     5,      $ Mobile Phone��6E    6%   �� *MobilePhone+6]     5-      $	 MRN��6E    6%   �� *Mrn+6T     5.      $ Doc Type��6E    6%   �� *	DocType+6Y   51      �� 53   
   -   5        $ 	btnSelect��53      $��Select��6$   $	��125��65   $��25��6A   $��2��6M   6Z   �� 55   
   -   5        $ 	btnCancel��55      $��Cancel��6$   $	��125��65   $��25��6A   $��2��6M   $��1��6Z   6j   �� 57   
   -   5        $ 	btnDelete��57      $��Delete��6$   $	��125��65   $��25��6A   $��2��6M   $��2��6Z   6j   58      59      