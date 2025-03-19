using System;
using Xamarin.Forms;
using Supabase;
using Xamarin.Essentials;
using System.Threading.Tasks;
using MSM.Models;
using MSM;
namespace MSM
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            ErrorLabel.IsVisible = false;

            try
            {
                // 1. Authenticate with Supabase Auth
                var authResponse = await App.SupabaseClient.Auth.SignIn(EmailEntry.Text, PasswordEntry.Text);

                if (authResponse?.User == null)
                {
                    ErrorLabel.Text = "Invalid login attempt.";
                    ErrorLabel.IsVisible = true;
                    return;
                }

                // 2. Retrieve user data from the 'Пользователи' table
                var userResponse = await App.SupabaseClient
                    .From<Пользователи>() // Замените на вашу модель Пользователи
                    .Select("*")
                    .Where(x => x.email == EmailEntry.Text)
                    .Get();

                if (userResponse?.Models == null || userResponse.Models.Count == 0)
                {
                    ErrorLabel.Text = "User not found in the system.";
                    ErrorLabel.IsVisible = true;
                    return;
                }

                var user = userResponse.Models[0];
                string role = user.роль; // Получаем роль пользователя

                // 3. Store the user's role and email in SecureStorage
                await SecureStorage.SetAsync("UserRole", role);
                await SecureStorage.SetAsync("UserEmail", EmailEntry.Text);

                // 4. Navigate to the appropriate page based on the user's role
                await NavigateToMainPage(role);
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = $"Login failed: {ex.Message}";
                ErrorLabel.IsVisible = true;
            }
        }

        private async Task NavigateToMainPage(string role)
        {
            // Передаем роль пользователя в конструктор MainPage
            App.Current.MainPage = new NavigationPage(new MainPage(role));
        }
    }
}