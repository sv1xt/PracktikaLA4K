   M S B A M L   `   `   `  ����  practika    practika.WarehouseWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
Склад��5   	   $	��250��6   $	��400��6$    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.   .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6   .��6-    Название склада:6?   �� 5   
   -   5        $��txtWarehouseName��5   /   $��0��6   $��1��6   $��5q�6G   �� 5   
   $��1��6   $��0��6   .��6-   Адрес:65   �� 5   
   -   5        $��
txtAddress��5   /   $��1��6   $��1��6   $��5q�6A   �� 5   
   $��2��6   $��0��6   .��6-   Тип склада:6:   �� 5   
   -   5        $��cmbWarehouseType��5   0   $��2��6   $��1��6    $��5q�6H    �� DisplayMemberPath$ Название_Типа��6S    �� SelectedValuePath$ Warehouse_Type_ID��6u   �� 5   
   -   5        $��btnSave��5   .   $��4��6   $��1��6   $��Сохранить��6=   $��5q�6Q   6\   �� 5   
   -   5        $��	btnCancel��5   .   $��4��6   $��0��6   $��Отмена��6?   $��5q�6P   6[   5      5      