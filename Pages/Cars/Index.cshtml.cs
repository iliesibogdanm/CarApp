using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Car_Dealership.Data;
using Car_Dealership.Models;

namespace Car_Dealership.Pages.Cars
{
    public class IndexModel : CarCategoriesPageModel
    {
        private readonly Car_Dealership.Data.Car_DealershipContext _context;

        public IndexModel(Car_Dealership.Data.Car_DealershipContext context)
        {
            _context = context;
        }

        public IList<Car> Car { get;set; }
        public CarData CarD { get; set; }
        public int CarID { get; set; }
        public int CategoryID { get; set; }



        public async Task OnGetAsync(int? id,int? categoryID)
        {
            CarD = new CarData();
            CarD.Cars = await _context.Car.Include(b => b.Seller).Include(b => b.CarCategories).ThenInclude(b => b.Category).AsNoTracking().OrderBy(b => b.Name).ToListAsync();

            if (id != null)
            {
                CarID = id.Value;
                Car car = CarD.Cars.Where(i => i.ID == id.Value).Single();
                CarD.Categories = car.CarCategories.Select(s => s.Category);
            }
        }
    }
}
