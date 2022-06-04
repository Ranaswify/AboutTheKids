using AboutTheKids.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutTheKids.Helper
{
    public class FirebaseStorageHelper
    {
        // FirebaseStorage firebaseStorage = new FirebaseStorage("babygames-b2767.appspot.com");
        //Upload file to Firebase
        FirebaseClient firebase = new FirebaseClient("https://aboutthekids-788ac-default-rtdb.firebaseio.com/");
        public async Task<string> UploadFile(Stream fileStream, string categoryName, string fileName)
        {
            try
            {
               var firebaseStorage = new FirebaseStorage("aboutthekids-788ac.appspot.com"
               ).Child(categoryName)
               .Child(fileName)
                .PutAsync(fileStream);
                var down = await firebaseStorage;
                return await firebaseStorage;
            }
            catch(Exception ex)
            {
                return "";
            }
            
        }

        public async Task DeleteItem(string document, string collection)
        {
            try
            {
                await CrossCloudFirestore.Current
                      .Instance
                      .Collection(collection)
                      .Document(document)
                      .DeleteAsync();
            }
            catch
            {

            }
         
        }

        public async Task UpdatePerson(PaymentInfo paymentInfo, string yourdocument)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .Collection("Payment")
                         .Document(yourdocument)
                         .UpdateAsync(paymentInfo);
        }

        public async Task<ObservableCollection<PaymentInfo>> GetMyOrders()
        {
            try
            {
                var group = await CrossCloudFirestore.Current
                                    .Instance
                                    .CollectionGroup("Payment")
                                    .GetAsync();
                var m = group.Documents.ToList();
                var yourModels = group.ToObjects<PaymentInfo>().ToList();

                for (int i = 0; i < m.Count; i++)
                {
                    yourModels[i].documentID = m[i].Id;
                }
                return new ObservableCollection<PaymentInfo>(yourModels);
            }
            catch (Exception ex)
            {
                return new ObservableCollection<PaymentInfo>();
            }

        }

        public async Task<ObservableCollection<ItemsModel>> GetNewArrivals()
        {
            try
            {
                var group = await CrossCloudFirestore.Current
                                    .Instance
                                    .CollectionGroup("NewArrival")
                                    .GetAsync();
                var m = group.Documents.ToList();
                var yourModels = group.ToObjects<ItemsModel>().ToList();

                var s = group.Documents.ToList();

                for (int i = 0; i < s.Count; i++)
                {
                    yourModels[i].documentID = s[i].Id;
                }
                return new ObservableCollection<ItemsModel>(yourModels);
            }
            catch (Exception ex)
            {
                return new ObservableCollection<ItemsModel>();
            }

        }
        public async Task<ObservableCollection<ItemsModel>> GetBestSeller()
        {
            try
            {
                var group = await CrossCloudFirestore.Current
                                    .Instance
                                    .CollectionGroup("BestSeller")
                                    .GetAsync();
                var m = group.Documents.ToList();

                var yourModels = group.ToObjects<ItemsModel>().ToList();
                var s = group.Documents.ToList();

                for (int i = 0; i < s.Count; i++)
                {
                    yourModels[i].documentID = s[i].Id;
                }
                return new ObservableCollection<ItemsModel>(yourModels);
            }
            catch (Exception ex)
            {
                return new ObservableCollection<ItemsModel>();
            }

        }

        public async Task UpdateItem(ItemsModel paymentInfo, string yourdocument, string name)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .Collection(name)
                         .Document(yourdocument)
                         .UpdateAsync(paymentInfo);
        }
    }
}
