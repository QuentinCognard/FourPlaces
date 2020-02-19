using FourPlaces.Views;
using Storm.Mvvm;
using System.Net;
using Xamarin.Forms;

namespace FourPlaces
{
    public partial class App : MvvmApplication
    {
        public static ApiClient API;


        public App() : base(() => new ConnexionMenu())
        {
            API = new ApiClient();
            InitializeComponent();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {

        }
    }
}
