//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace goldStore.Areas.Panel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class coupons
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string coupon { get; set; }
        public Nullable<decimal> discount { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public Nullable<System.DateTime> expired { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<int> userId { get; set; }
    
        public virtual user user { get; set; }
    }
}
