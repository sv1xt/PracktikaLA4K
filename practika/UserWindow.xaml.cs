using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using OtpNet; //Для генерации ключа

namespace practika
{
    public partial class UserWindow : Window
    {
        private string _connectionString = "Data Source=DESKTOP-UGJSU56\\SQLEXPRESS01;Initial Catalog=SkladUchet;Integrated Security=True;TrustServerCertificate=True";
        private int? _userId; // Nullable int (может быть null - для добавления, или значение - для редактирования)

        // Конструктор для ДОБАВЛЕНИЯ пользователя
        public UserWindow()
        {
            InitializeComponent();
            LoadRoles();
        }

        // Конструктор для РЕДАКТИРОВАНИЯ пользователя
        public UserWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
            LoadRoles();
            LoadUserData();
        }

        private void LoadRoles()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT Role_ID, Название_Роли FROM Роли";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Role> roles = new List<Role>();
                            while (reader.Read())
                            {
                                roles.Add(new Role { Role_ID = (int)reader["Role_ID"], Название_Роли = (string)reader["Название_Роли"] });
                            }
                            cmbRoles.ItemsSource = roles;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке ролей: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false; // Закрываем окно в случае ошибки
            }
        }


        private void LoadUserData()
        {
            if (_userId == null) return; // Не должно произойти, но на всякий случай

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT Имя_Пользователя, Электронная_Почта, Role_ID, Двухфакторная_Аутентификация_Включена, Секрет_Двухфакторной_Аутентификации FROM Пользователи WHERE User_ID = @UserId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", _userId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtUsername.Text = (string)reader["Имя_Пользователя"];
                                txtEmail.Text = (string)reader["Электронная_Почта"];
                                cmbRoles.SelectedValue = (int)reader["Role_ID"];
                                chkTwoFactorEnabled.IsChecked = (bool)reader["Двухфакторная_Аутентификация_Включена"];
                                if (reader["Секрет_Двухфакторной_Аутентификации"] != DBNull.Value)
                                {
                                    lblSecretKey.Content = (string)reader["Секрет_Двухфакторной_Аутентификации"];
                                    lblSecretKey.Visibility = Visibility.Visible;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // 1. Валидация
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || cmbRoles.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_userId == null) // Добавление нового пользователя
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Password) || string.IsNullOrWhiteSpace(txtConfirmPassword.Password))
                {
                    MessageBox.Show("Пожалуйста, введите пароль и подтверждение пароля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtPassword.Password) && txtPassword.Password != txtConfirmPassword.Password)
            {
                MessageBox.Show("Пароль и подтверждение пароля не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            //Проверка на уникальность имени пользователя
            if (IsUsernameTaken(txtUsername.Text))
            {
                MessageBox.Show("Такое имя пользователя уже занято.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // 2. Хеширование пароля (если добавляем нового или меняем пароль)
            string hashedPassword = null;
            string salt = null;

            if (_userId == null || !string.IsNullOrWhiteSpace(txtPassword.Password)) //Если новый пользователь или пароль был изменен
            {
                (hashedPassword, salt) = PasswordHasher.HashPassword(txtPassword.Password);
            }

            // 3. Генерация секретного ключа для 2FA
            string twoFactorSecret = null;
            if (chkTwoFactorEnabled.IsChecked == true)
            {
                twoFactorSecret = GenerateSecretKey();
            }


            // 4. Сохранение в БД
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query;

                    if (_userId == null) // ДОБАВЛЕНИЕ
                    {
                        query = "INSERT INTO Пользователи (Имя_Пользователя, Хеш_Пароля, Соль, Role_ID, Электронная_Почта, Двухфакторная_Аутентификация_Включена, Секрет_Двухфакторной_Аутентификации) " +
                                "VALUES (@Username, @PasswordHash, @Salt, @RoleId, @Email, @TwoFactorEnabled, @TwoFactorSecret)";
                    }
                    else // РЕДАКТИРОВАНИЕ
                    {
                        query = "UPDATE Пользователи SET Имя_Пользователя = @Username, Role_ID = @RoleId, Электронная_Почта = @Email, Двухфакторная_Аутентификация_Включена = @TwoFactorEnabled, Секрет_Двухфакторной_Аутентификации = @TwoFactorSecret";
                        // Добавляем изменение пароля, только если он был изменен
                        if (!string.IsNullOrWhiteSpace(txtPassword.Password))
                        {
                            query += ", Хеш_Пароля = @PasswordHash, Соль = @Salt";
                        }
                        query += " WHERE User_ID = @UserId";
                    }


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", txtUsername.Text);
                        if (!string.IsNullOrEmpty(hashedPassword)) // Пароль добавляем, только если он был изменен
                        {
                            command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                            command.Parameters.AddWithValue("@Salt", salt);
                        }
                        command.Parameters.AddWithValue("@RoleId", ((Role)cmbRoles.SelectedItem).Role_ID);
                        command.Parameters.AddWithValue("@Email", txtEmail.Text);
                        command.Parameters.AddWithValue("@TwoFactorEnabled", chkTwoFactorEnabled.IsChecked ?? false); //если null то false
                        command.Parameters.AddWithValue("@TwoFactorSecret", twoFactorSecret != null ? (object)twoFactorSecret : DBNull.Value); // ИСПРАВЛЕНО!


                        if (_userId != null)
                        {
                            command.Parameters.AddWithValue("@UserId", _userId);
                        }

                        command.ExecuteNonQuery();
                    }
                }

                DialogResult = true; // Успешно сохранено
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Закрываем окно без сохранения
        }

        private string GenerateSecretKey()
        {
            var key = KeyGeneration.GenerateRandomKey(20);
            return Base32Encoding.ToString(key);
        }

        private bool IsUsernameTaken(string username)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Пользователи WHERE Имя_Пользователя = @Username";
                if (_userId.HasValue) // Если это редактирование, исключаем текущего пользователя из проверки
                {
                    query += " AND User_ID <> @UserId";
                }
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    if (_userId.HasValue)
                    {
                        command.Parameters.AddWithValue("@UserId", _userId.Value);
                    }

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
    // Вспомогательный класс для ComboBox (чтобы отображать Название_Роли, а использовать Role_ID)
    public class Role
    {
        public int Role_ID { get; set; }
        public string Название_Роли { get; set; }
    }
}