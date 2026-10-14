using Microsoft.AspNetCore.Mvc.RazorPages; using Microsoft.EntityFrameworkCore;
using Hostel.RazorPages.Data; using Hostel.RazorPages.Models;
namespace Hostel.RazorPages.Pages.Rezervacije
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) => _context = context;
        public List<Rezervacija> Rezervacije { get; set; } = new();
        public async Task OnGetAsync() => Rezervacije = await _context.Rezervacije.Include(r => r.Lezaj).OrderByDescending(r => r.DatumPrijave).ToListAsync();
    }
}
