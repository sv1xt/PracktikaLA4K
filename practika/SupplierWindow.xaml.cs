using System;
using System.Data.SqlClient;
using System.Windows;

namespace practika
{
    public partial class SupplierWindow : Window
    {
        private string _connectionString = "Data Source=DESKTOP-UGJSU56\\SQLEXPRESS01;Initial Catalog=SkladUchet;Integrated Security=True;TrustServerCertificate=True";
        private int? _supplierId; // Nullable int

        public SupplierWindow()
        {
            InitializeComponent();
        }

        public SupplierWindow(int supplierId)
        {
            InitializeComponent();
            _supplierId = supplierId;
            LoadSupplierData();
        }

        private void LoadSupplierData()
        {
            if (_supplierId == null) return;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT Название_Поставщика, ИНН, КПП, Контактный_Телефон, Контактный_Email, Адрес FROM Поставщики WHERE Supplier_ID = @SupplierId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SupplierId", _supplierId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtSupplierName.Text = (string)reader["Название_Поставщика"];
                                txtINN.Text = reader["ИНН"] != DBNull.Value ? (string)reader["ИНН"] : null;
                                txtKPP.Text = reader["КПП"] != DBNull.Value ? (string)reader["КПП"] : null;
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
                MessageBox.Show($"Ошибка при загрузке данных поставщика: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text) || string.IsNullOrWhiteSpace(txtContactPhone.Text)
               || string.IsNullOrWhiteSpace(txtContactEmail.Text) || string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Пожалуйста заполните обязательные поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query;

                    if (_supplierId == null) // ДОБАВЛЕНИЕ
                    {
                        query = "INSERT INTO Поставщики (Название_Поставщика, ИНН, КПП, Контактный_Телефон, Контактный_Email, Адрес) VALUES (@Name, @INN, @KPP, @Phone, @Email, @Address)";
                    }
                    else // РЕДАКТИРОВАНИЕ
                    {
                        query = "UPDATE Поставщики SET Название_Поставщика = @Name, ИНН = @INN, КПП = @KPP, Контактный_Телефон = @Phone, Контактный_Email = @Email, Адрес = @Address WHERE Supplier_ID = @SupplierId";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", txtSupplierName.Text);
                        command.Parameters.AddWithValue("@INN", !string.IsNullOrEmpty(txtINN.Text) ? txtINN.Text : (object)DBNull.Value);
                        command.Parameters.AddWithValue("@KPP", !string.IsNullOrEmpty(txtKPP.Text) ? txtKPP.Text : (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", txtContactPhone.Text);
                        command.Parameters.AddWithValue("@Email", txtContactEmail.Text);
                        command.Parameters.AddWithValue("@Address", txtAddress.Text);

                        if (_supplierId != null)
                        {
                            command.Parameters.AddWithValue("@SupplierId", _supplierId);
                        }

                        command.ExecuteNonQuery();
                    }
                }

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении поставщика: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}