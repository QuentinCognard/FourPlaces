using Common.Api.Dtos;
using FourPlaces.Views;
using Storm.Mvvm;
using Storm.Mvvm.Navigation;
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
    class AddCommentViewModel: ViewModelBase
    {
        private Lazy<INavigationService> _navigationService;

        private int idPlace;

        private string _commenttext;
        public string CommentText
        {
            get => _commenttext;
            set => SetProperty(ref _commenttext, value);
        }

        public ICommand ConfirmCommentCommand { get; }

        public AddCommentViewModel(int id)
        {
            _navigationService = new Lazy<INavigationService>(() => DependencyService.Resolve<INavigationService>());
            ConfirmCommentCommand = new Command(ConfirmComment);
            idPlace = id;
        }

        public async void ConfirmComment()
        {
            CreateCommentRequest req = new CreateCommentRequest
            {
                Text = CommentText
            };
            HttpResponseMessage response = await App.API.Execute(HttpMethod.Post, ApiClient.CREATE_COMMENT+ idPlace + "/comments", req,App.API.Token.AccessToken);
            Response result = await App.API.ReadFromResponse<Response>(response);

            if (response.IsSuccessStatusCode)
            {
                await _navigationService.Value.PopAsync();
            }
            else
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }
    }
}
