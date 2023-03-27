using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealProject.Models
{
    public class DealsVM
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string Update_DateTime_UTC { get; set; }
        public string Server_DateTime { get; set; }
        public string DateTime_UTC { get; set; }

    }
}