using AboutTheKids.Models;
using AboutTheKids.Views;
using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AboutTheKids
{
    public partial class MainPage : ContentPage
    {
        //MediaFile file;
        //List<ImagesModel> imagesModels = new List<ImagesModel>();

        public MainPage()
        {
            InitializeComponent();
            
        }

        private async void NewArrival_Clicked(object sender, EventArgs e)
        {
            loader.IsVisible = true;
            loading.IsRunning = true;
            await Navigation.PushAsync(new UploadPage("NewArrival"));
            loader.IsVisible = false;
            loading.IsRunning = false;
        }

        private async void BestSeller_Clicked(object sender, EventArgs e)
        {
            loader.IsVisible = true;
            loading.IsRunning = true;
            await Navigation.PushAsync(new UploadPage("BestSeller"));
            loader.IsVisible = false;
            loading.IsRunning = false;
        }

        private async void PromoCode_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PromoCodePage());
        }

        private async void AllOrders_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AllOrdersPage());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AllToysPage());

        }

        //private async void btnPick_Clicked(object sender, EventArgs e)
        //{
        //await CrossMedia.Current.Initialize();
        //try
        //{

        //    file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
        //    {
        //        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

        //    });
        //    if (file == null)
        //        return;
        //    //imgChoosed.Source = ImageSource.FromStream(() =>
        //    //{
        //    //    var imageStram = file.GetStream();
        //    //    return imageStram;
        //    //});
        //    await StoreImages(file.GetStream());
        //}
        //catch (Exception ex)
        //{
        //    // Debug.WriteLine(ex.Message);
        //}
        //    try
        //    {
        //        var results = await MediaGallery.PickAsync(3, MediaFileType.Image, MediaFileType.Video);

        //        if (results?.Files == null)
        //        {
        //            return;
        //        }

        //        foreach (var media in results.Files)
        //        {
        //            var imageStram = await  media.OpenReadAsync();
        //            var fileName = media.NameWithoutExtension;
        //            var extension = media.Extension;
        //            var contentType = media.ContentType;
        //            imagesModels.Add(new ImagesModel { NameWithoutExtension = fileName + "." + extension, 
        //                Extension = extension, ContentType = contentType, imgStream=imageStram,
        //            imgSource=ImageSource.FromStream(()=> { return imageStram; })
        //        });
        //        }
        //        myList.ItemsSource = imagesModels;

        //    }
        //    catch
        //    {

        //    }
        //}
        //private async void btnStore_Clicked(object sender, EventArgs e)
        //{
        //    await StoreImages(file.GetStream());
        //}

        //public async Task<string> StoreImages(Stream imageStream)
        //{
        //    var stroageImage = await new FirebaseStorage("babygames-b2767.appspot.com")
        //        .Child("XamarinMonkeys")
        //        .Child("image.jpg")
        //        .PutAsync(imageStream);
        //    string imgurl = stroageImage;

        //    return imgurl;
        //}


    }
}
