   M S B A M L   `   `   `  ����  practika%    practika.ProductSelectionWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Выбор товара��5   	   $	��300��6   $	��400��6+    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��
dgProducts��5         AutoGenerateColumns .     
IsReadOnly .    SelectionMode$ Single=�6S   $��5q�6j      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6O   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 61   �� *	ProductId+6O     5      $# Название товара��6%   $ * 6^   	 6>   �� *ProductName+6^     5      $ Цена��6%   $
 Auto 6]   	 63   �� *Price+
 �� StringFormat$
 C��6]   5      �� 5   
   $��4��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Right=�6K   .+�5      �� -   5        $��	btnSelect��5      $��Выбрать��6&   $��5q�68   $	��5,2q�6C   6Q   �� 5      -   5        $��	btnCancel��5      $��Отмена��6&   $��5q�67   $	��5,2q�6B   6P   5      5      5      