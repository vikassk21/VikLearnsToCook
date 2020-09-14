using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VikLearnsToCook.Core;
using VikLearnsToCook.Data;

namespace VikLearnsToCook.Pages.R2
{
    public class IndexModel : PageModel
    {
        private readonly VikLearnsToCook.Data.VikLearnsToCookDbContext _context;

        public IndexModel(VikLearnsToCook.Data.VikLearnsToCookDbContext context)
        {
            _context = context;
        }

        public IList<Restaurant> Restaurant { get;set; }

        public async Task OnGetAsync()
        {
            Restaurant = await _context.Restaurants.ToListAsync();
        }
    }
}
