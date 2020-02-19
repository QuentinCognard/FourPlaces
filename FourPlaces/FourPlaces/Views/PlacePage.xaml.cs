using FourPlaces.ViewModels;
using Storm.Mvvm.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FourPlaces.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlacePage : BaseContentPage
    {
        public int idPlace;
        public PlacePage(int id)
        {
            idPlace = id;
            BindingContext = new PlacePageViewModel(idPlace);
            InitializeComponent();

        }
    }
}