using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VikLearnsToCook.Data;

namespace VikLearnsToCook.ViewComponents
{
    public class RestaurantCountViewComponent
        :ViewComponent
    {
        private readonly IRestaurantData restaurant;

        public RestaurantCountViewComponent(IRestaurantData restaurant)
        {
            this.restaurant = restaurant;
        }

        public  IViewComponentResult Invoke()
        {
            var count = restaurant.GetCountOfRestaurants();
            return View(count);
        }
    }
}
