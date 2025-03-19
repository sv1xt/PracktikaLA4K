using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using MSM.Models;
using MSM;

namespace MSM
{
    public partial class WarehousesPage : ContentPage
    {
        private ObservableCollection<Склады> _warehouses;

        public WarehousesPage()
        {
            InitializeComponent();
            _warehouses = new ObservableCollection<Склады>();
            LoadWarehouses();
        }

        private async Task LoadWarehouses()
        {
            try
            {
                // 1. Get the warehouses from Supabase
                var response = await App.SupabaseClient
                    .From<Склады>() // Replace with your actual model
                    .Select("*")
                    .Get();

                if (response?.Models != null)
                {
                    _warehouses.Clear();
                    foreach (var warehouse in response.Models)
                    {
                        _warehouses.Add(warehouse);
                    }
                    WarehousesCollectionView.ItemsSource = _warehouses;
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
    }
}