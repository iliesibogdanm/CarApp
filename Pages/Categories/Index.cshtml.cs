using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Car_Dealership.Data;
using Car_Dealership.Models;

namespace Car_Dealership.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Car_Dealership.Data.Car_DealershipContext _context;

        public IndexModel(Car_Dealership.Data.Car_DealershipContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}
