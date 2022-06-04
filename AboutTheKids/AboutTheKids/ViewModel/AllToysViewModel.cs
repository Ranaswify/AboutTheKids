using AboutTheKids.Helper;
using AboutTheKids.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AboutTheKids.ViewModel
{
    public class AllToysViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();


        ObservableCollection<ItemsModel> allToys;
        public ObservableCollection<ItemsModel> AllToys
        {
            set
            {
                allToys = value;
                OnPropertyChanged();
            }
            get=> allToys;
        }

        string image;
        public string Image
        {
            set
            {
                image = value;
                OnPropertyChanged();
            }
            get => image;
        }
        string isName;
        public string IsName
        {
            set
            {
                isName = value;
                OnPropertyChanged();
            }
            get => isName;
        }
        public Command GetNewArrivalToysCommand { get; set; }
        public Command GetBestSellerToysCommand { get; set; }
        public Command ChangePriceCommand { get; set; }
        public Command RemoveItemCommand { get; set; }
        

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
        public AllToysViewModel()
        {
            GetNewArrivalToysCommand = new Command(async () => { await GetNewArrivalToys(); });
            GetBestSellerToysCommand = new Command(async () => { await GetBestSellerToys(); });
            ChangePriceCommand = new Command<ItemsModel>(async (i) => { await ChangePrice(i); });
            RemoveItemCommand = new Command<ItemsModel>(async (i) => { await RemoveItem(i); });
        }

        private async Task RemoveItem(ItemsModel i)
        {
            try
            {
               bool yes = await  Application.Current.MainPage.DisplayAlert("", "Are you sure you want to delete this toy?", "Yes, sure", "No");
                if (yes)
                {
                    IsLoading = true;
                    await firebaseStorageHelper.DeleteItem(i.documentID, IsName);
                    if (IsName == "NewArrival")
                    {
                        await GetNewArrivalToys();
                    }
                    else
                    {
                        await GetBestSellerToys();
                    }
                    IsLoading = false;
                }
               
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("", "Sorry something went wrong :(" , "No");
                IsLoading = false;
            }
        }

        private async Task ChangePrice(ItemsModel i)
        {
            try
            {
                IsLoading = true;

                await firebaseStorageHelper.UpdateItem(i, i.documentID,IsName);
                IsLoading = false;

            }
            catch { }
        }

        private async Task GetBestSellerToys()
        {
            try
            {
                IsLoading = true;

                AllToys = await firebaseStorageHelper.GetBestSeller();
                foreach (var i in AllToys)
                {
                    i.image = i.imgUrls[0];
                }
                IsName = "BestSeller";
                IsLoading = false;

            }
            catch (Exception i)
            {
                IsLoading = false;
            }
        }

        private async Task GetNewArrivalToys()
        {
            try
            {
                IsLoading = true;
                AllToys = await firebaseStorageHelper.GetNewArrivals();
                foreach (var i in AllToys)
                {
                    i.image = i.imgUrls[0];
                }
                IsName = "NewArrival";
                IsLoading = false;
            }
            catch(Exception e)
            {

            }
        }
    }
}