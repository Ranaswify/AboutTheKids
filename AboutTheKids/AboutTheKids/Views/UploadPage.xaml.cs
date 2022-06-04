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
    public partial class UploadPage : ContentPage
    {
        private string v;

        public UploadPage()
        {
            InitializeComponent();
        }

        public UploadPage(string v)
        {
            InitializeComponent();
            this.v = v;
            BindingContext = new UploadViewModel(v);
        }
    }
}