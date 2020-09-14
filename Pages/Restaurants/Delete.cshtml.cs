using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VikLearnsToCook.Core;
using VikLearnsToCook.Data;

namespace VikLearnsToCook.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        public Restaurant Restaurant { get; set; }

        public DeleteModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }
        public IActionResult OnGet(int restaurantID)
        {
            Restaurant = restaurantData.GetByID(restaurantID);
            if(Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(int restaurantID)
        {
            var restaurant = restaurantData.Delete(restaurantID);
            restaurantData.Commit();
            if(restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            TempData["Message"] = $"{restaurant.Name} deleted";
                 return RedirectToPage("./List");
        }
    }
}
