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
            string imagePath;
            if (Id <= 6)
            {
                imagePath = "~/ProductImages/" + "stockPhoto" + Id + ".png";
            }
            else
            {
                imagePath = "~/ProductImages/" + "yourPhotoHere.jpg";
            }
            
            //To do handle missing file
            //string fileSystemPath = 

            return imagePath;
        }
    }
}