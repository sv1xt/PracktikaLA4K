   M S B A M L   `   `   `  ����  practika    practika.AccountantWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$#  Окно бухгалтера��5   	   $	��450��6!   $	��800��6.    � WindowStartupLocation$ CenterScreen=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $��* �6   .+�5   
   o� $��0��6   .@�5      m� $��Файл��6   .@�5      m� -   5        $��
Выход��5      6*   5      m� 5      -   5        $��menuReports��5   '   $��Отчёты��6   .@�5      m� -   5        $��menuWarehouseReport��5   5   $$��Отчет по складам��6   6P   m� 5      -   5        $��menuTurnoverReport��5   6   $&�� Отчет по оборотам��6   6P   m� 5      -   5        $��menuStockReport��5   6   $&�� Отчет по остаткам��6   6M   5      m� 5      -   5        $��menuInventory��5   /   $"��Инвентаризация��6   6D   5      �� 5   
   -   5        $��
tabControl��5   "   $��1��6   .@�5      �� -   5        $��tabReportWarehouse��5   0   $$��Отчёт по складам��6   .��5      &   System.Windows.Controls.DataGrid  -	   5        $��dgReportWarehouse��5         AutoGenerateColumns .    
IsReadOnly . 6O   5      �� 5      -
   5        $��tabReportTurnover��5   1   $&�� Отчёт по оборотам��6   .��5        -   5        $��dgReportTurnover��5       .  . 6N   5      �� 5!      -   5        $��tabReportStock��5!   1   $&�� Отчёт по остаткам��6   .��5"        -   5        $��dgReportStock��5"       .  . 6K   5#      �� 5%      -   5        $��tabInventory��5%   .   $"��Инвентаризация��6   .��5&      �  5'      �� 5(      $
��Auto �6(   �� 5)      $��* �6(   .+�5+      �� $��0��6!    �� Orientation$ 
Horizontal=�6.   $��5q�6G   .+�5,      �� -   5        $��btnStartInventory��5,   !   $/��)Начать инвентаризацию��6:   $��5q�6Z   $��10, 3q�6e   6u   �� 5-      -   5        $��btnCompleteInventory��5-   !   $5��/Завершить инвентаризацию��6=   $��5q�6`   $��10, 3q�6k   6{   5.        5/      -   5        $��dgInventory��5/      $��1��62    .   .     SelectionMode$ Single=�6n   $��5q�6�      Columns 50      0  *System.Windows.Controls.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn   Header$ ID Товара��61   	   
Visibility$	 	Collapsed=�6b   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6D   �� *	ProductId+6b     52      $ 
Товар��61      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6`      
IsReadOnly . 
 6@   �� *ProductName+6j     53      $) #Учетное количество��61   $ * 6n    . 
 6M   �� *BookQuantity+6x     54      $1 +Фактическое количество��61   $ * 6t   
 6Q   �� *ActualQuantity+6t     56      $ Разница��61   $ * 6a    . 
 6B   �� *
Difference+6k   58      59      5:      5;      5<      5=      