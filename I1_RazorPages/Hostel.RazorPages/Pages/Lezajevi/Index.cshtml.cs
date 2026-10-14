using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hostel.RazorPages.Data;
using Hostel.RazorPages.Models;
namespace Hostel.RazorPages.Pages.Lezajevi
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) => _context = context;
        public List<Lezaj> Lezaji { get; set; } = new();
        public async Task OnGetAsync() => Lezaji = await _context.Lezaji.ToListAsync();
    }
}
