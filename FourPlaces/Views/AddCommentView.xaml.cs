using FourPlaces.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms.Xaml;

namespace FourPlaces.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCommentView : BaseContentPage
    {
        public int idPlace;

        public AddCommentView(int id)
        {
            idPlace = id;
            BindingContext = new AddCommentViewModel(idPlace);
            InitializeComponent();
        }
    }
}