using AboutTheKids.Helper;
using AboutTheKids.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AboutTheKids.ViewModel
{
    public class AllOrdersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
        ObservableCollection<PaymentInfo> allSaved;
        public ObservableCollection<PaymentInfo> AllSaved
        {
            set
            {
                allSaved = value;
                OnPropertyChanged();
            }
            get
            {
                return allSaved;
            }
        }

        string allSavedCount;
        public string AllSavedCount
        {
            set
            {
                allSavedCount = value;
                OnPropertyChanged();
            }
            get
            {
                return allSavedCount;
            }
        }

        ObservableCollection<string> allStatus;
        public ObservableCollection<string> AllStatus
        {
            set
            {
                allStatus = value;
                OnPropertyChanged();
            }
            get
            {
                return allStatus;
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
        PaymentInfo selectedStatus;
        public PaymentInfo SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeStatusCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        
        public AllOrdersViewModel()
        {
            ChangeStatusCommand = new Command<PaymentInfo>(async (i) => { await ChangeStatus(i); });
            DeleteItemCommand = new Command<PaymentInfo>(async (i) => { await DeleteItem(i); });

            AllStatus = new ObservableCollection<string>();
            AllStatus.Add("Shipped");
            AllStatus.Add("Delivered");
            GetAll();
        }

        private async Task DeleteItem(PaymentInfo i)
        {
            IsLoading = true;

            try
            {
                AllSaved.Remove(i);
                await CrossCloudFirestore.Current
                         .Instance
                         .Collection("Payment")
                         .Document(i.documentID)
                         .DeleteAsync();
                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;
            }
        }

        private async Task ChangeStatus(PaymentInfo i)
        {
            try
            {
                var k = await Application.Current.MainPage.DisplayActionSheet("ChangeOrderStatus", "Cancel", "", AllStatus.ToArray());
                if (k != null && k != "Cancel")
                {
                    i.OrderStatus = k;

                    await firebaseStorageHelper.UpdatePerson(i, i.documentID);
                }
            }
            catch(Exception r)
            {

            }
           
        }

        async void GetAll()
        {
            IsLoading = true;
            try
            {
                AllSaved = await firebaseStorageHelper.GetMyOrders();
                var i = AllSaved.Where(a => a.OrderStatus != "Delivered");
                AllSaved = new ObservableCollection<PaymentInfo>(i);
                AllSavedCount = AllSaved.Count.ToString();
                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;
            }
        }
    }
}