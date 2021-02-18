using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Car_Dealership.Data;
using Car_Dealership.Models;

namespace Car_Dealership.Pages.Cars
{
    public class CreateModel : CarCategoriesPageModel
    {
        private readonly Car_Dealership.Data.Car_DealershipContext _context;

        public CreateModel(Car_Dealership.Data.Car_DealershipContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["SellerID"] = new SelectList(_context.Set<Seller>(), "ID", "SellerName");
            var car = new Car();
            car.CarCategories = new List<CarCategory>();
            PopulateAssignedCategoryData(_context, car);
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newCar = new Car();
            if (selectedCategories != null)
            {
                newCar.CarCategories = new List<CarCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new CarCategory { CategoryID=int.Parse(cat) };
                    newCar.CarCategories.Add(catToAdd);
                }
            }

            if (await TryUpdateModelAsync<Car>(newCar,"Car",i =>i.Name,i =>i.Make,i => i.Price, i => i.MaxSpeed, i => i.ListingDate, i => i.SellerID,i=>i.Seller))
            {
                _context.Car.Add(newCar);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedCategoryData(_context, newCar);
            return Page();
        }
    }
}
