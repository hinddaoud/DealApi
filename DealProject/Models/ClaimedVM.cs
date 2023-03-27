using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealProject.Models
{
    public class ClaimedVM
    {

       

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }
        public int User_ID { get; set; }
        public int Deal_ID { get; set; }
      
        public string Server_DateTime { get; set; }
        public string DateTime_UTC { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }

        public List<DealsVM> deals{ get; set; }
        public List<UsersVM> uses { get; set; }
    }
}