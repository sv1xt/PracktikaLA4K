using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // Для ObservableCollection
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace practika
{
    public partial class IncomingInvoiceWindow : Window
    {
        private string _connectionString = "Data Source=DESKTOP-UGJSU56\\SQLEXPRESS01;Initial Catalog=SkladUchet;Integrated Security=True;TrustServerCertificate=True";
        private int? _invoiceId; // Nullable int (может быть null - для добавления, или значение - для редактирования)
        public ObservableCollection<IncomingInvoiceItemViewModel> Items { get; set; } = new ObservableCollection<IncomingInvoiceItemViewModel>();


        // Конструктор для ДОБАВЛЕНИЯ пользователя
        public IncomingInvoiceWindow()
        {
            InitializeComponent();
            LoadSuppliers();
            dpInvoiceDate.SelectedDate = DateTime.Today; // Устанавливаем текущую дату по умолчанию
            dgInvoiceItems.ItemsSource = Items; // Привязываем коллекцию к DataGrid

        }

        // Конструктор для РЕДАКТИРОВАНИЯ пользователя
        public IncomingInvoiceWindow(int invoiceId)
        {
            InitializeComponent();
            _invoiceId = invoiceId;
            LoadSuppliers();
            LoadInvoiceData();
            dgInvoiceItems.ItemsSource = Items;
        }

        private void LoadSuppliers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT Supplier_ID, Название_Поставщика FROM Поставщики";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Supplier> suppliers = new List<Supplier>(); // Supplier -  класс из SupplierWindow
                            while (reader.Read())
                            {
                                suppliers.Add(new Supplier { Supplier_ID = (int)reader["Supplier_ID"], Название_Поставщика = (string)reader["Название_Поставщика"] });
                            }
                            cmbSupplier.ItemsSource = suppliers;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке поставщиков: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false; // Закрываем окно в случае ошибки
            }
        }

        private void LoadInvoiceData()
        {
            // TODO: Загрузить данные накладной и строки накладной из БД
            if (_invoiceId == null) return;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    //Загрузка шапки
                    string headQuery = @"SELECT Номер_Накладной, Дата_Накладной, Supplier_ID
                                        FROM ПриходныеНакладные
                                        WHERE Invoice_ID = @InvoiceId";
                    using (SqlCommand command = new SqlCommand(headQuery, connection))
                    {
                        command.Parameters.AddWithValue("@InvoiceId", _invoiceId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtInvoiceNumber.Text = (string)reader["Номер_Накладной"];
                                dpInvoiceDate.SelectedDate = (DateTime)reader["Дата_Накладной"];
                                cmbSupplier.SelectedValue = (int)reader["Supplier_ID"];
                            }
                        }
                    }
                    //Загрузка строк
                    string itemsQuery = @"SELECT ii.Product_ID, p.Название_Товара, ii.Количество, ii.Цена
                                        FROM СтрокиПриходнойНакладной ii
                                        JOIN Товары p ON ii.Product_ID = p.Product_ID
                                        WHERE ii.Invoice_ID = @InvoiceId";
                    using (SqlCommand command = new SqlCommand(itemsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@InvoiceId", _invoiceId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Items.Add(new IncomingInvoiceItemViewModel //Добавляем в ObservableCollection
                                {
                                    ProductId = (int)reader["Product_ID"],
                                    ProductName = (string)reader["Название_Товара"],
                                    Quantity = (int)reader["Количество"],
                                    Price = (decimal)reader["Цена"],
                                    Total = (int)reader["Количество"] * (decimal)reader["Цена"]
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных накладной: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false; //В случае ошибки, закрываем
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Сохранить накладную и строки накладной в БД (с обновлением остатков)
            // Валидация
            if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text) || dpInvoiceDate.SelectedDate == null || cmbSupplier.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля (номер, дата, поставщик).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Items.Count == 0) // Проверка, что есть хотя бы один товар
            {
                MessageBox.Show("Добавьте хотя бы один товар в накладную.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка корректности количества и цены в строках
            foreach (var item in Items)
            {
                if (item.Quantity <= 0 || item.Price <= 0)
                {
                    MessageBox.Show("Количество и цена должны быть больше нуля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            SqlTransaction transaction = null; // Транзакция
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    transaction = connection.BeginTransaction(); //Начинаем транзакцию

                    string invoiceQuery;
                    if (_invoiceId == null) // ДОБАВЛЕНИЕ
                    {
                        invoiceQuery = "INSERT INTO ПриходныеНакладные (Номер_Накладной, Дата_Накладной, Supplier_ID, Сумма_Накладной) OUTPUT INSERTED.Invoice_ID VALUES (@InvoiceNumber, @InvoiceDate, @SupplierId, 0);"; //Сумму пока ставим 0
                    }
                    else    //РЕДАКТИРОВАНИЕ
                    {
                        invoiceQuery = "UPDATE ПриходныеНакладные SET Номер_Накладной = @InvoiceNumber, Дата_Накладной = @InvoiceDate, Supplier_ID = @SupplierId WHERE Invoice_ID = @InvoiceId";
                    }
                    int newInvoiceId; //Для сохранения ID новой накладной
                    using (SqlCommand command = new SqlCommand(invoiceQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNumber.Text);
                        command.Parameters.AddWithValue("@InvoiceDate", dpInvoiceDate.SelectedDate.Value);
                        command.Parameters.AddWithValue("@SupplierId", ((Supplier)cmbSupplier.SelectedItem).Supplier_ID);
                        if (_invoiceId != null)
                        {
                            command.Parameters.AddWithValue("@InvoiceId", _invoiceId.Value);
                        }
                        if (_invoiceId == null)
                        {
                            newInvoiceId = (int)command.ExecuteScalar(); // Получаем ID новой накладной
                        }
                        else
                        {
                            newInvoiceId = _invoiceId.Value;
                            command.ExecuteNonQuery();
                        }
                    }

                    //Удаляем старые строки (при редактировании)
                    if (_invoiceId.HasValue)
                    {
                        string deleteItemsQuery = "DELETE FROM СтрокиПриходнойНакладной WHERE Invoice_ID = @InvoiceId";
                        using (SqlCommand command = new SqlCommand(deleteItemsQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@InvoiceId", _invoiceId.Value);
                            command.ExecuteNonQuery();
                        }
                    }
                    // Добавление/обновление СТРОК накладной
                    string itemsQuery = "INSERT INTO СтрокиПриходнойНакладной (Invoice_ID, Product_ID, Количество, Цена) VALUES (@InvoiceId, @ProductId, @Quantity, @Price)";

                    decimal totalAmount = 0; // Общая сумма накладной (для обновления шапки)
                    using (SqlCommand command = new SqlCommand(itemsQuery, connection, transaction))
                    {
                        //Параметры, которые будут использоваться многократно
                        command.Parameters.Add("@InvoiceId", System.Data.SqlDbType.Int);
                        command.Parameters.Add("@ProductId", System.Data.SqlDbType.Int);
                        command.Parameters.Add("@Quantity", System.Data.SqlDbType.Int);
                        command.Parameters.Add("@Price", System.Data.SqlDbType.Decimal);
                        foreach (var item in Items)
                        {
                            command.Parameters["@InvoiceId"].Value = newInvoiceId;
                            command.Parameters["@ProductId"].Value = item.ProductId;
                            command.Parameters["@Quantity"].Value = item.Quantity;
                            command.Parameters["@Price"].Value = item.Price;
                            command.ExecuteNonQuery();

                            totalAmount += item.Quantity * item.Price; // Считаем сумму
                                                                       //Обновление остатков
                            UpdateStock(connection, transaction, item.ProductId, item.Quantity, true); //true - приход
                        }
                    }

                    //Обновляем сумму в шапке накладной
                    string updateTotalQuery = "UPDATE ПриходныеНакладные SET Сумма_Накладной = @TotalAmount WHERE Invoice_ID = @InvoiceId";
                    using (SqlCommand command = new SqlCommand(updateTotalQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        command.Parameters.AddWithValue("@InvoiceId", newInvoiceId);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit(); //Если всё хорошо, комитим
                    DialogResult = true; // Закрываем окно
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback(); //Если ошибка, откатываем изменения
                MessageBox.Show($"Ошибка при сохранении накладной: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Закрываем окно без сохранения
        }
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно выбора товара (ProductSelectionWindow)
            ProductSelectionWindow productSelectionWindow = new ProductSelectionWindow();
            if (productSelectionWindow.ShowDialog() == true)
            {
                //Добавляем выбранный товар в список
                var selectedProduct = productSelectionWindow.SelectedProduct;
                if (selectedProduct != null)
                {
                    // Проверяем, нет ли уже такого товара в списке
                    if (!Items.Any(item => item.ProductId == selectedProduct.ProductId))
                    {
                        Items.Add(new IncomingInvoiceItemViewModel
                        {
                            ProductId = selectedProduct.ProductId,
                            ProductName = selectedProduct.ProductName,
                            Quantity = 1, // Начальное количество 1
                            Price = selectedProduct.Price,
                            Total = selectedProduct.Price // Сумма = цена * 1
                        });

                    }
                    else
                    {
                        MessageBox.Show("Товар уже добавлен в накладную.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            //Удаляем
            if (sender is Button button && button.DataContext is IncomingInvoiceItemViewModel item)
            {
                Items.Remove(item);
            }
        }
        private void UpdateStock(SqlConnection connection, SqlTransaction transaction, int productId, int quantity, bool isIncoming)
        {
            // + quantity если приход, - quantity если расход
            int change = isIncoming ? quantity : -quantity;
            string query = @"
              MERGE INTO ТоварыНаСкладе AS target
              USING (SELECT @ProductId AS Product_ID, @Change AS Quantity,
							(SELECT Warehouse_ID FROM Склады WHERE Название_Склада = 'Главный склад') as Warehouse_ID) AS source
              ON (target.Product_ID = source.Product_ID)
              WHEN MATCHED THEN
                  UPDATE SET target.Количество = target.Количество + source.Quantity
              WHEN NOT MATCHED THEN
                  INSERT (Product_ID, Количество, Warehouse_ID)
                  VALUES (source.Product_ID, source.Quantity, source.Warehouse_ID);";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@ProductId", productId);
                command.Parameters.AddWithValue("@Change", change);
                command.ExecuteNonQuery();
            }
        }
    }
    // ViewModel для строки накладной
    public class IncomingInvoiceItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
    //Вспомогательный класс
    public class Supplier
    {
        public int Supplier_ID { get; set; }
        public string Название_Поставщика { get; set; }
    }
}