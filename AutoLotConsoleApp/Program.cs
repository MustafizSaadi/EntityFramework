using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotConsoleApp.EF;

namespace AutoLotConsoleApp
{
    internal class Program
    {
        private static void PrintAllInventory()
        {
            // Select all items from the Inventory table of AutoLot,
            // and print out the data using our custom ToString() of the Car entity class.
            using (var context = new AutoLotEntities())
            {
                foreach (Car c in context.Cars)
                {
                    Console.WriteLine(c);
                }
            }
        }
        private static int AddNewRecord()
        {
            // Add record to the Inventory table of the AutoLot database.
            using (var context = new AutoLotEntities())
            {
                try
                {
                    // Hard-code data for a new record, for testing.
                    var car = new Car() { Make = "Bongio", Color = "Red", CarNickName = "Rossy" };
                    context.Cars.Add(car);
                    context.SaveChanges();
                    // On a successful save, EF populates the database generated identity field.
                    return car.CarId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    return 0;
                }
            }
        }

        private static void FunWithLinqQueries()
        {
            using (var context = new AutoLotEntities())
            {
                // Get a projection of new data.
                var colorsMakes = from item in context.Cars select new { item.Color, item.Make };
                foreach (var item in colorsMakes)
                {
                    Console.WriteLine(item);
                }
                // Get only items where Color == "Black"
                var blackCars = from item in context.Cars where item.Color == "Black" select item;
                foreach (var item in blackCars)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine(context.Cars.Find(5));
            }
        }

        private static void LazyLoading()
        {
            using (var context = new AutoLotEntities())
            {
                DateTime dateTime = DateTime.Now;
                foreach (Car c in context.Cars)
                {
                    foreach (Order o in c.Orders)
                    {
                        Console.WriteLine(o.OrderId);
                    }
                }

                Console.WriteLine((DateTime.Now - dateTime).TotalMilliseconds);
            }
        }

        private static void EagerLoading()
        {
            using (var context = new AutoLotEntities())
            {
                DateTime dateTime = DateTime.Now;
                foreach (Car c in context.Cars.Include("Orders"))
                {
                    foreach (Order o in c.Orders)
                    {
                        Console.WriteLine(o.OrderId);
                    }
                }
                Console.WriteLine((DateTime.Now - dateTime).TotalMilliseconds);
            }
        }

        private static void ExplicitLoading()
        {
            using (var context = new AutoLotEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                foreach (Car c in context.Cars)
                {
                    context.Entry(c).Collection(x => x.Orders).Load();
                    foreach (Order o in c.Orders)
                    {
                        Console.WriteLine(o.OrderId);
                    }
                }
                foreach (Order o in context.Orders)
                {
                    context.Entry(o).Reference(x => x.Inventory).Load();
                }
            }
        }

        private static void RemoveRecord(int carId)
        {
            // Find a car to delete by primary key.
            using (var context = new AutoLotEntities())
            {
                // See if we have it.
                Car carToDelete = context.Cars.Find(carId);
                if (carToDelete != null)
                {
                    context.Cars.Remove(carToDelete);
                    //This code is purely demonstrative to show the entity state changed to Deleted
                    if (context.Entry(carToDelete).State != EntityState.Deleted)
                    {
                        throw new Exception("Unable to delete the record");
                    }
                    context.SaveChanges();
                }
            }
        }

        private static void UpdateRecord(int carId)
        {
            // Find a car to delete by primary key.
            using (var context = new AutoLotEntities())
            {
                // Grab the car, change it, save!
                Car carToUpdate = context.Cars.Find(carId);
                if (carToUpdate != null)
                {
                    Console.WriteLine(context.Entry(carToUpdate).State);
                    carToUpdate.Color = "Blue";
                    Console.WriteLine(context.Entry(carToUpdate).State);
                    context.SaveChanges();
                }
            }
        }



        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with ADO.NET EF *****\n");
            //int carId = AddNewRecord();
            //Console.WriteLine(carId);

            //PrintAllInventory();

            //FunWithLinqQueries();

            //LazyLoading();

            //EagerLoading();

            //RemoveRecord(3);

            //UpdateRecord(4);
        }
    }
}
