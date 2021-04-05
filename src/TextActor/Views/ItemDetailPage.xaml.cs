using System.ComponentModel;
using Xamarin.Forms;
using TextActor.ViewModels;

namespace TextActor.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}