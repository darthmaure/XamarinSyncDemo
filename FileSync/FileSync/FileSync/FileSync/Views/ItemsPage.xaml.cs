using System;
using System.ComponentModel;
using FileSync.Shared.Models;
using FileSync.ViewModels;
using Xamarin.Forms;

namespace FileSync.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        private readonly ItemsViewModel _viewModel = DependencyService.Get<ItemsViewModel>();

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel;
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (SyncItem)layout.BindingContext;
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAppearing();
        }
    }
}