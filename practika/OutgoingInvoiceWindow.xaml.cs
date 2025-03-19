using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace practika
{
    public partial class OutgoingInvoiceWindow : Window
    {
        private string _connectionString = "Data Source=DESKTOP-UGJSU56\\SQLEXPRESS01;Initial Catalog=SkladUchet;Integrated Security=True;TrustServerCertificate=True";
        private int? _invoiceId; // Nullable int
        public ObservableCollection<OutgoingInvoiceItemViewModel> Items { get; set; } = new ObservableCollection<OutgoingInvoiceItemViewModel>();

        public OutgoingInvoiceWindow()
        {
            InitializeComponent();
            LoadCustomers();
            dpInvoiceDate.SelectedDate = DateTime.Today;
            dgInvoiceItems.ItemsSource = Items;
        }

        public OutgoingInvoiceWindow(int invoiceId)
        {
            InitializeComponent();
            _invoiceId = invoiceId;
            LoadCustomers();
            LoadInvoiceData();
            dgInvoiceItems.ItemsSource = Items;
        }

        private void LoadCustomers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT Customer_ID, Название_Клиента FROM Клиенты";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Customer> customers = new List<Customer>();
                            while (reader.Read())
                            {
                                customers.Add(new Customer { Customer_ID = (int)reader["Customer_ID"], Название_Клиента = (string)reader["Название_Клиента"] });
                            }
                            cmbCustomer.ItemsSource = customers;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке клиентов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }


        private void LoadInvoiceData()
        {
            if (_invoiceId == null) return;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string headQuery = @"SELECT Номер_Накладной, Дата_Накладной, Customer_ID
                                    FROM РасходныеНакладные
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
                                cmbCustomer.SelectedValue = (int)reader["Customer_ID"];
                            }
                        }
                    }
                    string itemsQuery = @"SELECT ii.Product_ID, p.Название_Товара, ii.Количество, ii.Цена
                                        FROM СтрокиРасходнойНакладной ii
                                        JOIN Товары p ON ii.Product_ID = p.Product_ID
                                        WHERE ii.Invoice_ID = @InvoiceId";
                    using (SqlCommand command = new SqlCommand(itemsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@InvoiceId", _invoiceId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Items.Add(new OutgoingInvoiceItemViewModel
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
                DialogResult = false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text) || dpInvoiceDate.SelectedDate == null || cmbCustomer.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля (номер, дата, клиент).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Items.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы один товар в накладную.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var item in Items)
            {
                if (item.Quantity <= 0 || item.Price <= 0)
                {
                    MessageBox.Show("Количество и цена должны быть больше нуля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    string invoiceQuery;
                    if (_invoiceId == null) // ДОБАВЛЕНИЕ
                    {
                        invoiceQuery = "INSERT INTO РасходныеНакладные (Номер_Накладной, Дата_Накладной, Customer_ID, Сумма_Накладной) OUTPUT INSERTED.Invoice_ID VALUES (@InvoiceNumber, @InvoiceDate, @CustomerId, 0);";
                    }
                    else // РЕДАКТИРОВАНИЕ
                    {
                        invoiceQuery = "UPDATE РасходныеНакладные SET Номер_Накладной = @InvoiceNumber, Дата_Накладной = @InvoiceDate, Customer_ID = @CustomerId WHERE Invoice_ID = @InvoiceId";
                    }

                    int newInvoiceId;
                    using (SqlCommand command = new SqlCommand(invoiceQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@InvoiceNumber", txtInvoiceNumber.Text);
                        command.Parameters.AddWithValue("@InvoiceDate", dpInvoiceDate.SelectedDate.Value);
                        command.Parameters.AddWithValue("@CustomerId", ((Customer)cmbCustomer.SelectedItem).Customer_ID); // Исправлено!

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

                    // Удаляем старые строки (при редактировании)
                    if (_invoiceId.HasValue)
                    {
                        string deleteItemsQuery = "DELETE FROM СтрокиРасходнойНакладной WHERE Invoice_ID = @InvoiceId";
                        using (SqlCommand command = new SqlCommand(deleteItemsQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@InvoiceId", _invoiceId.Value);
                            command.ExecuteNonQuery();
                        }
                    }

                    // Добавление/обновление СТРОК накладной
                    string itemsQuery = "INSERT INTO СтрокиРасходнойНакладной (Invoice_ID, Product_ID, Количество, Цена) VALUES (@InvoiceId, @ProductId, @Quantity, @Price)";
                    decimal totalAmount = 0;
                    using (SqlCommand command = new SqlCommand(itemsQuery, connection, transaction))
                    {
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

                            totalAmount += item.Quantity * item.Price;
                            UpdateStock(connection, transaction, item.ProductId, item.Quantity, false); // false - расход!
                        }
                    }
                    //Обновляем сумму в шапке накладной
                    string updateTotalQuery = "UPDATE РасходныеНакладные SET Сумма_Накладной = @TotalAmount WHERE Invoice_ID = @InvoiceId";
                    using (SqlCommand command = new SqlCommand(updateTotalQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        command.Parameters.AddWithValue("@InvoiceId", newInvoiceId);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show($"Ошибка при сохранении накладной: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
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

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            ProductSelectionWindow productSelectionWindow = new ProductSelectionWindow();
            if (productSelectionWindow.ShowDialog() == true)
            {
                var selectedProduct = productSelectionWindow.SelectedProduct;
                if (selectedProduct != null)
                {
                    if (!Items.Any(item => item.ProductId == selectedProduct.ProductId))
                    {
                        Items.Add(new OutgoingInvoiceItemViewModel //Исправлено
                        {
                            ProductId = selectedProduct.ProductId,
                            ProductName = selectedProduct.ProductName,
                            Quantity = 1, // Начальное количество 1
                            Price = selectedProduct.Price,
                            Total = selectedProduct.Price
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
            if (sender is Button button && button.DataContext is OutgoingInvoiceItemViewModel item)
            {
                Items.Remove(item);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
    // ViewModel для строки накладной
    public class OutgoingInvoiceItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
    //Вспомогательный класс
    public class Customer
    {
        public Customer()
        {
        }

        public int Customer_ID { get; set; }
        public string Название_Клиента { get; set; }
    }
}