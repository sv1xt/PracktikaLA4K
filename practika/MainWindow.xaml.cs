using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace practika
{
    public partial class MainWindow : Window
    {
        private int _userId;
        private string _connectionString = "Data Source=DESKTOP-UGJSU56\\SQLEXPRESS01;Initial Catalog=SkladUchet;Integrated Security=True;TrustServerCertificate=True";

        public MainWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Загружаем данные для всех вкладок
            LoadUsers();
            LoadWarehouses();
            LoadProducts();
            LoadClients();
            LoadSuppliers();
        }

        private void LoadUsers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT u.User_ID, u.Имя_Пользователя, u.Электронная_Почта, r.Название_Роли " +
                                   "FROM Пользователи u " +
                                   "JOIN Роли r ON u.Role_ID = r.Role_ID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<UserViewModel> users = new List<UserViewModel>();
                            while (reader.Read())
                            {
                                users.Add(new UserViewModel
                                {
                                    UserId = (int)reader["User_ID"],
                                    Username = (string)reader["Имя_Пользователя"],
                                    Email = (string)reader["Электронная_Почта"],
                                    RoleName = (string)reader["Название_Роли"]
                                });
                            }
                            dgUsers.ItemsSource = users;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке пользователей: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                            dgWarehouses.ItemsSource = warehouses; // Привязываем данные к DataGrid
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
                            dgProducts.ItemsSource = products;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке товаров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                            dgClients.ItemsSource = clients;
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
                            dgSuppliers.ItemsSource = suppliers;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке поставщиков: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadWarehouseReport()
        {
            // Реализовать загрузку данных для отчета по складам
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT
                    w.Название_Склада,
                    wt.Название_Типа AS Тип_Склада,
                    COUNT(DISTINCT p.Product_ID) AS Количество_Товаров,
                    SUM(wp.Количество) AS Общее_Количество,
                    SUM(wp.Количество * p.Цена) AS Общая_Стоимость
                FROM
                    Склады w
                JOIN
                    ТипыСкладов wt ON w.Warehouse_Type_ID = wt.Warehouse_Type_ID
                LEFT JOIN
                    ТоварыНаСкладе wp ON w.Warehouse_ID = wp.Warehouse_ID
                LEFT JOIN
                    Товары p ON wp.Product_ID = p.Product_ID
                GROUP BY
                    w.Название_Склада, wt.Название_Типа
                ORDER BY
                    w.Название_Склада;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dgReportWarehouse.ItemsSource = dataTable.DefaultView; // Используем DataTable
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке отчета по складам: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadTurnoverReport()
        {
            //  Реализовать загрузку данных для отчета по оборотам
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
              SELECT
                  i.InvoiceDate AS Дата,
                  CASE
                      WHEN i.SupplierID IS NOT NULL THEN 'Приход'
                      WHEN i.CustomerID IS NOT NULL THEN 'Расход'
                      ELSE 'Неизвестно'
                  END AS Тип_Операции,
                  COALESCE(s.SupplierName, c.CustomerName) AS Контрагент,
                  SUM(ii.Quantity * ii.Price) AS Сумма
              FROM
                  IncomingInvoices i
              LEFT JOIN
                  Suppliers s ON i.SupplierID = s.SupplierID
              LEFT JOIN
                  Customers c ON i.CustomerID = c.CustomerID
              LEFT JOIN
                  IncomingInvoiceItems ii ON i.InvoiceID = ii.InvoiceID
              GROUP BY
                  i.InvoiceDate,
                  CASE
                      WHEN i.SupplierID IS NOT NULL THEN 'Приход'
                      WHEN i.CustomerID IS NOT NULL THEN 'Расход'
                      ELSE 'Неизвестно'
                  END,
                  COALESCE(s.SupplierName, c.CustomerName)
              UNION ALL
              SELECT
                  o.InvoiceDate AS Дата,
                  CASE
                    WHEN o.SupplierID IS NOT NULL THEN 'Приход'
                    WHEN o.CustomerID IS NOT NULL THEN 'Расход'
                    ELSE 'Неизвестно'
                  END AS Тип_Операции,
                  COALESCE(s.SupplierName, c.CustomerName) AS Контрагент,
                  SUM(oi.Quantity * oi.Price) AS Сумма
              FROM
                  OutgoingInvoices o
              LEFT JOIN
                  Customers c ON o.CustomerID = c.CustomerID
              LEFT JOIN
                 Suppliers s on o.SupplierID = s.SupplierID
              LEFT JOIN
                  OutgoingInvoiceItems oi ON o.InvoiceID = oi.InvoiceID

              GROUP BY
                  o.InvoiceDate,
                CASE
                    WHEN o.SupplierID IS NOT NULL THEN 'Приход'
                    WHEN o.CustomerID IS NOT NULL THEN 'Расход'
                    ELSE 'Неизвестно'
                  END,
                  COALESCE(s.SupplierName, c.CustomerName)
              ORDER BY
                  Дата;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dgReportTurnover.ItemsSource = dataTable.DefaultView; // Используем DataTable
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке отчёта по оборотам: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadStockReport()
        {
            //Реализовать загрузку данных для отчета по остаткам
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
             SELECT
                p.Название_Товара,
                p.Артикул,
                c.Название_Категории,
                u.Название_Единицы_Измерения,
                SUM(wp.Количество) AS Остаток
            FROM
                Товары p
            JOIN
                КатегорииТоваров c ON p.Category_ID = c.Category_ID
            JOIN
                ЕдиницыИзмерения u ON p.Unit_ID = u.Unit_ID
            LEFT JOIN
                ТоварыНаСкладе wp ON p.Product_ID = wp.Product_ID
            GROUP BY
                p.Название_Товара, p.Артикул, c.Название_Категории, u.Название_Единицы_Измерения
            ORDER BY
                p.Название_Товара;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dgReportStock.ItemsSource = dataTable.DefaultView; // Используем DataTable
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке отчёта по остаткам: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadInventoryReport(int inventoryId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
                    SELECT
                        ii.Product_ID,
                        p.Название_Товара,
                        ii.Учетное_Количество,
                        ii.Фактическое_Количество,
                        (ii.Фактическое_Количество - ii.Учетное_Количество) AS Разница
                    FROM
                        СтрокиИнвентаризации ii
                    JOIN
                        Товары p ON ii.Product_ID = p.Product_ID
                    WHERE
                        ii.Inventory_ID = @InventoryId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InventoryId", inventoryId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<InventoryItemViewModel> inventoryItems = new List<InventoryItemViewModel>();
                            while (reader.Read())
                            {
                                inventoryItems.Add(new InventoryItemViewModel
                                {
                                    ProductId = (int)reader["Product_ID"],
                                    ProductName = (string)reader["Название_Товара"],
                                    BookQuantity = (int)reader["Учетное_Количество"],
                                    ActualQuantity = (int)reader["Фактическое_Количество"],
                                    Difference = (int)reader["Разница"]
                                });
                            }
                            dgReportInventory.ItemsSource = inventoryItems;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузки инвенторизации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void menuUsers_Click(object sender, RoutedEventArgs e)
        {
            tabUsers.Visibility = Visibility.Visible;
            tabUsers.IsSelected = true;
        }

        private void menuWarehouses_Click(object sender, RoutedEventArgs e)
        {
            tabWarehouses.Visibility = Visibility.Visible;
            tabWarehouses.IsSelected = true;
        }

        private void menuProducts_Click(object sender, RoutedEventArgs e)
        {
            tabProducts.Visibility = Visibility.Visible;
            tabProducts.IsSelected = true;
            LoadProducts(); // Нужно загружать товары, когда вкладка становится активной
        }

        private void menuClients_Click(object sender, RoutedEventArgs e)
        {
            tabClients.Visibility = Visibility.Visible;
            tabClients.IsSelected = true;
            LoadClients(); // Загружаем данные при активации вкладки
        }

        private void menuSuppliers_Click(object sender, RoutedEventArgs e)
        {
            tabSuppliers.Visibility = Visibility.Visible;
            tabSuppliers.IsSelected = true;
            LoadSuppliers(); // Загружаем данные при активации вкладки
        }

        private void menuWarehouseReport_Click(object sender, RoutedEventArgs e)
        {
            tabReportWarehouse.Visibility = Visibility.Visible;
            tabReportWarehouse.IsSelected = true;
            LoadWarehouseReport();
        }

        private void menuTurnoverReport_Click(object sender, RoutedEventArgs e)
        {
            tabReportTurnover.Visibility = Visibility.Visible;
            tabReportTurnover.IsSelected = true;
            LoadTurnoverReport();
        }

        private void menuStockReport_Click(object sender, RoutedEventArgs e)
        {
            tabReportStock.Visibility = Visibility.Visible;
            tabReportStock.IsSelected = true;
            LoadStockReport();
        }
        private void inventoryReport_Click(object sender, RoutedEventArgs e)
        {
            tabReportInventory.Visibility = Visibility.Visible;
            tabReportInventory.IsSelected = true;
            //LoadInventoryReport();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = new UserWindow();
            if (userWindow.ShowDialog() == true)
            {
                LoadUsers();
            }
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem != null)
            {
                UserViewModel selectedUser = (UserViewModel)dgUsers.SelectedItem;
                UserWindow userWindow = new UserWindow(selectedUser.UserId);
                if (userWindow.ShowDialog() == true)
                {
                    LoadUsers();
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя для редактирования.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem != null)
            {
                UserViewModel selectedUser = (UserViewModel)dgUsers.SelectedItem;

                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить пользователя '{selectedUser.Username}'?", "Удаление пользователя", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(_connectionString))
                        {
                            connection.Open();
                            string query = "DELETE FROM Пользователи WHERE User_ID = @UserId";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@UserId", selectedUser.UserId);
                                command.ExecuteNonQuery();
                            }
                        }
                        LoadUsers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAddWarehouse_Click(object sender, RoutedEventArgs e)
        {
            WarehouseWindow warehouseWindow = new WarehouseWindow();
            if (warehouseWindow.ShowDialog() == true)
            {
                LoadWarehouses();
            }
        }

        private void btnEditWarehouse_Click(object sender, RoutedEventArgs e)
        {
            if (dgWarehouses.SelectedItem != null)
            {
                WarehouseViewModel selectedWarehouse = (WarehouseViewModel)dgWarehouses.SelectedItem;
                WarehouseWindow warehouseWindow = new WarehouseWindow(selectedWarehouse.WarehouseId);
                if (warehouseWindow.ShowDialog() == true)
                {
                    LoadWarehouses();
                }
            }
            else
            {
                MessageBox.Show("Выберите склад для редактирования.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDeleteWarehouse_Click(object sender, RoutedEventArgs e)
        {
            if (dgWarehouses.SelectedItem != null)
            {
                WarehouseViewModel selectedWarehouse = (WarehouseViewModel)dgWarehouses.SelectedItem;

                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить склад '{selectedWarehouse.WarehouseName}'?", "Удаление склада", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(_connectionString))
                        {
                            connection.Open();
                            string query = "DELETE FROM Склады WHERE Warehouse_ID = @WarehouseId";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@WarehouseId", selectedWarehouse.WarehouseId);
                                command.ExecuteNonQuery();
                            }
                        }
                        LoadWarehouses();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении склада: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите склад для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow();
            if (productWindow.ShowDialog() == true)
            {
                LoadProducts();
            }
        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem != null)
            {
                ProductViewModel selectedProduct = (ProductViewModel)dgProducts.SelectedItem;
                ProductWindow productWindow = new ProductWindow(selectedProduct.ProductId);
                if (productWindow.ShowDialog() == true)
                {
                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для редактирования.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem != null)
            {
                ProductViewModel selectedProduct = (ProductViewModel)dgProducts.SelectedItem;

                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить товар '{selectedProduct.ProductName}'?", "Удаление товара", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(_connectionString))
                        {
                            connection.Open();
                            string query = "DELETE FROM Товары WHERE Product_ID = @ProductId";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@ProductId", selectedProduct.ProductId);
                                command.ExecuteNonQuery();
                            }
                        }
                        LoadProducts();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void btnGenerateBarcode_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem != null)
            {
                ProductViewModel selectedProduct = (ProductViewModel)dgProducts.SelectedItem;
                // Создаем и показываем окно с штрих-кодом
                BarcodeWindow barcodeWindow = new BarcodeWindow(selectedProduct.Article); // Передаем артикул
                barcodeWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите товар для генерации штрихкода.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();
            if (clientWindow.ShowDialog() == true)
            {
                LoadClients();
            }
        }

        private void btnEditClient_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem != null)
            {
                ClientViewModel selectedClient = (ClientViewModel)dgClients.SelectedItem;
                ClientWindow clientWindow = new ClientWindow(selectedClient.CustomerId);
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
            if (dgClients.SelectedItem != null)
            {
                ClientViewModel selectedClient = (ClientViewModel)dgClients.SelectedItem;
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
        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            SupplierWindow supplierWindow = new SupplierWindow();
            if (supplierWindow.ShowDialog() == true)
            {
                LoadSuppliers();
            }
        }
        private void btnEditSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (dgSuppliers.SelectedItem != null)
            {
                SupplierViewModel selectedSupplier = (SupplierViewModel)dgSuppliers.SelectedItem;
                SupplierWindow supplierWindow = new SupplierWindow(selectedSupplier.SupplierId);
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
            if (dgSuppliers.SelectedItem != null)
            {
                SupplierViewModel selectedSupplier = (SupplierViewModel)dgSuppliers.SelectedItem;
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
        // Класс для представления данных пользователя в DataGrid
        public class UserViewModel
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string RoleName { get; set; }
            // Другие свойства, которые вы хотите отображать
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

    }
}