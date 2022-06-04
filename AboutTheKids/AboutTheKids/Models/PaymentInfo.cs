using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AboutTheKids.Models
{
    public class PaymentInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string FullName { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string UserId { get; set; }
        public string PromoCode { get; set; }
       // public string OrderStatus { get; set; }
        string orderStatus;
        public string OrderStatus
        {
            get
            {
                return orderStatus;

            }
            set
            {
                orderStatus = value;
                OnPropertyChanged();
            }
        }

        public double PaidAmount { get; set; }
        public ItemsToPay MyItemsToPay { get; set; }
        public DateTime CreatedTime { get; set; }
        public string documentID { get; set; }
    }

    public class ItemsToPay
    {
        public ObservableCollection<SavedItems> ItemToPay { get; set; }
       
    }

    public class SavedItems 
    {
        public Items myItem { get; set; }
        public string documentID { get; set; }
        public string userId { get; set; }
        public int Count { get; set; }
        public double TotalSellingPrice { get; set; }
    }

    public class Items
    {
       
        public List<string> imgUrls { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double coast { get; set; }

        public string category { get; set; }
        public string imageUrl { get; set; }
    }
}
