   M S B A M L   `   `   `  ����  practika    practika.ProductWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
Товар��5   	   $	��400��6   $	��400��6$    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.   .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6   .��6-    Название товара:6?   �� 5   
   -   5        $��txtProductName��5   /   $��0��6   $��1��6   $��5q�6E   �� 5   
   $��1��6   $��0��6   .��6-   Артикул:67   �� 5   
   -   5        $��
txtArticle��5   /   $��1��6   $��1��6   $��5q�6A   �� 5   
   $��2��6   $��0��6   .��6-   Штрихкод:68   �� 5   
   -   5        $��
txtBarcode��5   /   $��2��6   $��1��6   $��5q�6A   �� 5!   
   $��3��6   $��0��6   .��6-   Категория:69   �� 5"   
   -   5        $��cmbCategory��5"   0   $��3��6   $��1��6    $��5q�6C    �� DisplayMemberPath$) #Название_Категории��6N    �� SelectedValuePath$ Category_ID��6u   �� 5$   
   $��4��6   $��0��6   .��6-   $"Единица измерения:6A   �� 5%   
   -   5        $��cmbUnit��5%   0   $��4��6   $��1��6    $��5q�6?   $8 2Название_Единицы_Измерения��6J   $ Unit_ID��6y   �� 5'   
   $��5��6   $��0��6   .��6-   	Цена:64   �� 5(   
   -   5        $��txtPrice��5(   /   $��5��6   $��1��6   $��5q�6?   �� 5*   
   $��6��6   $��0��6   .��6-   Серийный номер:6>   �� 5+   
   -   5        $��txtSerialNumber��5+   /   $��6��6   $��1��6   $��5q�6F   �� 5-   
   $��7��6   $��0��6   .��6-   (&Минимальный остаток:6C   �� 5.   
   -   5        $��txtMinStockLevel��5.   /   $��7��6   $��1��6   $��5q�6G   �� 50   
   $��8��6   $��0��6   .��6-   Срок годности:6=   (  "System.Windows.Controls.DatePicker  51   
   -	   5        $��dpExpiryDate��51   2   $��8��6   $��1��6"   $��5q�6F   �� 53   
   -
   5        $��btnSave��53   .   $��9��6   $��1��6   $��Сохранить��6=   $��5q�6Q   6\   �� 54   
   -   5        $��	btnCancel��54   .   $��9��6   $��0��6   $��Отмена��6?   $��5q�6P   6[   55      56      