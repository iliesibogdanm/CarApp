using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Car_Dealership.Data;
using Car_Dealership.Models;


namespace Car_Dealership.Pages.Cars
{
    public class EditModel : CarCategoriesPageModel
    {
        private readonly Car_Dealership.Data.Car_DealershipContext _context;

        public EditModel(Car_Dealership.Data.Car_DealershipContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Car Car { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Car = await _context.Car.Include(b => b.Seller)
                .Include(b => b.CarCategories).ThenInclude(b => b.Category)
                .AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (Car == null)
            {
                return NotFound();
            }

            PopulateAssignedCategoryData(_context, Car);

            ViewData["SellerID"] = new SelectList(_context.Set<Seller>(), "ID", "SellerName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null) { return NotFound(); }

            var carToUpdate = await _context.Car.Include(i => i.Seller).Include(i => i.CarCategories)
                .ThenInclude(i => i.Category).FirstOrDefaultAsync(s => s.ID == id);

            if(carToUpdate == null)
            {
                return NotFound();
            }

            if(await TryUpdateModelAsync<Car>(carToUpdate,"Car",i =>i.Name,i=>i.Make,i => i.Price ,i => i.MaxSpeed , i =>i.ListingDate,i => i.Seller))
            {
                UpdateCarCategories(_context, selectedCategories, carToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateCarCategories(_context, selectedCategories, carToUpdate);
            PopulateAssignedCategoryData(_context, carToUpdate);
            return Page();
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.ID == id);
        }
    }
}
