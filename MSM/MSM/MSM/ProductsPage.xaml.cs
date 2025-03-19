using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using MSM.Models;
using System.Collections.Generic;
using System.Linq;
using MSM;

namespace MSM
{
    public partial class ProductsPage : ContentPage
    {
        private ObservableCollection<Товары> _products;
        private List<Склады> _warehouses;

        public ProductsPage()
        {
            InitializeComponent();

            _products = new ObservableCollection<Товары>();
            ProductsCollectionView.ItemsSource = _products;

            LoadWarehouses();
        }

        private async Task LoadWarehouses()
        {
            try
            {
                var response = await App.SupabaseClient
                    .From<Склады>()
                    .Select("*")
                    .Get();

                if (response?.Models != null)
                {
                    _warehouses = response.Models;
                    WarehousePicker.ItemsSource = _warehouses.Select(w => w.название_склада).ToList();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to load warehouses.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error loading warehouses: {ex.Message}", "OK");
            }
        }

        private async void WarehousePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WarehousePicker.SelectedIndex >= 0)
            {
                string selectedWarehouseName = (string)WarehousePicker.SelectedItem;
                Склады selectedWarehouse = _warehouses.FirstOrDefault(w => w.название_склада == selectedWarehouseName);

                if (selectedWarehouse != null)
                {
                    await LoadProductsForWarehouse(selectedWarehouse.id);
                }
            }
        }

        private async Task LoadProductsForWarehouse(Guid warehouseId)
        {
            try
            {
                // 1. Get Остатки_товаров for the selected warehouse
                var ostatkiResponse = await App.SupabaseClient
                    .From<ОстаткиТоваров>()
                    .Select("id_товара, количество") // Select both id_товара and количество
                    .Where(x => x.id_склада == warehouseId)
                    .Get();

                if (ostatkiResponse?.Models != null && ostatkiResponse.Models.Any())
                {
                    // 2. Get the list of product IDs
                    var productIds = ostatkiResponse.Models.Select(o => o.id_товара).ToList();

                    // 3. Get Товары for the selected product IDs using Filter
                    var productsResponse = await App.SupabaseClient
                        .From<Товары>()
                        .Select("*, штрихкод_товара") // Select all columns including штрихкод_товара
                        .Filter("id", Postgrest.Constants.Operator.In, productIds)
                        .Get();

                    if (productsResponse?.Models != null)
                    {
                        _products.Clear();
                        foreach (var product in productsResponse.Models)
                        {
                            // Find the matching ОстаткиТоваров entry
                            var ostatok = ostatkiResponse.Models.FirstOrDefault(o => o.id_товара == product.id);

                            // Get the public URL for the QR code
                            string qrCodeUrl = null;
                            if (!string.IsNullOrEmpty(product.штрихкод_товара))
                            {
                                qrCodeUrl = App.SupabaseClient.Storage
                                              .From("barcode")
                                              .GetPublicUrl(product.штрихкод_товара);
                            }

                            // set data from DB
                            product.количество = ostatok?.количество ?? 0;
                            product.qrCodeUrl = qrCodeUrl;
                            _products.Add(product); // we use Товары as it is the binding
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to load products.", "OK");
                    }
                }
                else
                {
                    _products.Clear(); // No products for this warehouse
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error loading products: {ex.Message}", "OK");
            }
        }
    }
}