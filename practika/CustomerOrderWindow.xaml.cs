using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static practika.SalesManagerWindow;

namespace practika
{
    public partial class CustomerOrderWindow : Window
    {
        private string _connectionString = "Data Source=DESKTOP-UGJSU56\\SQLEXPRESS01;Initial Catalog=SkladUchet;Integrated Security=True;TrustServerCertificate=True";
        private int? _orderId; // Nullable int
        public ObservableCollection<CustomerOrderItemViewModel> Items { get; set; } = new ObservableCollection<CustomerOrderItemViewModel>();

        // Конструктор для ДОБАВЛЕНИЯ заказа
        public CustomerOrderWindow()
        {
            InitializeComponent();
            LoadCustomers();
            dpOrderDate.SelectedDate = DateTime.Today;
            dgOrderItems.ItemsSource = Items;
        }

        // Конструктор для РЕДАКТИРОВАНИЯ заказа
        public CustomerOrderWindow(int orderId)
        {
            InitializeComponent();
            _orderId = orderId;
            LoadCustomers();
            LoadOrderData();
            dgOrderItems.ItemsSource = Items;
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
                            List<Customer> customers = new List<Customer>(); // Класс Customer должен быть определен!
                            while (reader.Read())
                            {
                                customers.Add(new Customer { Customer_ID = (int)reader["Customer_ID"], Название_Клиента = (string)reader["Название_Клиента"] });
                            }
                            cmbCustomer.ItemsSource = customers;
                            cmbCustomer.DisplayMemberPath = "Название_Клиента";
                            cmbCustomer.SelectedValuePath = "Customer_ID";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке клиентов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false; // Закрываем окно в случае ошибки
            }
        }


        private void LoadOrderData()
        {
            if (_orderId == null) return;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    // 1. Загружаем данные заголовка заказа
                    string headerQuery = @"SELECT OrderNumber, OrderDate, CustomerID FROM CustomerOrders WHERE CustomerOrderID = @OrderId";
                    using (SqlCommand command = new SqlCommand(headerQuery, connection))
                    {
                        command.Parameters.AddWithValue("@OrderId", _orderId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtOrderNumber.Text = (string)reader["OrderNumber"];
                                dpOrderDate.SelectedDate = (DateTime)reader["OrderDate"];
                                cmbCustomer.SelectedValue = (int)reader["CustomerID"];  // Устанавливаем выбранного поставщика
                            }
                        }
                    }

                    // 2. Загружаем строки заказа
                    string itemsQuery = @"
                        SELECT poi.Product_ID, p.Название_Товара, poi.Quantity, p.Цена  -- Цена товара берется из справочника товаров
                        FROM CustomerOrderItems poi
                        JOIN Товары p ON poi.Product_ID = p.Product_ID
                        WHERE poi.CustomerOrderID = @OrderId";
                    using (SqlCommand command = new SqlCommand(itemsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@OrderId", _orderId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Items.Add(new CustomerOrderItemViewModel
                                {
                                    ProductId = (int)reader["Product_ID"],
                                    ProductName = (string)reader["Название_Товара"],
                                    Quantity = (int)reader["Quantity"],
                                    Price = (decimal)reader["Цена"],
                                    Total = (int)reader["Quantity"] * (decimal)reader["Цена"] // Сумма вычисляется
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }


        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
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
                        Items.Add(new CustomerOrderItemViewModel
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
                        MessageBox.Show("Товар уже добавлен в заказ.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is CustomerOrderItemViewModel item)
            {
                Items.Remove(item);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(txtOrderNumber.Text) || dpOrderDate.SelectedDate == null || cmbCustomer.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля (номер, дата, клиент).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Items.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы один товар в заказ.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var item in Items)  // Дополнительная валидация количества
            {
                if (item.Quantity <= 0)
                {
                    MessageBox.Show("Количество товара должно быть больше нуля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            SqlTransaction transaction = null; // Объявляем транзакцию
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    transaction = connection.BeginTransaction(); // Начинаем транзакцию

                    string orderQuery;
                    if (_orderId == null) // ДОБАВЛЕНИЕ
                    {
                        orderQuery = "INSERT INTO CustomerOrders (OrderNumber, OrderDate, CustomerID, TotalAmount) OUTPUT INSERTED.CustomerOrderID VALUES (@OrderNumber, @OrderDate, @CustomerId, 0);";
                    }
                    else // РЕДАКТИРОВАНИЕ
                    {
                        orderQuery = "UPDATE CustomerOrders SET OrderNumber = @OrderNumber, OrderDate = @OrderDate, CustomerID = @CustomerId, TotalAmount = 0 WHERE CustomerOrderID = @OrderId;";  // TotalAmount пока ставим 0
                    }

                    int newOrderId;  //Для хранения ID
                    using (SqlCommand command = new SqlCommand(orderQuery, connection, transaction)) // Передаем транзакцию
                    {
                        command.Parameters.AddWithValue("@OrderNumber", txtOrderNumber.Text);
                        command.Parameters.AddWithValue("@OrderDate", dpOrderDate.SelectedDate.Value);
                        command.Parameters.AddWithValue("@CustomerId", ((Customer)cmbCustomer.SelectedItem).Customer_ID); // Исправлено!

                        if (_orderId != null)
                        {
                            command.Parameters.AddWithValue("@OrderId", _orderId.Value);
                        }

                        if (_orderId == null) //Если новый заказ
                        {
                            newOrderId = (int)command.ExecuteScalar(); // Получаем ID нового заказа
                        }
                        else
                        {
                            newOrderId = _orderId.Value;
                            command.ExecuteNonQuery(); //Для редактирования
                        }

                    }

                    //Удаляем старые заказы
                    if (_orderId.HasValue)
                    {
                        string deleteItemsQuery = "DELETE FROM CustomerOrderItems WHERE CustomerOrderID = @OrderId";
                        using (SqlCommand command = new SqlCommand(deleteItemsQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@OrderId", _orderId.Value);
                            command.ExecuteNonQuery();
                        }
                    }

                    // Добавление/обновление СТРОК заказа
                    string itemsQuery = "INSERT INTO CustomerOrderItems (CustomerOrderID, Product_ID, Quantity) VALUES (@OrderId, @ProductId, @Quantity)";
                    decimal totalAmount = 0; // Общая сумма заказа
                    using (SqlCommand command = new SqlCommand(itemsQuery, connection, transaction)) // Передаем транзакцию
                    {
                        command.Parameters.Add("@OrderId", System.Data.SqlDbType.Int);
                        command.Parameters.Add("@ProductId", System.Data.SqlDbType.Int);
                        command.Parameters.Add("@Quantity", System.Data.SqlDbType.Int);

                        foreach (var item in Items)
                        {
                            command.Parameters["@OrderId"].Value = newOrderId;
                            command.Parameters["@ProductId"].Value = item.ProductId;
                            command.Parameters["@Quantity"].Value = item.Quantity;
                            command.ExecuteNonQuery();

                            totalAmount += item.Quantity * item.Price; // Считаем сумму
                        }
                    }
                    //Обновляем сумму заказа
                    string updateTotalQuery = "UPDATE CustomerOrders SET TotalAmount = @TotalAmount WHERE CustomerOrderID = @OrderId";
                    using (SqlCommand command = new SqlCommand(updateTotalQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        command.Parameters.AddWithValue("@OrderId", newOrderId);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit(); // Фиксируем транзакцию
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();  // Откатываем транзакцию в случае ошибки
                MessageBox.Show($"Ошибка при сохранении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}