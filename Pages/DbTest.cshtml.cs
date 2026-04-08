using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ryadevn.Data;

namespace Ryadevn.Pages
{
    public class DbTestModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public DbTestModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ApplicationDbContext DbContext => _dbContext;
        public List<Product> Products { get; set; } = new();

        [BindProperty]
        public Product NewProduct { get; set; } = new();

        public async Task OnGetAsync()
        {
            Products = await _dbContext.Products.ToListAsync();
        }

        public async Task<IActionResult> OnPostAddProductAsync()
        {
            if (!ModelState.IsValid)
            {
                Products = await _dbContext.Products.ToListAsync();
                return Page();
            }

            _dbContext.Products.Add(NewProduct);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}