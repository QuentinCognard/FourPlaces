using System;
using System.ComponentModel;
using FourPlaces.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms;

namespace FourPlaces.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ConnexionMenu : BaseContentPage
    {
        public ConnexionMenu()
        {
            BindingContext = new ConnexionMenuViewModel();
            InitializeComponent();
        }

    }
}
