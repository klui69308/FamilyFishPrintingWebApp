using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FamilyFishMVC.Models
{
    public partial class Product
    {
        public string GetImagePath(HttpContextBase context, bool checkExist = true)
        {
            string imagePath = "~/ProductImages/" + "stockPhoto" + Id + ".png";
            var filePath = context.Server.MapPath(imagePath);
            if (!File.Exists(filePath) && checkExist)
            {
                imagePath = "~/ProductImages/" + "yourPhotoHere.jpg";
            }

            return imagePath;
        }
    }
}