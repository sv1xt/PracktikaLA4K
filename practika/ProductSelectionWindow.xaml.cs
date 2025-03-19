using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using static practika.MainWindow;

namespace practika
{
    public partial class ProductSelectionWindow : Window
    {
        private string _connectionString = "Data Source=DESKTOP-UGJSU56\\SQLEXPRESS01;Initial Catalog=SkladUchet;Integrated Security=True;TrustServerCertificate=True";

        public ProductViewModel SelectedProduct { get; private set; } // ViewModel вместо Product

        public ProductSelectionWindow()
        {
            InitializeComponent();
            LoadProducts();
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
                            List<ProductViewModel> products = new List<ProductViewModel>(); // Используем ProductViewModel
                            while (reader.Read())
                            {
                                products.Add(new ProductViewModel // Используем ProductViewModel
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


        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem != null)
            {
                SelectedProduct = (ProductViewModel)dgProducts.SelectedItem; // Сохраняем *ViewModel*
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите товар.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Закрываем окно без выбора
        }
    }
}