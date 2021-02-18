using Microsoft.AspNetCore.Mvc.RazorPages;
using Car_Dealership.Data;
using System.Collections.Generic;
using System.Linq;

namespace Car_Dealership.Models
{

    public class CarCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryClass> AssignedCategoryClassList;

        public void PopulateAssignedCategoryData(Car_DealershipContext context, Car car)
        {
            var allCategories = context.Category;
            var carCategories = new HashSet<int>(car.CarCategories.Select(c => c.CarID));
            AssignedCategoryClassList = new List<AssignedCategoryClass>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryClassList.Add(new AssignedCategoryClass
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = carCategories.Contains(cat.ID)
                });
            }
        }

        public void UpdateCarCategories(Car_DealershipContext context, string[] selectedCategories, Car carToUpdate)
        {
            if (selectedCategories == null)
            {
                carToUpdate.CarCategories = new List<CarCategory>();
                return;
            }
            //
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var carCategories = new HashSet<int>(carToUpdate.CarCategories.Select(c => c.Category.ID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!carCategories.Contains(cat.ID))
                    {
                        carToUpdate.CarCategories.Add(new CarCategory
                        {
                            CarID = carToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (carCategories.Contains(cat.ID))
                    {
                        CarCategory carToRemove = carToUpdate.CarCategories.SingleOrDefault(i => i.CategoryID == cat.ID);
                        context.Remove(carToRemove);
                    }
                }
            }
        }
    }
}
