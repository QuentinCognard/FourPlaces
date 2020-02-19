using Common.Api.Dtos;
using FourPlaces.Views;
using Plugin.Geolocator;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.Api.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FourPlaces.ViewModels
{
    class PlacePageViewModel : ViewModelBase
    {
        private Lazy<INavigationService> _navigationService;
        public int idPlace;

        private static Position defaultPosition;
        public static double radiusMap;
        public Map WorldMap { get; set; }

        private PlaceItem _place;

        public PlaceItem Place
        {
            get => _place;
            set => SetProperty(ref _place, value);
        }

        private string _imageSourceURL;
        public string ImageSourceURL
        {
            get => _imageSourceURL;
            set => SetProperty(ref _imageSourceURL, value);
        }

        public ICommand GoAddCommentCommand { get; }

        public PlacePageViewModel(int id)
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            idPlace =  id;
            defaultPosition = new Position(47.845647, 1.939958);
            radiusMap = 2.5;
            WorldMap = new Map
            {
                MapType = MapType.Street
            };
            GoAddCommentCommand = new Command(GoAddComment);
            GetPlace();
        }

        public async void GetPlace()
        {
            HttpResponseMessage response = await App.API.Execute(HttpMethod.Get, ApiClient.GET_PLACE+idPlace);
            Response<PlaceItem> result = await App.API.ReadFromResponse<Response<PlaceItem>>(response);
            Place = result.Data;
            ImageSourceURL = ApiClient.GET_IMAGE + Place.ImageId;
/*            Position user_position = await LocalisationAsync();
*/            var position = new Position(Place.Latitude, Place.Longitude);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = Place.Title
            };
            WorldMap.Pins.Add(pin);
            WorldMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(10)));
            /*            WorldMap.MoveToRegion(MapSpan.FromCenterAndRadius(user_position, Distance.FromKilometers(radiusMap)));
            */
        }

        public async void GoAddComment()
        {
            await NavigationService.PushAsync(new AddCommentView(idPlace));
        }


        public static async Task<Position> LocalisationAsync()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;
                if (CrossGeolocator.IsSupported && CrossGeolocator.Current.IsGeolocationEnabled && CrossGeolocator.Current.IsGeolocationAvailable)
                {

                    var current_location = await locator.GetPositionAsync();
                    Position current_position = new Position(current_location.Latitude, current_location.Longitude);

                    return current_position;
                }
                var last_location = await locator.GetLastKnownLocationAsync();
                return new Position(last_location.Latitude, last_location.Longitude);

            }
            catch (Exception)
            {

                return defaultPosition;
            }
        }
    }
}
