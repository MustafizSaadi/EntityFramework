using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using AutoLotDAL.Models;

namespace AutoLotDAL.EF
{
    public partial class AutoLotDALEntities : DbContext
    {
        public AutoLotDALEntities()
            : base("name=AutoLotDALEntities")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Car>().WillCascadeOnDelete();
        }
    }
}
