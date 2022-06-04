
using System;
using System.Collections.Generic;
using System.Text;

namespace AboutTheKids.Models
{
    public class ItemsModel
    {
        public double coast { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string image { get; set; }
        public List<string> imgUrls { get; set; }
        public string documentID { get; set; }

        public static string CollectionPath = "todoItems";
    }
}
