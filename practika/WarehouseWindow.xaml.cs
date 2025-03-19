using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace practika
{
    public partial class WarehouseWindow : Window
    {
        private string _connectionString = "Data Source=DESKTOP-UGJSU56\\SQLEXPRESS01;Initial Catalog=SkladUchet;Integrated Security=True;TrustServerCertificate=True";
        private int? _warehouseId; // Nullable int

        // Конструктор для ДОБАВЛЕНИЯ
        public WarehouseWindow()
        {
            InitializeComponent();
            LoadWarehouseTypes();
        }

        // Конструктор для РЕДАКТИРОВАНИЯ
        public WarehouseWindow(int warehouseId)
        {
            InitializeComponent();
            _warehouseId = warehouseId;
            LoadWarehouseTypes();
            LoadWarehouseData();
        }

        private void LoadWarehouseTypes()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT Warehouse_Type_ID, Название_Типа FROM ТипыСкладов";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<WarehouseType> types = new List<WarehouseType>();
                            while (reader.Read())
                            {
                                types.Add(new WarehouseType { Warehouse_Type_ID = (int)reader["Warehouse_Type_ID"], Название_Типа = (string)reader["Название_Типа"] });
                            }
                            cmbWarehouseType.ItemsSource = types;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке типов складов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }


        private void LoadWarehouseData()
        {
            if (_warehouseId == null) return;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT Название_Склада, Адрес, Warehouse_Type_ID FROM Склады WHERE Warehouse_ID = @WarehouseId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@WarehouseId", _warehouseId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtWarehouseName.Text = (string)reader["Название_Склада"];
                                txtAddress.Text = (string)reader["Адрес"];
                                cmbWarehouseType.SelectedValue = (int)reader["Warehouse_Type_ID"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных склада: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWarehouseName.Text) || string.IsNullOrWhiteSpace(txtAddress.Text) || cmbWarehouseType.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query;

                    if (_warehouseId == null) // ДОБАВЛЕНИЕ
                    {
                        query = "INSERT INTO Склады (Название_Склада, Адрес, Warehouse_Type_ID) VALUES (@Name, @Address, @TypeId)";
                    }
                    else // РЕДАКТИРОВАНИЕ
                    {
                        query = "UPDATE Склады SET Название_Склада = @Name, Адрес = @Address, Warehouse_Type_ID = @TypeId WHERE Warehouse_ID = @WarehouseId";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", txtWarehouseName.Text);
                        command.Parameters.AddWithValue("@Address", txtAddress.Text);
                        command.Parameters.AddWithValue("@TypeId", ((WarehouseType)cmbWarehouseType.SelectedItem).Warehouse_Type_ID);

                        if (_warehouseId != null)
                        {
                            command.Parameters.AddWithValue("@WarehouseId", _warehouseId);
                        }

                        command.ExecuteNonQuery();
                    }
                }

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении склада: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }

    // Вспомогательный класс для ComboBox
    public class WarehouseType
    {
        public int Warehouse_Type_ID { get; set; }
        public string Название_Типа { get; set; }
    }
}