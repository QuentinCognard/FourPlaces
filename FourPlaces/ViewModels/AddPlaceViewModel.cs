using Common.Api.Dtos;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace FourPlaces.ViewModels
{
    class AddPlaceViewModel: ViewModelBase
    {

        private Lazy<INavigationService> _navigationService;
        private MediaFile picture;


        private string _titreplace;
        private string _descriptionplace;
        private string _imagesrc;
        private double _longitude;
        private double _latitude;

        public string TitrePlace
        {
            get => _titreplace;
            set => SetProperty(ref _titreplace, value);
        }
        public string DescriptionPlace
        {
            get => _descriptionplace;
            set => SetProperty(ref _descriptionplace, value);
        }
        public string ImageSrc
        {
            get => _imagesrc;
            set => SetProperty(ref _imagesrc, value);
        }
        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }
        public double Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }
        public ICommand SendImgCommand { get; }
        public ICommand SendCommand { get; }


        public AddPlaceViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            SendImgCommand = new Command(SendImg);
            SendCommand = new Command(Send);
        }

        public async void SendImg()
        {
            MediaFile pic = await PickAPic();
            if (pic == null)

                ImageSrc = null;
            else
            {
                picture = pic;
                ImageSrc = pic.Path;
            }
        }
        public async void Send()
        {
            CreatePlaceRequest req = new CreatePlaceRequest
            {
                Title = TitrePlace,
                Description = DescriptionPlace,
                Latitude = Latitude,
                Longitude = Longitude
            };

            HttpResponseMessage responseimg = await App.API.Execute(HttpMethod.Post, ApiClient.CREATE_IMAGE);
            Response result = await App.API.ReadFromResponse<Response>(responseimg);
/*            req.ImageId = resut
*/

            HttpResponseMessage response = await App.API.Execute(HttpMethod.Post, ApiClient.CREATE_PLACE, req, App.API.Token.AccessToken);
            
            if (response.IsSuccessStatusCode)
            {
                await _navigationService.Value.PopAsync();
            }

        }

        public static async Task<MediaFile> PickAPic()
        {
            await CrossMedia.Current.Initialize();
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                return await CrossMedia.Current.PickPhotoAsync();
            }

            return null;
        }
    }
}
