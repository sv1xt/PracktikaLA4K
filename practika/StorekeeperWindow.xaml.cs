using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace practika
{
    public partial class StorekeeperWindow : Window
    {
        private int _userId;
        private string _connectionString = "Data Source=DESKTOP-UGJSU56\\SQLEXPRESS01;Initial Catalog=SkladUchet;Integrated Security=True;TrustServerCertificate=True";

        public StorekeeperWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            Loaded += StorekeeperWindow_Loaded;
        }

        private void StorekeeperWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadIncomingInvoices();
            LoadOutgoingInvoices();
            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void menuIncomingInvoices_Click(object sender, RoutedEventArgs e)
        {
            tabIncomingInvoices.Visibility = Visibility.Visible;
            tabIncomingInvoices.IsSelected = true;
            LoadIncomingInvoices();
        }

        private void btnAddIncomingInvoice_Click(object sender, RoutedEventArgs e)
        {
            IncomingInvoiceWindow incomingInvoiceWindow = new IncomingInvoiceWindow();
            if (incomingInvoiceWindow.ShowDialog() == true)
            {
                LoadIncomingInvoices(); // Обновляем список приходных накладных
            }
        }

        private void btnEditIncomingInvoice_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Открыть окно редактирования приходной накладной (IncomingInvoiceWindow)
            if (dgIncomingInvoices.SelectedItem != null)
            {
                IncomingInvoiceViewModel selectedInvoice = (IncomingInvoiceViewModel)dgIncomingInvoices.SelectedItem;
                IncomingInvoiceWindow incomingInvoiceWindow = new IncomingInvoiceWindow(selectedInvoice.InvoiceId);
                if (incomingInvoiceWindow.ShowDialog() == true)
                {
                    LoadIncomingInvoices();
                }
            }
            else
            {
                MessageBox.Show("Выберите накладную для редактирования.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDeleteIncomingInvoice_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Удалить приходную накладную (с подтверждением)
            if (dgIncomingInvoices.SelectedItem != null)
            {
                IncomingInvoiceViewModel selectedInvoice = (IncomingInvoiceViewModel)dgIncomingInvoices.SelectedItem;

                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить накладную №{selectedInvoice.InvoiceNumber}?", "Удаление накладной", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(_connectionString))
                        {
                            connection.Open();
                            //Сначала удаляем строки
                            string deleteItemsQuery = "DELETE FROM СтрокиПриходнойНакладной WHERE Invoice_ID = @InvoiceId";
                            using (SqlCommand deleteItemsCommand = new SqlCommand(deleteItemsQuery, connection))
                            {
                                deleteItemsCommand.Parameters.AddWithValue("@InvoiceId", selectedInvoice.InvoiceId);
                                deleteItemsCommand.ExecuteNonQuery();
                            }
                            // Потом удаляем саму накладную
                            string query = "DELETE FROM ПриходныеНакладные WHERE Invoice_ID = @InvoiceId";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@InvoiceId", selectedInvoice.InvoiceId);
                                command.ExecuteNonQuery();
                            }
                        }
                        LoadIncomingInvoices(); // Обновляем список
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении накладной: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите накладную для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
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
                            dgIncomingInvoices.ItemsSource = invoices;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузки приходных накладных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // ViewModel для приходных накладных (можно разместить в отдельном файле, если хотите)


        // Добавляем ПУСТЫЕ обработчики для меню и кнопок, которых не было
        private void menuOutgoingInvoices_Click(object sender, RoutedEventArgs e)
        {
            tabOutgoingInvoices.Visibility = Visibility.Visible;
            tabOutgoingInvoices.IsSelected = true;
            LoadOutgoingInvoices();
        }

        private void InventoryReport_Click(object sender, RoutedEventArgs e)
        {
            tabReportInventory.Visibility = Visibility.Visible; //Показываем соответсвующую вкладку
            tabReportInventory.IsSelected = true;
            // TODO: Загрузить отчет по инвентаризации (LoadInventoryReport)   Реализовать!!
        }

        private void btnAddOutgoingInvoice_Click(object sender, RoutedEventArgs e)
        {
            OutgoingInvoiceWindow outgoingInvoiceWindow = new OutgoingInvoiceWindow();
            if (outgoingInvoiceWindow.ShowDialog() == true)
            {
                LoadOutgoingInvoices(); // Обновляем список
            }
        }

        private void btnEditOutgoingInvoice_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Открыть окно редактирования расходной накладной (OutgoingInvoiceWindow) РЕАЛИЗОВАТЬ!
            if (dgOutgoingInvoices.SelectedItem != null)
            {
                OutgoingInvoiceViewModel selectedInvoice = (OutgoingInvoiceViewModel)dgOutgoingInvoices.SelectedItem;
                OutgoingInvoiceWindow outgoingInvoiceWindow = new OutgoingInvoiceWindow(selectedInvoice.InvoiceId); // Открываем для РЕДАКТИРОВАНИЯ
                if (outgoingInvoiceWindow.ShowDialog() == true)
                {
                    LoadOutgoingInvoices();
                }
            }
            else
            {
                MessageBox.Show("Выберите накладную для редактирования.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDeleteOutgoingInvoice_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Удалить расходную накладную (с подтверждением и обновлением остатков) РЕАЛИЗОВАТЬ!
            if (dgOutgoingInvoices.SelectedItem != null)
            {
                OutgoingInvoiceViewModel selectedInvoice = (OutgoingInvoiceViewModel)dgOutgoingInvoices.SelectedItem;

                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить накладную №{selectedInvoice.InvoiceNumber}?", "Удаление накладной", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(_connectionString))
                        {
                            connection.Open();

                            // Сначала удаляем строки
                            string deleteItemsQuery = "DELETE FROM СтрокиРасходнойНакладной WHERE Invoice_ID = @InvoiceId";
                            using (SqlCommand deleteItemsCommand = new SqlCommand(deleteItemsQuery, connection))
                            {
                                deleteItemsCommand.Parameters.AddWithValue("@InvoiceId", selectedInvoice.InvoiceId);
                                deleteItemsCommand.ExecuteNonQuery();
                            }

                            // Потом удаляем саму накладную
                            string deleteInvoiceQuery = "DELETE FROM РасходныеНакладные WHERE Invoice_ID = @InvoiceId";
                            using (SqlCommand deleteInvoiceCommand = new SqlCommand(deleteInvoiceQuery, connection))
                            {
                                deleteInvoiceCommand.Parameters.AddWithValue("@InvoiceId", selectedInvoice.InvoiceId);
                                deleteInvoiceCommand.ExecuteNonQuery();
                            }
                        }

                        LoadOutgoingInvoices(); // Обновляем список
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении накладной: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите накладную для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
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
                            dgOutgoingInvoices.ItemsSource = invoices;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке расходных накладных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
        private void inventoryReport_Click(object sender, RoutedEventArgs e)
        {
            tabReportInventory.Visibility = Visibility.Visible;
            tabReportInventory.IsSelected = true;
            //LoadInventoryReport(); //Убираем пока что
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
        }

//Метод загрузки данных
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
                        ii.Inventory_ID = @InventoryId;";

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
          catch(Exception ex)
          {
            MessageBox.Show($"Ошибка при загрузки инвенторизации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
          }
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

        private void btnCompleteInventory_Click(object sender, RoutedEventArgs e)
        {
            // 1. Находим текущую активную инвентаризацию
            int? inventoryId = GetCurrentInventoryId();
            if (inventoryId == null)
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
                        foreach (InventoryItemViewModel item in dgReportInventory.Items)
                        {
                            command.Parameters["@InventoryId"].Value = inventoryId.Value;
                            command.Parameters["@ProductId"].Value = item.ProductId;
                            command.Parameters["@ActualQuantity"].Value = item.ActualQuantity;
                            command.ExecuteNonQuery();
                        }
                    }
                }
                // После сохранения обновляем отображение
                LoadInventoryReport(inventoryId.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при завершении инвентаризации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //TODO:  Реализовать Обновление остатков(сделать потом)
        }

        // Вспомогательный метод для получения ID текущей инвентаризации
        private int? GetCurrentInventoryId()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    //  Предполагаем, что инвентаризация, начатая последней, является текущей.
                    //  В реальной системе, возможно, понадобится добавить столбец "Статус" в таблицу "Инвентаризации"
                    string query = "SELECT TOP 1 Inventory_ID FROM Инвентаризации ORDER BY Дата_Инвентаризации DESC";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        return result != DBNull.Value ? (int?)result : null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении ID инвентаризации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; // Возвращаем null в случае ошибки
            }
        }


        private void menuGenerateBarcode_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Сделать генерацию штрихкода РЕАЛИЗОВАТЬ!
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
    }
}