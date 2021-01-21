using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FamilyFishMVC.Models
{
    [MetadataType(typeof(CustomerAnnotation))]
    public partial class Customer
    {
    }
    public class CustomerAnnotation
    {
        [Display(Name = "First Name")]
        public string Fname { get; set; }
        [Display(Name = "Middle Name")]
        public string Mname { get; set; }
        [Display(Name = "Last Name")]
        public string Lname { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Home Phone #")]
        public string HPhone { get; set; }
        [Display(Name = "Cell Phone #")]
        public string CPhone { get; set; }
        [Display(Name = "Street Number")]
        public string StreetNum { get; set; }
        [Display(Name = "Street Name")]
        public string StreetName { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Zip")]
        public string Zip { get; set; }

    }
}