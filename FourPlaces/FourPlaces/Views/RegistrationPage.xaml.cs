using FourPlaces.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FourPlaces.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : BaseContentPage
    {

        public RegistrationPage()
        {
            BindingContext = new RegistrationPageViewModel();
            InitializeComponent();
        }
    }
}