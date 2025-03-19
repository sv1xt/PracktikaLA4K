using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Linq;
using System;
using MSM;

namespace MSM
{
    public partial class MainPage : TabbedPage
    {
        public MainPage(string role)
        {
            InitializeComponent();
            Title = $"MegaSklad ({role})";

            RemoveScanTabIfNeeded(); // Вызываем метод для удаления вкладки
        }

        private async Task RemoveScanTabIfNeeded()
        {
            try
            {
                string userEmail = await SecureStorage.GetAsync("UserEmail");

                if (userEmail != "kladovshik@gmail.com")
                {
                    // Находим ScanPage среди дочерних страниц
                    var scanPage = Children.FirstOrDefault(page => page is ScanPage);

                    if (scanPage != null)
                    {
                        Children.Remove(scanPage); // Удаляем вкладку
                    }
                }
            }
            catch (System.Exception ex)
            {
                // Обработка ошибок (например, SecureStorage недоступен)
                Console.WriteLine($"Ошибка при удалении вкладки Scan: {ex.Message}");
            }
        }
    }
}