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
    public partial class AllToysPage : ContentPage
    {
        public AllToysPage()
        {
            InitializeComponent();
            BindingContext = new AllToysViewModel();

        }
    }
}