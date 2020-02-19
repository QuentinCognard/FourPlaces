using Common.Api.Dtos;
using FourPlaces.Views;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace FourPlaces.ViewModels
{
    class UserMenuViewModel: ViewModelBase
    {
        private Lazy<INavigationService> _navigationService;
        private int idUser;


        private string _updatefirstname;
        public string UpdateFirstName
        {
            get => _updatefirstname;
            set => SetProperty(ref _updatefirstname, value);
        }

        private string _currentfirstname;
        public string CurrentFirstName
        {
            get => _currentfirstname;
            set => SetProperty(ref _currentfirstname, value);
        }

        private string _updatelastname;
        public string UpdateLastName
        {
            get => _updatelastname;
            set => SetProperty(ref _updatelastname, value);
        }

        private string _currentlastname;
        public string CurrentLastName
        {
            get => _currentlastname;
            set => SetProperty(ref _currentlastname, value);
        }

        private string _updatepassword;
        public string UpdatePassword
        {
            get => _updatepassword;
            set => SetProperty(ref _updatepassword, value);
        }


        private string _imagesrc;
        public string ImageSrc
        {
            get => _imagesrc;
            set => SetProperty(ref _imagesrc, value);
        }

        private string _oldpassword;
        public string OldPassword
        {
            get => _oldpassword;
            set => SetProperty(ref _oldpassword, value);
        }

        public ICommand UpdateUserCommand { get; }

        public UserMenuViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            UpdateUserCommand = new Command(UpdateUser);
            GetUser();

        }

        public async void GetUser()
        {
            HttpResponseMessage response = await App.API.Execute(HttpMethod.Get, ApiClient.ME,null, App.API.Token.AccessToken);
            Response<UserItem> result = await App.API.ReadFromResponse<Response<UserItem>>(response);
            UserItem user = result.Data;
           
            CurrentFirstName = user.FirstName;
            CurrentLastName = user.LastName;
            ImageSrc = ApiClient.GET_IMAGE+user.ImageId;
        }

        public async void UpdateUser()
        {
            UpdateProfileRequest userreq = new UpdateProfileRequest();
            userreq.FirstName = UpdateFirstName;
            userreq.LastName = UpdateLastName;
            userreq.ImageId = int.Parse(ImageSrc.Substring(ImageSrc.Length - 1));

            UpdatePasswordRequest pwdreq = new UpdatePasswordRequest();
            pwdreq.OldPassword = OldPassword;
            pwdreq.NewPassword = UpdatePassword;

            HttpResponseMessage responseuser = await App.API.Execute(new HttpMethod("PATCH"), ApiClient.ME, userreq,App.API.Token.AccessToken);
            HttpResponseMessage responsepwd = await App.API.Execute(new HttpMethod("PATCH"), ApiClient.ME_PASSWORD, pwdreq, App.API.Token.AccessToken);
            await _navigationService.Value.PushAsync<PlacesList>();
        }
    }
}
