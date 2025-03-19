using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace practika
{
    public partial class ProductWindow : Window
    {
        private string _connectionString = "Data Source=DESKTOP-UGJSU56\\SQLEXPRESS01;Initial Catalog=SkladUchet;Integrated Security=True;TrustServerCertificate=True";
        private int? _productId; // Nullable int

        public ProductWindow()
        {
            InitializeComponent();
            LoadCategories();
            LoadUnits();
        }

        public ProductWindow(int productId)
        {
            InitializeComponent();
            _productId = productId;
            LoadCategories();
            LoadUnits();
            LoadProductData();
        }

        private void LoadCategories()
        {
            // Загрузка категорий в ComboBox
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT Category_ID, Название_Категории FROM КатегорииТоваров";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Category> categories = new List<Category>();
                            while (reader.Read())
                            {
                                categories.Add(new Category { Category_ID = (int)reader["Category_ID"], Название_Категории = (string)reader["Название_Категории"] });
                            }
                            cmbCategory.ItemsSource = categories;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке категорий: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }

        private void LoadUnits()
        {
            // Загрузка единиц измерения в ComboBox
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT Unit_ID, Название_Единицы_Измерения FROM ЕдиницыИзмерения";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Unit> units = new List<Unit>();
                            while (reader.Read())
                            {
                                units.Add(new Unit { Unit_ID = (int)reader["Unit_ID"], Название_Единицы_Измерения = (string)reader["Название_Единицы_Измерения"] });
                            }
                            cmbUnit.ItemsSource = units;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке единиц измерения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }

        private void LoadProductData()
        {
            if (_productId == null) return;

            // Загрузка данных товара для редактирования
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT Название_Товара, Артикул, Штрихкод, Category_ID, Unit_ID, Цена, Серийный_Номер, Минимальный_Остаток, Срок_Годности FROM Товары WHERE Product_ID = @ProductId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", _productId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtProductName.Text = (string)reader["Название_Товара"];
                                txtArticle.Text = (string)reader["Артикул"];
                                txtBarcode.Text = reader["Штрихкод"] != DBNull.Value ? (string)reader["Штрихкод"] : null;
                                cmbCategory.SelectedValue = (int)reader["Category_ID"];
                                cmbUnit.SelectedValue = (int)reader["Unit_ID"];
                                txtPrice.Text = ((decimal)reader["Цена"]).ToString();
                                txtSerialNumber.Text = reader["Серийный_Номер"] != DBNull.Value ? (string)reader["Серийный_Номер"] : null;
                                txtMinStockLevel.Text = reader["Минимальный_Остаток"] != DBNull.Value ? reader["Минимальный_Остаток"].ToString() : null;
                                dpExpiryDate.SelectedDate = reader["Срок_Годности"] != DBNull.Value ? (DateTime?)reader["Срок_Годности"] : null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(txtProductName.Text) ||
                string.IsNullOrWhiteSpace(txtArticle.Text) ||
                cmbCategory.SelectedItem == null ||
                cmbUnit.SelectedItem == null ||
                !decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля и проверьте правильность введенных данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка уникальности артикула
            if (IsArticleTaken(txtArticle.Text))
            {
                MessageBox.Show("Товар с таким артикулом уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Проверка уникальности штрихкода
            if (!string.IsNullOrEmpty(txtBarcode.Text))
            {
                if (IsBarcodeTaken(txtBarcode.Text))
                {
                    MessageBox.Show("Товар с таким штрихкодом уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }


            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query;

                    if (_productId == null) // ДОБАВЛЕНИЕ
                    {
                        query = @"INSERT INTO Товары (Название_Товара, Артикул, Штрихкод, Category_ID, Unit_ID, Цена, Серийный_Номер, Минимальный_Остаток, Срок_Годности)
                              VALUES (@ProductName, @Article, @Barcode, @CategoryId, @UnitId, @Price, @SerialNumber, @MinStockLevel, @ExpiryDate)";
                    }
                    else // РЕДАКТИРОВАНИЕ
                    {
                        query = @"UPDATE Товары SET Название_Товара = @ProductName, Артикул = @Article, Штрихкод = @Barcode, Category_ID = @CategoryId,
                              Unit_ID = @UnitId, Цена = @Price, Серийный_Номер = @SerialNumber, Минимальный_Остаток = @MinStockLevel, Срок_Годности = @ExpiryDate
                              WHERE Product_ID = @ProductId";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                        command.Parameters.AddWithValue("@Article", txtArticle.Text);
                        command.Parameters.AddWithValue("@Barcode", !string.IsNullOrEmpty(txtBarcode.Text) ? txtBarcode.Text : (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CategoryId", ((Category)cmbCategory.SelectedItem).Category_ID);
                        command.Parameters.AddWithValue("@UnitId", ((Unit)cmbUnit.SelectedItem).Unit_ID);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@SerialNumber", !string.IsNullOrEmpty(txtSerialNumber.Text) ? txtSerialNumber.Text : (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MinStockLevel", !string.IsNullOrEmpty(txtMinStockLevel.Text) ? int.Parse(txtMinStockLevel.Text) : (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ExpiryDate", dpExpiryDate.SelectedDate != null ? dpExpiryDate.SelectedDate : (object)DBNull.Value);

                        if (_productId != null)
                        {
                            command.Parameters.AddWithValue("@ProductId", _productId);
                        }

                        command.ExecuteNonQuery();
                    }
                }

                DialogResult = true; // Успех
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }
        private bool IsArticleTaken(string article)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Товары WHERE Артикул = @Article";
                if (_productId.HasValue) // Если это редактирование, исключаем текущий товар
                {
                    query += " AND Product_ID <> @ProductId";
                }
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Article", article);
                    if (_productId.HasValue)
                    {
                        command.Parameters.AddWithValue("@ProductId", _productId.Value);
                    }
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }

        }
        private bool IsBarcodeTaken(string barcode)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Товары WHERE Штрихкод = @Barcode";
                if (_productId.HasValue)  // Если это редактирование, исключаем текущий товар
                {
                    query += " AND Product_ID <> @ProductId";
                }
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Barcode", barcode);
                    if (_productId.HasValue)
                    {
                        command.Parameters.AddWithValue("@ProductId", _productId.Value);
                    }
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        // Вспомогательные классы для ComboBox'ов
        public class Category
        {
            public int Category_ID { get; set; }
            public string Название_Категории { get; set; }
        }

        public class Unit
        {
            public int Unit_ID { get; set; }
            public string Название_Единицы_Измерения { get; set; }
        }
    }
}