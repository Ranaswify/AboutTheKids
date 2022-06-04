using AboutTheKids.Helper;
using AboutTheKids.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AboutTheKids.ViewModel
{
    public class PromoCodeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
        string _Value;
        public string Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                OnPropertyChanged();
            }
        }

        string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
        }
        bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }
        public Command UpladItemsCommand { get; set; }
        public PromoCodeViewModel()
        {
            UpladItemsCommand = new Command(async () => { await UpladItems(); });

        }

        private async Task UpladItems()
        {
            try
            {
                IsLoading = true;
                await Task.Delay(600);
                PromoCode promoCode = new PromoCode() { name = Name, value = Value };
                var i = CrossCloudFirestore.Current
                                              .Instance
                                              .Collection("PromoCode")
                                              .AddAsync(promoCode)
                                              .ContinueWith(t =>
                                              {
                                                  System.Diagnostics.Debug.WriteLine(t.Exception);
                                              }, TaskContinuationOptions.OnlyOnFaulted);
                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;
            }
        }
    }
}
