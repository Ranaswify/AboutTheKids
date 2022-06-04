using AboutTheKids.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AboutTheKids.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllOrdersPage : ContentPage
    {
        public AllOrdersPage()
        {
            InitializeComponent();
            BindingContext = new AllOrdersViewModel();
        }
    }
}