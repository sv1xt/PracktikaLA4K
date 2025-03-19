using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Linq;
using MSM.Models;
using MSM;
using System.Xml;

namespace MSM
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            LoadUserInfo();
        }

        private async Task LoadUserInfo()
        {
            try
            {
                // Get user's email from SecureStorage
                string userEmail = await SecureStorage.GetAsync("UserEmail");

                // Make sure email exists
                if (string.IsNullOrEmpty(userEmail))
                {
                    await DisplayAlert("Ошибка", $"Почта не найдена", "OK");
                    return;
                }

                var userResponse = await App.SupabaseClient
                    .From<Models.Пользователи>()
                    .Select("*")
                    .Where(x => x.email == userEmail)
                    .Get();

                if (userResponse?.Models != null && userResponse.Models.Any())
                {
                    var user = userResponse.Models.FirstOrDefault();
                    // Set the data from DB
                    EmailLabel.Text = user.email;
                    RoleLabel.Text = user.роль;
                    RoleOnPassLabel.Text = user.роль;
                    NameLabel.Text = user.имя_пользователя;

                    // Костыль: скрываем пропуск, если email не kladovshik@gmail.com
                    if (user.email != "kladovshik@gmail.com")
                    {
                        PassSection.IsVisible = false;
                    }
                }
                else
                {
                    await DisplayAlert("Ошибка", "Не удалось получить данные о пользователе", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось загрузить информацию о пользователе: {ex.Message}", "OK");
            }
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            //Clear data and sign out
            SecureStorage.RemoveAll();
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}