using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using lengkeng.Models;

namespace lengkeng.DataAccessLayer
{
    public class DBContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<DoContact> DoContacts { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<News> Newss { get; set; }
        public DbSet<SteptoCreate> SteptoCreates { get; set; }
        public DbSet<Welcome> Welcomes { get; set; }
        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Entity<Account>().ToTable("Account");
            dbModelBuilder.Entity<Category>().ToTable("Category");
            dbModelBuilder.Entity<Item>().ToTable("Item");
            dbModelBuilder.Entity<Price>().ToTable("Price");
            dbModelBuilder.Entity<Promotion>().ToTable("Promotion");
            dbModelBuilder.Entity<DoContact>().ToTable("DoContact");
            dbModelBuilder.Entity<Contact>().ToTable("Contact");
            dbModelBuilder.Entity<About>().ToTable("About");
            dbModelBuilder.Entity<Feature>().ToTable("Feature");
            dbModelBuilder.Entity<News>().ToTable("News");
            dbModelBuilder.Entity<SteptoCreate>().ToTable("SteptoCreate");
            dbModelBuilder.Entity<Welcome>().ToTable("Welcome");
            base.OnModelCreating(dbModelBuilder);
        }

        public System.Data.Entity.DbSet<lengkeng.Models.YoutubeHome> YoutubeHomes { get; set; }

        public System.Data.Entity.DbSet<lengkeng.Models.Service> Services { get; set; }
    }
}