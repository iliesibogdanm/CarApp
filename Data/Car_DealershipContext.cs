using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Car_Dealership.Models;

namespace Car_Dealership.Data
{
    public class Car_DealershipContext : DbContext
    {
        public Car_DealershipContext (DbContextOptions<Car_DealershipContext> options)
            : base(options)
        {
        }

        public DbSet<Car_Dealership.Models.Car> Car { get; set; }

        public DbSet<Car_Dealership.Models.Seller> Seller { get; set; }

        public DbSet<Car_Dealership.Models.Category> Category { get; set; }
    }
}
