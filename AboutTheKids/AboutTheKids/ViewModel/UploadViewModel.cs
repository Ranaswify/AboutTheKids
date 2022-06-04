using AboutTheKids.Helper;
using AboutTheKids.Models;
using Firebase.Storage;
using Plugin.CloudFirestore;
//using Plugin.CloudFirestore;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AboutTheKids.ViewModel
{
    public class UploadViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
        MediaFile file;
        public Command UpladItemsCommand { get; set; }
        public Command SelectCategoryCommand { get; set; }
        public Command StoreItemsCommand { get; set; }
        public Command DeleteItemCommand { get; set; }
        public List<string> urls { get; set; }
        ObservableCollection<ImagesModel> _images;
        public ObservableCollection<ImagesModel> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<Category> _category;
        public ObservableCollection<Category> category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        Category _SelectedCategory;
        public Category SelectedCategory
        {
            get { return _SelectedCategory; }
            set
            {
                _SelectedCategory = value;
                OnPropertyChanged();
            }
        }

        string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
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
        private string v;

        double _Coast;
        public double Coast
        {
            get { return _Coast; }
            set
            {
                _Coast = value;
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


        public UploadViewModel()
        {
            UpladItemsCommand = new Command(async () => { await UpladItems(); });
            Images = new ObservableCollection<ImagesModel>();
        }

        public UploadViewModel(string v)
        {
            this.v = v;
            UpladItemsCommand = new Command(async () => { await ayhaga(); });
            StoreItemsCommand = new Command(async () => { await StoreItems(); });
            SelectCategoryCommand = new Command<Category>(SelectCategory);
            DeleteItemCommand = new Command<ImagesModel>(RemoveImage);
            Images = new ObservableCollection<ImagesModel>();
            category = new ObservableCollection<Category>();
            category.Add(new Category() { name = "Girls" });
            category.Add(new Category() { name = "Boys" });
            category.Add(new Category() { name = "Education" });
            category.Add(new Category() { name = "Clothes" });
            category.Add(new Category() { name = "Entertaining" });
            urls = new List<string>();
        }

        public void SelectCategory(Category obj)
        {
            if (obj.IsCheck)
            {
                obj.IsCheck = false;
            }
            else { obj.IsCheck = true; }
        }

        private void RemoveImage(ImagesModel obj)
        {
            Images.Remove(obj);
        }

        private async Task StoreItems()
        {
            IsLoading = true;
            try
            {
                foreach (var j in Images)
                {
                    string url = await firebaseStorageHelper.UploadFile(j.imgStream, v, j.NameWithoutExtension);
                    urls.Add(url);
                }
              

                ItemsModel items = new ItemsModel()
                {
                    description = Description,
                    coast = Coast,
                    name = Name,
                    imgUrls = urls,
                    category = SelectedCategory.name
                };
                var i = CrossCloudFirestore.Current
                                         .Instance
                                         .Collection(v)
                                         .AddAsync(items)
                                         .ContinueWith(t =>
                                         {
                                             System.Diagnostics.Debug.WriteLine(t.Exception);
                                         }, TaskContinuationOptions.OnlyOnFaulted);
                if (i.Status == TaskStatus.WaitingForActivation)
                {
                    await Application.Current.MainPage.DisplayAlert("", "Images uploaded successfully", "Ok");
                    Images.Clear();
                    Description = "";
                    Name = "";
                    Coast = 0;
                    SelectedCategory = null;
                    urls.Clear();
                }
                IsLoading = false;
            }
            catch(Exception ex)
            {
                IsLoading = false;
            }
            
        }

        private async Task UpladItems()
        {
            try
            {
                //var results = await MediaGallery.PickAsync(3, MediaFileType.Image, MediaFileType.Video);

                //if (results?.Files == null)
                //{
                //    return;
                //}

                //foreach (var media in results.Files)
                //{
                //    var fileName = media.NameWithoutExtension+"."+media.Extension;
                //    var extension = media.Extension;
                //    var contentType = media.ContentType;
                //    Stream imageStram = await media.OpenReadAsync();
                //   // Stream stream1 = await media.OpenReadAsync();
                //    FileStream newStream;
                //    FileStream fileStream = null;

                    //var newFile = Path.Combine(FileSystem.CacheDirectory, media.NameWithoutExtension +"."+ media.Extension);
                    //using (var stream = await media.OpenReadAsync())
                    //using (newStream = File.OpenWrite(newFile))
                    //await stream.CopyToAsync(newStream);
                    //var PhotoPath = newFile;
                    //Stream stream = null;
                    //var newFile = Path.Combine(FileSystem.CacheDirectory, media.NameWithoutExtension + "." + media.Extension);
                    //using(stream = await media.OpenReadAsync())

                    //using (newStream = File.OpenWrite(newFile))
                    //{
                    //    await stream.CopyToAsync(newStream);
                    //}

                    //string fileNameq = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), media.NameWithoutExtension + media.Extension);

                    //using (fileStream = new FileStream(fileNameq, FileMode.Create, FileAccess.Write))
                    //{
                    //    imageStram.CopyTo(fileStream);
                    //}
                    //MemoryStream ms = new MemoryStream();
                    //using (FileStream file = new FileStream(fileNameq, FileMode.Open, FileAccess.Read))
                    //    imageStram.CopyTo(ms);

                    //var arr = ms.ToArray();

                    //var contentSignature = (new MemoryStream(arr, 0, arr.Length), "SignatureForm");
                    //var contentSignature = ToStreamContent(new MemoryStream(arr, 0, arr.Length), "SignatureForm");

                    //Images.Add(new ImagesModel
                    //{
                    //    NameWithoutExtension = fileName,
                    //    Extension = extension,
                    //    ContentType = contentType,
                    //    imgStream = imageStram,
                    //    imgSource = ImageSource.FromStream(() => { return imageStram; })
                    //});
               // }
            }
            catch(Exception ex)
            {

            }
        }

        //public static Stream ToStreamContent(this MemoryStream stream, string fieldName)
        //{
        //    var contentType = "application/octet-stream";
        //    var fileName = Path.GetFileName("SignatureForm.jpg");
        //    Stream fileContent = new Stream(stream);
        //    //fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //    //{
        //    //    Name = $"\"{ fieldName}\"",
        //    //    FileName = $"\"{ fileName}\""
        //    //};
        //    //fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        //    return fileContent;
        //}

        async Task ayhaga()
        {
            await CrossMedia.Current.Initialize();
            IsLoading = true;

            try
            {
                //var file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                //{
                //     Title="Fuck you"
                //});
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                    
                });
                var fileNameWithExtenstion = Path.GetFileName(file.Path);
                if (file == null)
                    return;
                
                Images.Add(new ImagesModel
                {
                    NameWithoutExtension = fileNameWithExtenstion,
                    imgStream = file.GetStream(),
                    imgSource = ImageSource.FromStream(() => { return file.GetStream(); })
                });
                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;
                // Debug.WriteLine(ex.Message);
            }
        }
        //public async Task<string> UploadFile(Stream fileStream, string fileName)
        //{
        //    var imageUrl = await firebaseStorage
        //        .Child("XamarinMonkeys")
        //        .Child(fileName)
        //        .PutAsync(fileStream);
        //    return imageUrl;
        //}
        //public async Task<string> UploadFile(Stream fileStream, string fileName)
        //{
        //    try
        //    {
        //        var imageUrl = await firebaseStorage
        //         .Child("XamarinMonkeys")
        //         .Child(fileName)
        //         .PutAsync(fileStream);
        //        return imageUrl;
        //    }
        //    catch(Exception e)
        //    {
        //        return "";
        //    }
        //}

    }
    public class Category : INotifyPropertyChanged
    {
        public string name { get; set; }
        public bool _IsCheck = false;
        public bool IsCheck
        {
            get
            {
                return _IsCheck;
            }
            set
            {
                _IsCheck = value;
                this.OnPropertyChanged("IsCheck");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}