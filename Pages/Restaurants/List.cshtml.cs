using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VikLearnsToCook.Core;
using VikLearnsToCook.Data;

namespace VikLearnsToCook.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        public string Message { get; set; }
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurantData;
        private readonly ILogger<ListModel> logger;

        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public IEnumerable<Restaurant> Restaurants{ get; set; }

        public ListModel(IConfiguration config, 
                         IRestaurantData restaurantData,
                         ILogger<ListModel> logger)
        {
            this.config = config;
            this.restaurantData = restaurantData;
            this.logger = logger;
        }
        public void OnGet()
        {
            logger.LogError("I am testing logging functionality");
            Message = config["Message"];
            Restaurants = restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}
