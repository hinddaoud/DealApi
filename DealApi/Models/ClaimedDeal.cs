//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DealApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClaimedDeal
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public int Deal_ID { get; set; }
        public Nullable<System.DateTime> Server_DateTime { get; set; }
        public Nullable<System.DateTime> DateTime_UTC { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
    
        public virtual Deal Deal { get; set; }
        public virtual User User { get; set; }
    }
}
