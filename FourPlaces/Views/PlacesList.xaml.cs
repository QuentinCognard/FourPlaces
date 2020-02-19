using FourPlaces.ViewModels;
using Storm.Mvvm.Forms;
using System;
using TD.Api.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FourPlaces.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlacesList : BaseContentPage
    {
        public PlacesList()
        {
            BindingContext = new ListPlacesViewModel();
            InitializeComponent();
        }

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            PlaceItemSummary place = (PlaceItemSummary)e.SelectedItem;
            await Navigation.PushAsync(new PlacePage(place.Id));
            ((ListView)sender).SelectedItem = null;
        }
    }
}
