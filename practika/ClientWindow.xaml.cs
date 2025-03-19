using System;
using System.Data.SqlClient;
using System.Windows;

namespace practika
{
    public partial class ClientWindow : Window
    {
        private string _connectionString = "Data Source=DESKTOP-UGJSU56\\SQLEXPRESS01;Initial Catalog=SkladUchet;Integrated Security=True;TrustServerCertificate=True";
        private int? _clientId; // Nullable int

        public ClientWindow()
        {
            InitializeComponent();
        }

        public ClientWindow(int clientId)
        {
            InitializeComponent();
            _clientId = clientId;
            LoadClientData();
        }

        private void LoadClientData()
        {
            if (_clientId == null) return;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT Название_Клиента, Контактный_Телефон, Контактный_Email, Адрес FROM Клиенты WHERE Customer_ID = @ClientId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClientId", _clientId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtClientName.Text = (string)reader["Название_Клиента"];
                                txtContactPhone.Text = (string)reader["Контактный_Телефон"];
                                txtContactEmail.Text = (string)reader["Контактный_Email"];
                                txtAddress.Text = (string)reader["Адрес"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClientName.Text) || string.IsNullOrWhiteSpace(txtContactPhone.Text)
                  || string.IsNullOrWhiteSpace(txtContactEmail.Text) || string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Пожалуйста заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query;

                    if (_clientId == null) // ДОБАВЛЕНИЕ
                    {
                        query = "INSERT INTO Клиенты (Название_Клиента, Контактный_Телефон, Контактный_Email, Адрес) VALUES (@Name, @Phone, @Email, @Address)";
                    }
                    else // РЕДАКТИРОВАНИЕ
                    {
                        query = "UPDATE Клиенты SET Название_Клиента = @Name, Контактный_Телефон = @Phone, Контактный_Email = @Email, Адрес = @Address WHERE Customer_ID = @ClientId";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", txtClientName.Text);
                        command.Parameters.AddWithValue("@Phone", txtContactPhone.Text);
                        command.Parameters.AddWithValue("@Email", txtContactEmail.Text);
                        command.Parameters.AddWithValue("@Address", txtAddress.Text);

                        if (_clientId != null)
                        {
                            command.Parameters.AddWithValue("@ClientId", _clientId);
                        }

                        command.ExecuteNonQuery();
                    }
                }

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}