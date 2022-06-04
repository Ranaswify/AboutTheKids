using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace AboutTheKids.Models
{
    public class ImagesModel
    {
        public string NameWithoutExtension { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public Stream imgStream { get; set; }

        public ImageSource imgSource { get; set; }
    }
}
