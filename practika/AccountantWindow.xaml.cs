using System;
using System.Collections.Generic;
using System.Data; // Для DataTable
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls; // Для TabControl и SelectionChangedEventArgs

namespace practika
{
    public partial class AccountantWindow : Window
    {
        private int _userId;
        private string _connectionString = "Data Source=DESKTOP-UGJSU56\\SQLEXPRESS01;Initial Catalog=SkladUchet;Integrated Security=True;TrustServerCertificate=True";
        private int? _currentInventoryId; // ID текущей инвентаризации (Nullable)

        public AccountantWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            Loaded += AccountantWindow_Loaded; // Подписываемся на событие Loaded
        }

        private void AccountantWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //При загрузке окна, сразу получаем ID инвентаризации
            _currentInventoryId = GetCurrentInventoryId();
            // Если есть активная инвентаризация, сразу загружаем ее
            if (_currentInventoryId.HasValue)
            {
                LoadInventoryReport(_currentInventoryId.Value);
            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl tabControl)
            {
                if (tabControl.SelectedItem == tabReportWarehouse)
                {
                    LoadWarehouseReport();
                }
                else if (tabControl.SelectedItem == tabReportTurnover)
                {
                    LoadTurnoverReport();
                }
                else if (tabControl.SelectedItem == tabReportStock)
                {
                    LoadStockReport();
                }
            }
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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

        private void menuInventory_Click(object sender, RoutedEventArgs e)
        {
            tabInventory.Visibility = Visibility.Visible;
            tabInventory.IsSelected = true;
            if (_currentInventoryId.HasValue)  //Загружаем если есть
            {
                LoadInventoryReport(_currentInventoryId.Value);
            }
        }

        //-------Методы отчётов (реализация)---------
        private void LoadWarehouseReport()
        {
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
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = @"
        SELECT
            COALESCE(пн.Дата_Накладной, рн.Дата_Накладной) AS Дата,
            CASE
                WHEN пн.Invoice_ID IS NOT NULL THEN 'Приход'
                WHEN рн.Invoice_ID IS NOT NULL THEN 'Расход'
                ELSE 'Неизвестно'
            END AS Тип_Операции,
            COALESCE(пост.Название_Поставщика, кл.Название_Клиента) AS Контрагент,
            SUM(COALESCE(стрпн.Количество * стрпн.Цена, стррн.Количество * стррн.Цена)) AS Сумма
        FROM
            ПриходныеНакладные пн  -- Исправлено!
        FULL OUTER JOIN
            РасходныеНакладные рн ON пн.Дата_Накладной = рн.Дата_Накладной  -- Исправлено!
        LEFT JOIN
            Поставщики пост ON пн.Supplier_ID = пост.Supplier_ID
        LEFT JOIN
            Клиенты кл ON рн.Customer_ID = кл.Customer_ID
        LEFT JOIN
            СтрокиПриходнойНакладной стрпн ON пн.Invoice_ID = стрпн.Invoice_ID  -- Исправлено!
        LEFT JOIN
            СтрокиРасходнойНакладной стррн ON рн.Invoice_ID = стррн.Invoice_ID  -- Исправлено!
        GROUP BY
            COALESCE(пн.Дата_Накладной, рн.Дата_Накладной),
            CASE
                WHEN пн.Invoice_ID IS NOT NULL THEN 'Приход'
                WHEN рн.Invoice_ID IS NOT NULL THEN 'Расход'
                ELSE 'Неизвестно'
            END,
            COALESCE(пост.Название_Поставщика, кл.Название_Клиента)
        ORDER BY
            Дата;";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dgReportTurnover.ItemsSource = dataTable.DefaultView;
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
                            dgInventory.ItemsSource = inventoryItems; //Привязываем
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузки инвенторизации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Вспомогательный метод для получения ID текущей инвентаризации
        private int? GetCurrentInventoryId()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT TOP 1 Inventory_ID FROM Инвентаризации ORDER BY Дата_Инвентаризации DESC";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        return result != DBNull.Value ? (int?)result : null; // Возвращаем null, если нет активной инвентаризации
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении ID инвентаризации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }


        private void btnStartInventory_Click(object sender, RoutedEventArgs e)
        {
            // 1. Создаем запись в таблице Инвентаризации
            int inventoryId;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string startInventoryQuery = "INSERT INTO Инвентаризации (Дата_Инвентаризации, User_ID) OUTPUT INSERTED.Inventory_ID VALUES (@InventoryDate, @UserId);";
                    using (SqlCommand command = new SqlCommand(startInventoryQuery, connection))
                    {
                        command.Parameters.AddWithValue("@InventoryDate", DateTime.Now); // Текущая дата
                        command.Parameters.AddWithValue("@UserId", _userId); // ID текущего пользователя
                        inventoryId = (int)command.ExecuteScalar(); // Получаем ID новой записи
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при начале инвентаризации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 2. Копируем учетные остатки в СтрокиИнвентаризации
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string copyStockQuery = @"
                        INSERT INTO СтрокиИнвентаризации (Inventory_ID, Product_ID, Учетное_Количество, Фактическое_Количество)
                        SELECT @InventoryId, wp.Product_ID, wp.Количество, 0  -- Фактическое количество изначально 0
                        FROM ТоварыНаСкладе wp;";
                    using (SqlCommand command = new SqlCommand(copyStockQuery, connection))
                    {
                        command.Parameters.AddWithValue("@InventoryId", inventoryId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при копировании остатков: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 3. Загружаем данные для отображения
            LoadInventoryReport(inventoryId);
            _currentInventoryId = inventoryId;  //Сохраняем
        }

        private void btnCompleteInventory_Click(object sender, RoutedEventArgs e)
        {
            // 1. Находим текущую активную инвентаризацию
            //int? inventoryId = GetCurrentInventoryId();  // Уже сохранено в _currentInventoryId
            if (_currentInventoryId == null)
            {
                MessageBox.Show("Нет активной инвентаризации.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // 2. Сохраняем фактические количества и пересчитываем разницу
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string updateQuery = @"
                UPDATE СтрокиИнвентаризации
                SET Фактическое_Количество = @ActualQuantity,
                    Разница = @ActualQuantity - Учетное_Количество
                WHERE Inventory_ID = @InventoryId AND Product_ID = @ProductId";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.Add("@InventoryId", SqlDbType.Int);
                        command.Parameters.Add("@ProductId", SqlDbType.Int);
                        command.Parameters.Add("@ActualQuantity", SqlDbType.Int);


                        //Перебираем DataGrid, и обновляем данные в БД
                        foreach (InventoryItemViewModel item in dgInventory.Items)  // Исправлено!
                        {
                            //Валидация данных на уровне кода
                            if (item.ActualQuantity < 0)
                            {
                                MessageBox.Show($"Фактическое количество не может быть меньше нуля. Товар: {item.ProductName}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            command.Parameters["@InventoryId"].Value = _currentInventoryId.Value;
                            command.Parameters["@ProductId"].Value = item.ProductId;
                            command.Parameters["@ActualQuantity"].Value = item.ActualQuantity;
                            command.ExecuteNonQuery();
                        }
                    }
                }
                // После сохранения обновляем отображение
                LoadInventoryReport(_currentInventoryId.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при завершении инвентаризации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _currentInventoryId = null; //Сбрасываем
            //TODO:  Реализовать Обновление остатков(сделать потом)
        }
        // ViewModel для строки инвентаризации
        public class InventoryItemViewModel
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int BookQuantity { get; set; } // Учетное количество
            public int ActualQuantity { get; set; } // Фактическое количество, теперь можно редактировать
            public int Difference { get; set; }     // Разница
        }
    }
}