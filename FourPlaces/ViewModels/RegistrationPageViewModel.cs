using FourPlaces.Views;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Net.Http;
using System.Windows.Input;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace FourPlaces.ViewModels
{
    public class RegistrationPageViewModel : ViewModelBase
    {
        private Lazy<INavigationService> _navigationService;


        private string _email;
        private string _firstname;
        private string _lastname;
        private string _password1;
        private string _password2;
        private string _registrationerror;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }


        public string FirstName
        {
            get => _firstname;
            set => SetProperty(ref _firstname, value);
        }
        public string LastName
        {
            get => _lastname;
            set => SetProperty(ref _lastname, value);
        }
        public string Password1
        {
            get => _password1;
            set => SetProperty(ref _password1, value);
        }

        public string Password2
        {
            get => _password2;
            set => SetProperty(ref _password2, value);
        }

        public string RegistrationError
        {
            get => _registrationerror;
            set => SetProperty(ref _registrationerror, value);
        }

        public ICommand RegistrationCommand { get; }

        public RegistrationPageViewModel()
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            RegistrationCommand = new Command(Register);
        }

        public async void Register()
        {
            if (Password1.Equals(Password2))
            {
                RegisterRequest req = new RegisterRequest
                {
                    Email = Email,
                    FirstName = FirstName,
                    LastName = LastName,
                    Password = Password1
                };

                HttpResponseMessage response = await App.API.Execute(HttpMethod.Post, ApiClient.REGISTER, req);
                if (response.IsSuccessStatusCode)
                {
                    await _navigationService.Value.PushAsync<ConnexionMenu>();
                }
            }
            else
            {
                RegistrationError = "Mot de passes différents";
            }


        }
    }
}
 