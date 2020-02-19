using Storm.Mvvm;
using TD.Api.Dtos;
using System.Windows.Input;
using Xamarin.Forms;
using FourPlaces.Views;
using System;
using Storm.Mvvm.Services;
using System.Threading.Tasks;
using System.Net.Http;
using Common.Api.Dtos;

namespace FourPlaces.ViewModels
{
    public class ConnexionMenuViewModel: ViewModelBase
    {
        private Lazy<INavigationService> _navigationService;

        private string _email;
        private string _password;
        private string _loginerror;


        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string LoginError
        {
            get => _loginerror;
            set => SetProperty(ref _loginerror, value);
        }


        public ICommand AuthCommand { get; }
        public ICommand GoRegistrationCommand { get; }


        public ConnexionMenuViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            AuthCommand = new Command(Authenticate);
            GoRegistrationCommand = new Command(GoRegister);
        }
 
        async void Authenticate()
        {
            LoginRequest req = new LoginRequest
            {
                Email = Email,
                Password = Password
            };

            HttpResponseMessage response = await App.API.Execute(HttpMethod.Post, ApiClient.LOGIN,req);
            if (response.IsSuccessStatusCode)
            {
                Response<LoginResult> result = await App.API.ReadFromResponse<Response<LoginResult>>(response);
                App.API.Token = result.Data;
                Console.WriteLine("Acess :" + App.API.Token.AccessToken);
                await _navigationService.Value.PushAsync<PlacesList>();
            }
            else
            {
                LoginError = "Mauvais mail / password";
            }

        }

        async void GoRegister()
        {
            await _navigationService.Value.PushAsync<RegistrationPage>();
        }
    }
}
