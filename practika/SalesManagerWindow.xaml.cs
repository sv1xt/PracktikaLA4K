using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls; // Для TabControl и SelectionChangedEventArgs

namespace practika
{
    public partial class SalesManagerWindow : Window
    {
        private int _userId;
        private string _connectionString = "Data Source=DESKTOP-UGJSU56\\SQLEXPRESS01;Initial Catalog=SkladUchet;Integrated Security=True;TrustServerCertificate=True";

        public SalesManagerWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            Loaded += SalesManagerWindow_Loaded;
        }

        private void SalesManagerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Загружаем данные, которые нужны менеджеру при открытии окна
            LoadPurchaseOrders();  // Заказы поставщикам
            LoadCustomerOrders();   // Заказы клиентам
            LoadClients();         // Список клиентов
            LoadSuppliers();       // Список поставщиков
            LoadWarehouses(); //Склады
            LoadProducts(); //Товары
            LoadIncomingInvoices();//Приходные накладные
            LoadOutgoingInvoices();//Расходные накладные
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Методы для открытия вкладок (при нажатии на пункты меню)
        private void menuPurchaseOrders_Click(object sender, RoutedEventArgs e)
        {
            tabPurchaseOrders.Visibility = Visibility.Visible;
            tabPurchaseOrders.IsSelected = true;
        }

        private void menuCustomerOrders_Click(object sender, RoutedEventArgs e)
        {
            tabCustomerOrders.Visibility = Visibility.Visible;
            tabCustomerOrders.IsSelected = true;
            LoadCustomerOrders(); // Сразу загружаем заказы
        }

        private void menuClients_Click(object sender, RoutedEventArgs e)
        {
            //Открываем вкладку с клиентами
            tabViewClients.Visibility = Visibility.Visible;
            tabViewClients.IsSelected = true;
        }

        private void menuSuppliers_Click(object sender, RoutedEventArgs e)
        {
            //Открываем вкладку с поставщиками
            tabViewSuppliers.Visibility = Visibility.Visible;
            tabViewSuppliers.IsSelected = true;
        }

        private void menuWarehouses_Click(object sender, RoutedEventArgs e)
        {
            tabViewWarehouses.Visibility = Visibility.Visible;
            tabViewWarehouses.IsSelected = true;
            //LoadWarehouses();  // Загрузка при открытии вкладки
        }
        private void menuProducts_Click(object sender, RoutedEventArgs e)
        {
            tabViewProducts.Visibility = Visibility.Visible;
            tabViewProducts.IsSelected = true;
            //LoadProducts();
        }
        private void menuShowIncomingInvoices_Click(object sender, RoutedEventArgs e)
        {
            tabViewIncomingInvoices.Visibility = Visibility.Visible;
            tabViewIncomingInvoices.IsSelected = true;
            //LoadIncomingInvoices();
        }

        private void menuShowOutgoingInvoices_Click(object sender, RoutedEventArgs e)
        {
            tabViewOutgoingInvoices.Visibility = Visibility.Visible;
            tabViewOutgoingInvoices.IsSelected = true;
            //LoadOutgoingInvoices();
        }

        // Обработчики SelectionChanged для TabControl (если нужно загружать данные при переключении вкладок)
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl tabControl)
            {
                if (tabControl.SelectedItem == tabViewWarehouses)
                {
                    LoadWarehouses();
                }
                else if (tabControl.SelectedItem == tabViewProducts)
                {
                    LoadProducts();
                }
                else if (tabControl.SelectedItem == tabViewIncomingInvoices)
                {
                    LoadIncomingInvoices();
                }
                else if (tabControl.SelectedItem == tabViewOutgoingInvoices)
                {
                    LoadOutgoingInvoices();
                }
                else if (tabControl.SelectedItem == tabViewClients)
                {
                    LoadClients();
                }
                else if (tabControl.SelectedItem == tabViewSuppliers)
                {
                    LoadSuppliers();
                }
                else if (tabControl.SelectedItem == tabPurchaseOrders)
                {
                    LoadPurchaseOrders();
                }
                else if (tabControl.SelectedItem == tabCustomerOrders)
                {
                    LoadCustomerOrders(); // Добавлено!
                }
            }
        }


        // Методы загрузки данных (реализуем позже)
        private void LoadPurchaseOrders()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT po.PurchaseOrderID, po.OrderNumber, po.OrderDate, s.Название_Поставщика, po.TotalAmount, po.SupplierID
                FROM PurchaseOrders po
                JOIN Поставщики s ON po.SupplierID = s.Supplier_ID
                ORDER BY po.OrderDate DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<PurchaseOrderViewModel> orders = new List<PurchaseOrderViewModel>();
                            while (reader.Read())
                            {
                                orders.Add(new PurchaseOrderViewModel
                                {
                                    OrderId = (int)reader["PurchaseOrderID"],
                                    OrderNumber = (string)reader["OrderNumber"],
                                    OrderDate = (DateTime)reader["OrderDate"],
                                    SupplierName = (string)reader["Название_Поставщика"],
                                    SupplierID = (int)reader["SupplierID"],
                                    TotalAmount = (decimal)reader["TotalAmount"]
                                });
                            }
                            dgPurchaseOrders.ItemsSource = orders; // dgPurchaseOrders - DataGrid на вкладке "Заказы поставщикам"
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке заказов поставщикам: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadCustomerOrders()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT co.CustomerOrderID, co.OrderNumber, co.OrderDate, c.Название_Клиента, co.TotalAmount, co.CustomerID
                        FROM CustomerOrders co
                        JOIN Клиенты c ON co.CustomerID = c.Customer_ID
                        ORDER BY co.OrderDate DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<CustomerOrderViewModel> orders = new List<CustomerOrderViewModel>();
                            while (reader.Read())
                            {
                                orders.Add(new CustomerOrderViewModel
                                {
                                    OrderId = (int)reader["CustomerOrderID"],
                                    OrderNumber = (string)reader["OrderNumber"],
                                    OrderDate = (DateTime)reader["OrderDate"],
                                    CustomerName = (string)reader["Название_Клиента"],
                                    CustomerId = (int)reader["CustomerID"],
                                    TotalAmount = (decimal)reader["TotalAmount"]
                                });
                            }
                            dgCustomerOrders.ItemsSource = orders; // Привязываем к DataGrid
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке заказов клиентам: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadWarehouses()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT w.Warehouse_ID, w.Название_Склада, w.Адрес, wt.Название_Типа
                         FROM Склады w
                         JOIN ТипыСкладов wt ON w.Warehouse_Type_ID = wt.Warehouse_Type_ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<WarehouseViewModel> warehouses = new List<WarehouseViewModel>();
                            while (reader.Read())
                            {
                                warehouses.Add(new WarehouseViewModel
                                {
                                    WarehouseId = (int)reader["Warehouse_ID"],
                                    WarehouseName = (string)reader["Название_Склада"],
                                    Address = (string)reader["Адрес"],
                                    WarehouseTypeName = (string)reader["Название_Типа"]
                                });
                            }
                            dgViewWarehouses.ItemsSource = warehouses; // Привязываем данные к DataGrid
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке складов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT p.Product_ID, p.Название_Товара, p.Артикул, p.Цена,
                       c.Название_Категории, u.Название_Единицы_Измерения
                FROM Товары p
                JOIN КатегорииТоваров c ON p.Category_ID = c.Category_ID
                JOIN ЕдиницыИзмерения u ON p.Unit_ID = u.Unit_ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<ProductViewModel> products = new List<ProductViewModel>();
                            while (reader.Read())
                            {
                                products.Add(new ProductViewModel
                                {
                                    ProductId = (int)reader["Product_ID"],
                                    ProductName = (string)reader["Название_Товара"],
                                    Article = (string)reader["Артикул"],
                                    CategoryName = (string)reader["Название_Категории"],
                                    UnitName = (string)reader["Название_Единицы_Измерения"],
                                    Price = (decimal)reader["Цена"]
                                });
                            }
                            dgViewProducts.ItemsSource = products;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке товаров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadIncomingInvoices()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT i.Invoice_ID, i.Номер_Накладной, i.Дата_Накладной, s.Название_Поставщика, i.Сумма_Накладной
                              FROM ПриходныеНакладные i
                              JOIN Поставщики s ON i.Supplier_ID = s.Supplier_ID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<IncomingInvoiceViewModel> invoices = new List<IncomingInvoiceViewModel>();
                            while (reader.Read())
                            {
                                invoices.Add(new IncomingInvoiceViewModel
                                {
                                    InvoiceId = (int)reader["Invoice_ID"],
                                    InvoiceNumber = (string)reader["Номер_Накладной"],
                                    InvoiceDate = (DateTime)reader["Дата_Накладной"],
                                    SupplierName = (string)reader["Название_Поставщика"],
                                    TotalAmount = (decimal)reader["Сумма_Накладной"]
                                });
                            }
                            dgViewIncomingInvoices.ItemsSource = invoices;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузки приходных накладных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadOutgoingInvoices()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"SELECT o.Invoice_ID, o.Номер_Накладной, o.Дата_Накладной, c.Название_Клиента, o.Сумма_Накладной
                                FROM РасходныеНакладные o
                                JOIN Клиенты c ON o.Customer_ID = c.Customer_ID"; // JOIN с таблицей Клиенты

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<OutgoingInvoiceViewModel> invoices = new List<OutgoingInvoiceViewModel>();  // Исправлено!
                            while (reader.Read())
                            {
                                invoices.Add(new OutgoingInvoiceViewModel  // Исправлено!
                                {
                                    InvoiceId = (int)reader["Invoice_ID"],
                                    InvoiceNumber = (string)reader["Номер_Накладной"],
                                    InvoiceDate = (DateTime)reader["Дата_Накладной"],
                                    CustomerName = (string)reader["Название_Клиента"],  // Исправлено!
                                    TotalAmount = (decimal)reader["Сумма_Накладной"]
                                });
                            }
                            dgViewOutgoingInvoices.ItemsSource = invoices;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке расходных накладных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadClients()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT Customer_ID, Название_Клиента, Контактный_Телефон, Контактный_Email, Адрес FROM Клиенты";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<ClientViewModel> clients = new List<ClientViewModel>();
                            while (reader.Read())
                            {
                                clients.Add(new ClientViewModel
                                {
                                    CustomerId = (int)reader["Customer_ID"],
                                    CustomerName = (string)reader["Название_Клиента"],
                                    ContactPhone = (string)reader["Контактный_Телефон"],
                                    ContactEmail = (string)reader["Контактный_Email"],
                                    Address = (string)reader["Адрес"]
                                });
                            }
                            dgViewClients.ItemsSource = clients;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке клиентов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT Supplier_ID, Название_Поставщика, ИНН, КПП, Контактный_Телефон, Контактный_Email, Адрес FROM Поставщики";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<SupplierViewModel> suppliers = new List<SupplierViewModel>();
                            while (reader.Read())
                            {
                                suppliers.Add(new SupplierViewModel
                                {
                                    SupplierId = (int)reader["Supplier_ID"],
                                    SupplierName = (string)reader["Название_Поставщика"],
                                    INN = reader["ИНН"] != DBNull.Value ? (string)reader["ИНН"] : null, // Обработка NULL
                                    KPP = reader["КПП"] != DBNull.Value ? (string)reader["КПП"] : null, // Обработка NULL
                                    ContactPhone = (string)reader["Контактный_Телефон"],
                                    ContactEmail = (string)reader["Контактный_Email"],
                                    Address = (string)reader["Адрес"]
                                });
                            }
                            dgViewSuppliers.ItemsSource = suppliers;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке поставщиков: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчики для вкладки "Заказы поставщикам"
        private void btnAddPurchaseOrder_Click(object sender, RoutedEventArgs e)
        {
            PurchaseOrderWindow purchaseOrderWindow = new PurchaseOrderWindow();
            if (purchaseOrderWindow.ShowDialog() == true)
            {
                LoadPurchaseOrders(); // Обновляем список заказов
            }
        }

        private void btnEditPurchaseOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgPurchaseOrders.SelectedItem != null)
            {
                PurchaseOrderViewModel selectedOrder = (PurchaseOrderViewModel)dgPurchaseOrders.SelectedItem;
                PurchaseOrderWindow purchaseOrderWindow = new PurchaseOrderWindow(selectedOrder.OrderId);
                if (purchaseOrderWindow.ShowDialog() == true)
                {
                    LoadPurchaseOrders();
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для редактирования.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDeletePurchaseOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgPurchaseOrders.SelectedItem != null)
            {
                PurchaseOrderViewModel selectedOrder = (PurchaseOrderViewModel)dgPurchaseOrders.SelectedItem;

                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить заказ №{selectedOrder.OrderNumber}?", "Удаление заказа", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(_connectionString))
                        {
                            connection.Open();
                            //Сначала удаляем строки
                            string deleteItemsQuery = "DELETE FROM PurchaseOrderItems WHERE PurchaseOrderID = @OrderId";
                            using (SqlCommand deleteItemsCommand = new SqlCommand(deleteItemsQuery, connection))
                            {
                                deleteItemsCommand.Parameters.AddWithValue("@OrderId", selectedOrder.OrderId);
                                deleteItemsCommand.ExecuteNonQuery();
                            }
                            //Удаляем заказ
                            string query = "DELETE FROM PurchaseOrders WHERE PurchaseOrderID = @OrderId";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@OrderId", selectedOrder.OrderId);
                                command.ExecuteNonQuery();
                            }
                        }
                        LoadPurchaseOrders(); // Обновляем список
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void btnAddCustomerOrder_Click(object sender, RoutedEventArgs e)
        {
            CustomerOrderWindow customerOrderWindow = new CustomerOrderWindow(); // Открываем НОВОЕ окно
            if (customerOrderWindow.ShowDialog() == true)
            {
                LoadCustomerOrders(); // Обновляем список
            }
        }
        private void btnEditCustomerOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomerOrders.SelectedItem != null)
            {
                CustomerOrderViewModel selectedOrder = (CustomerOrderViewModel)dgCustomerOrders.SelectedItem;
                CustomerOrderWindow customerOrderWindow = new CustomerOrderWindow(selectedOrder.OrderId); // Открываем для РЕДАКТИРОВАНИЯ
                if (customerOrderWindow.ShowDialog() == true)
                {
                    LoadCustomerOrders();
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для редактирования.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDeleteCustomerOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomerOrders.SelectedItem != null)
            {
                CustomerOrderViewModel selectedOrder = (CustomerOrderViewModel)dgCustomerOrders.SelectedItem;

                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить заказ №{selectedOrder.OrderNumber}?", "Удаление заказа", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(_connectionString))
                        {
                            connection.Open();

                            // Сначала удаляем строки
                            string deleteItemsQuery = "DELETE FROM CustomerOrderItems WHERE CustomerOrderID = @OrderId";
                            using (SqlCommand deleteItemsCommand = new SqlCommand(deleteItemsQuery, connection))
                            {
                                deleteItemsCommand.Parameters.AddWithValue("@OrderId", selectedOrder.OrderId);
                                deleteItemsCommand.ExecuteNonQuery();
                            }

                            // Потом удаляем сам заказ
                            string deleteInvoiceQuery = "DELETE FROM CustomerOrders WHERE CustomerOrderID = @OrderId";
                            using (SqlCommand deleteInvoiceCommand = new SqlCommand(deleteInvoiceQuery, connection))
                            {
                                deleteInvoiceCommand.Parameters.AddWithValue("@OrderId", selectedOrder.OrderId);
                                deleteInvoiceCommand.ExecuteNonQuery();
                            }
                        }

                        LoadCustomerOrders(); // Обновляем список
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        // Обработчики для вкладки "Клиенты" (вызываем существующие окна)
        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();  // Используем окно, созданное для администратора
            if (clientWindow.ShowDialog() == true)
            {
                LoadClients(); // Обновляем список клиентов
            }
        }

        private void btnEditClient_Click(object sender, RoutedEventArgs e)
        {
            if (dgViewClients.SelectedItem != null) // Исправлено: dgViewClients
            {
                ClientViewModel selectedClient = (ClientViewModel)dgViewClients.SelectedItem; // Исправлено
                ClientWindow clientWindow = new ClientWindow(selectedClient.CustomerId); // Используем существующее окно
                if (clientWindow.ShowDialog() == true)
                {
                    LoadClients();
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента для редактирования.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (dgViewClients.SelectedItem != null)  // Исправлено: dgViewClients
            {
                ClientViewModel selectedClient = (ClientViewModel)dgViewClients.SelectedItem;// Исправлено
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить клиента '{selectedClient.CustomerName}'?", "Удаление клиента", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(_connectionString))
                        {
                            connection.Open();
                            string query = "DELETE FROM Клиенты WHERE Customer_ID = @CustomerId";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@CustomerId", selectedClient.CustomerId);
                                command.ExecuteNonQuery();
                            }
                        }
                        LoadClients();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        // Обработчики для вкладки "Поставщики" (вызываем существующие окна)
        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            SupplierWindow supplierWindow = new SupplierWindow();  // Используем окно, созданное для администратора
            if (supplierWindow.ShowDialog() == true)
            {
                LoadSuppliers();
            }
        }

        private void btnEditSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (dgViewSuppliers.SelectedItem != null)  // Исправлено: dgViewSuppliers
            {
                SupplierViewModel selectedSupplier = (SupplierViewModel)dgViewSuppliers.SelectedItem; // Исправлено
                SupplierWindow supplierWindow = new SupplierWindow(selectedSupplier.SupplierId); // Используем сущ. окно
                if (supplierWindow.ShowDialog() == true)
                {
                    LoadSuppliers();
                }
            }
            else
            {
                MessageBox.Show("Выберите поставщика для редактирования.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (dgViewSuppliers.SelectedItem != null)   // Исправлено: dgViewSuppliers
            {
                SupplierViewModel selectedSupplier = (SupplierViewModel)dgViewSuppliers.SelectedItem; // Исправлено
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить поставщика '{selectedSupplier.SupplierName}'?", "Удаление поставщика", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(_connectionString))
                        {
                            connection.Open();
                            string query = "DELETE FROM Поставщики WHERE Supplier_ID = @SupplierId";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@SupplierId", selectedSupplier.SupplierId);
                                command.ExecuteNonQuery();
                            }
                        }
                        LoadSuppliers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении поставщика: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите поставщика для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // ViewModel для приходных накладных (можно разместить в отдельном файле, если хотите)
        public class IncomingInvoiceViewModel
        {
            public int InvoiceId { get; set; }
            public string InvoiceNumber { get; set; }
            public DateTime InvoiceDate { get; set; }
            public string SupplierName { get; set; }
            public decimal TotalAmount { get; set; }
        }
        // ViewModel для РАСХОДНЫХ накладных
        public class OutgoingInvoiceViewModel
        {
            public int InvoiceId { get; set; }
            public string InvoiceNumber { get; set; }
            public DateTime InvoiceDate { get; set; }
            public string CustomerName { get; set; } // Вместо SupplierName
            public decimal TotalAmount { get; set; }
        }
        // ViewModel для строки инвентаризации
        public class InventoryItemViewModel
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int BookQuantity { get; set; } // Учетное количество
            public int ActualQuantity { get; set; } // Фактическое количество
            public int Difference { get; set; }     // Разница
        }
        // ViewModel для Товаров
        public class ProductViewModel
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string Article { get; set; }
            public string CategoryName { get; set; }
            public string UnitName { get; set; }
            public decimal Price { get; set; }
            // Другие свойства
        }
        // ViewModel для Складов
        public class WarehouseViewModel
        {
            public int WarehouseId { get; set; }
            public string WarehouseName { get; set; }
            public string Address { get; set; }
            public string WarehouseTypeName { get; set; }
            // Другие свойства, если нужны
        }
        // ViewModel для Клиентов
        public class ClientViewModel
        {
            public int CustomerId { get; set; }
            public string CustomerName { get; set; }
            public string ContactPhone { get; set; }
            public string ContactEmail { get; set; }
            public string Address { get; set; }
        }

        // ViewModel для Поставщиков
        public class SupplierViewModel
        {
            public int SupplierId { get; set; }
            public string SupplierName { get; set; }
            public string INN { get; set; }
            public string KPP { get; set; }
            public string ContactPhone { get; set; }
            public string ContactEmail { get; set; }
            public string Address { get; set; }
        }
        // ViewModel для заказов поставщикам
        public class PurchaseOrderViewModel
        {
            public int OrderId { get; set; }
            public string OrderNumber { get; set; }
            public DateTime OrderDate { get; set; }
            public string SupplierName { get; set; }  // Отображаем имя, а не ID
            public int SupplierID { get; set; }      // Для связывания
            public decimal TotalAmount { get; set; }    // Общая сумма заказа (можно вычислять)
            public List<PurchaseOrderItemViewModel> Items { get; set; } = new List<PurchaseOrderItemViewModel>(); // Строки заказа
        }
        // ViewModel для строки заказа поставщику
        public class PurchaseOrderItemViewModel
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; } // Цена за единицу
            public decimal Total { get; set; }   // Сумма по строке (Количество * Цена)
        }
        // ViewModel для заказов КЛИЕНТАМ
        public class CustomerOrderViewModel
        {
            public int OrderId { get; set; }
            public string OrderNumber { get; set; }
            public DateTime OrderDate { get; set; }
            public string CustomerName { get; set; }  // Имя клиента
            public int CustomerId { get; set; } //Для связки
            public decimal TotalAmount { get; set; }
            public List<CustomerOrderItemViewModel> Items { get; set; } = new List<CustomerOrderItemViewModel>();
        }

        // ViewModel для строки заказа КЛИЕНТА
        public class CustomerOrderItemViewModel
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Total { get; set; }
        }
    }
}