   M S B A M L   `   `   `  ����  practika"    practika.CustomerOrderWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Заказ клиенту��5   	   $	��450��6   $	��600��6,    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.   .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6   .��6-   Номер заказа:6<   �� 5   
   -   5        $��txtOrderNumber��5   /   $��0��6   $��1��6   $��5q�6E   �� 5   
   $��1��6   $��0��6   .��6-   Дата заказа:6;   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��dpOrderDate��5   2   $��1��6   $��1��6"   $��5q�6E      SelectedDate 6P   �� *	OrderDate+�� 5   
   $��2��6   $��0��6   .��6-   Клиент:66   �� 5   
   -   5        $��cmbCustomer��5   0   $��2��6   $��1��6    $��5q�6C    �� DisplayMemberPath$% Название_Клиента��6N    �� SelectedValuePath$ Customer_ID��6s   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dgOrderItems��5   D   $��3��6   $��0��6    $��2��60      AutoGenerateColumns .  $��5q�6t   	   CanUserAddRows	 .  
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID Товара��6%      
Visibility$ 	Collapsed=�6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *	ProductId+6V     5      $ 
Товар��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6T      
IsReadOnly .  64   �� *ProductName+6^     5       $ Количество��6%   $
 Auto 6V    69   �� *
Quantity+6V     5!      $ Цена��6%   $
 Auto 6]    .  63   �� *Price+ �� StringFormat$ C��6j     5"      $ 
Сумма��6%   $
 Auto 6^    .  64   �� *Total+$ C��6k   4  .System.Windows.Controls.DataGridTemplateColumn  5#         CellTemplate 5$      �� 5%      .R�5&      �� -   5        $��Удалить��5&   %   $��2q�6S   5'      5)      5+      �� 5,   
   $��4��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Right=�6K   .+�5-      �� -   5        $��
btnAddItem��5-      $!��Добавить товар��6(   $��5q�6A   $	��5,2q�6L   6Z   �� 5.      -   5        $��btnSave��5.      $��Сохранить��6$   $��5q�68   $
��10,3q�6C   6R   �� 5/      -   5        $��	btnCancel��5/      $��Отмена��6&   $��5q�67   $
��10,3q�6B   6Q   50      51      52      