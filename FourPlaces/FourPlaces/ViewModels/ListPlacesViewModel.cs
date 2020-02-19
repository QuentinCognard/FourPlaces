using Common.Api.Dtos;
using FourPlaces.Views;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.Api.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FourPlaces.ViewModels
{
    class ListPlacesViewModel : ViewModelBase
    {
        private Lazy<INavigationService> _navigationService;

        private List<PlaceItemSummary> _places;
        public List<PlaceItemSummary> Places
        {
            get => _places;
            set => SetProperty(ref _places, value);
        }

        public ICommand GoToAddPlaceCommand { get; }
        public ICommand GoToUserCommand { get; }




        public ListPlacesViewModel()
        {

            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            GoToAddPlaceCommand = new Command(GoToAddPlace);
            GoToUserCommand = new Command(GoToUser);
            GetPlaces();
        }

        public async void GetPlaces()
        {

            HttpResponseMessage response = await App.API.Execute(HttpMethod.Get,ApiClient.LIST_PLACES);

            Response<List<PlaceItemSummary>> result = await App.API.ReadFromResponse<Response<List<PlaceItemSummary>>>(response);
            Places = result.Data;
     
            foreach (PlaceItemSummary p in Places)
            {
                p.ImageSourceURL = ApiClient.GET_IMAGE + p.ImageId;
            }
        }



        public async void GoToAddPlace()
        {
            await _navigationService.Value.PushAsync<AddPlaceView>();
        }

        public async void GoToUser()
        {
            await _navigationService.Value.PushAsync<UserMenuView>();
        }



    }
}
