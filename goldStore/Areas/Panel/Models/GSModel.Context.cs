﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class goldstoreEntities : DbContext
    {
        public goldstoreEntities()
            : base("name=goldstoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<brand> brand { get; set; }
        public virtual DbSet<category> category { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<productImage> productImage { get; set; }
        public virtual DbSet<role> role { get; set; }
        public virtual DbSet<user> user { get; set; }
        public virtual DbSet<orderDetails> orderDetails { get; set; }
        public virtual DbSet<orders> orders { get; set; }
        public virtual DbSet<wishlist> wishlist { get; set; }
        public virtual DbSet<coupons> coupons { get; set; }
    }
}
