using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilyFishMVC.Models
{
    public class UserProfile
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Mname { get; set; }
        public int StreetNum { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; } 
        public string HPhone { get; set; }
        public string CPhone { get; set; }

    }
}