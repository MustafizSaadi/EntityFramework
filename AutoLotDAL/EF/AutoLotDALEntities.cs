using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using AutoLotDAL.Models;
using AutoLotDAL.Interception;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace AutoLotDAL.EF
{
    public partial class AutoLotDALEntities : DbContext
    {
        static readonly DatabaseLogger DatabaseLogger =
            new DatabaseLogger("sqllog.txt", true);
        public AutoLotDALEntities()
            : base("name=AutoLotDALEntities")
        {
            //DbInterception.Add(new ConsoleWriterInterceptor());
            //DatabaseLogger.StartLogging();
            //DbInterception.Add(DatabaseLogger);

            //Interceptor code
            var context = (this as IObjectContextAdapter).ObjectContext;
            context.ObjectMaterialized += OnObjectMaterialized;
            context.SavingChanges += OnSavingChanges;
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Car>().WillCascadeOnDelete();
        }
        protected override void Dispose(bool disposing)
        {
            DbInterception.Remove(DatabaseLogger);
            DatabaseLogger.StopLogging();
            base.Dispose(disposing);
        }

        private void OnSavingChanges(object sender, EventArgs eventArgs)
        {
        }
        private void OnObjectMaterialized(object sender,
        System.Data.Entity.Core.Objects.ObjectMaterializedEventArgs e)
        {
            //Sender is of type ObjectContext. Can get current and original values, and
            //cancel/modify the save operation as desired.
            var context = sender as ObjectContext;
            if (context == null) return;
            foreach (ObjectStateEntry item in
            context.ObjectStateManager.GetObjectStateEntries(
            EntityState.Modified | EntityState.Added))
            {
                //Do something important here
                if ((item.Entity as Inventory) != null)
                {
                    var entity = (Inventory)item.Entity;
                    if (entity.Color == "Red")
                    {
                        item.RejectPropertyChanges(nameof(entity.Color));
                    }
                }
            }
        }
    }
}
