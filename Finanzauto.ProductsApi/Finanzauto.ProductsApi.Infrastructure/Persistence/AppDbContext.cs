using Finanzauto.ProductsApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Finanzauto.ProductsApi.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options) {
            try
            {
                var dbcreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbcreator != null)
                {
                    Console.WriteLine($"orojasli paso 0 :" + dbcreator.ToString() + " / " + dbcreator);
                    if (!dbcreator.CanConnect())
                    {
                        //Console.WriteLine($"orojasli paso 1");
                        dbcreator.Create();
                    }
                 
                    if (dbcreator.HasTables())
                    {
                        //Console.WriteLine($"orojasli paso 2");
                        dbcreator.CreateTables();

                    }
                        
                    //Console.WriteLine($"orojasli paso 3");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB error: {ex.Message}");
            }                   
        }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();

     
    }
}
