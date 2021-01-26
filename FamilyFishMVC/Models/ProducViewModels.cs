using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilyFishMVC.Models
{
    public class ProducViewModels: Product
    {
        public List<int> CategoryIds { get; set; }
    }
}