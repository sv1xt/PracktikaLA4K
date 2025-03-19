using System;
using System.Windows;
using System.Data.SqlClient; // Или NpgsqlConnection для PostgreSQL

namespace practika
{
    public partial class LoginWindow : Window
    {
        private string _connectionString = "Data Source=DESKTOP-UGJSU56\\SQLEXPRESS01;Initial Catalog=SkladUchet;Integrated Security=True;TrustServerCertificate=True"; // Замените на вашу строку подключения
        private int _currentUserId;
        private int _currentUserRoleId;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            lblErrorMessage.Visibility = Visibility.Collapsed;

            string username = txtUsername.Text;

            if (string.IsNullOrEmpty(username))
            {
                ShowErrorMessage("Введите имя пользователя.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString)) // Или NpgsqlConnection
                {
                    connection.Open();
                    // УПРОЩЕННЫЙ ЗАПРОС - выбираем только User_ID и Role_ID
                    string query = "SELECT User_ID, Role_ID FROM Пользователи WHERE Имя_Пользователя = @Username";
                    using (SqlCommand command = new SqlCommand(query, connection)) // Или NpgsqlCommand
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = command.ExecuteReader()) // Или NpgsqlDataReader
                        {
                            if (reader.Read())
                            {
                                _currentUserId = (int)reader["User_ID"];
                                _currentUserRoleId = (int)reader["Role_ID"];

                                // Успешный вход (ТОЛЬКО ПО ИМЕНИ ПОЛЬЗОВАТЕЛЯ!)
                                OpenMainWindow();
                            }
                            else
                            {
                                ShowErrorMessage("Неверное имя пользователя."); // Упрощенное сообщение
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка при подключении к базе данных: {ex.Message}");
            }
        }


        private void OpenMainWindow()
        {
            Window mainWindow = null;

            switch (_currentUserRoleId)
            {
                case 1: // Администратор
                    mainWindow = new MainWindow(_currentUserId); // Открываем MainWindow
                    break;
                case 2: // Кладовщик
                    mainWindow = new StorekeeperWindow(_currentUserId);
                    break;
                case 3: // Менеджер по продажам
                    mainWindow = new SalesManagerWindow(_currentUserId);
                    break;
                case 4: // Бухгалтер
                    mainWindow = new AccountantWindow(_currentUserId);
                    break;
                default:
                    MessageBox.Show("Неизвестная роль пользователя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }

            mainWindow.Show();
            this.Close();
        }

        private void ShowErrorMessage(string message)
        {
            lblErrorMessage.Text = message;
            lblErrorMessage.Visibility = Visibility.Visible;
        }
    }
}