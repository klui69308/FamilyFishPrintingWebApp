using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilyFishMVC.Models
{
    public partial class Product
    {
        public string GetImagePath()
        {
            string imagePath = "~/ProductImages/" + "stockPhoto" + Id + ".png";
            //To do handle missing file
            //string fileSystemPath = 

            return imagePath;
        }
    }
}