using Microsoft.AspNetCore.Mvc; using Microsoft.AspNetCore.Mvc.RazorPages; using Microsoft.EntityFrameworkCore;
using Hostel.RazorPages.Data; using Hostel.RazorPages.Models;
namespace Hostel.RazorPages.Pages.Lezajevi
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DetailsModel(ApplicationDbContext context) => _context = context;
        public Lezaj? Lezaj { get; set; }
        public List<Rezervacija> Rezervacije { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Lezaj = await _context.Lezaji.FindAsync(id);
            if (Lezaj == null) return NotFound();
            Rezervacije = await _context.Rezervacije.Where(r => r.LezajId == id).OrderByDescending(r => r.DatumPrijave).ToListAsync();
            return Page();
        }
    }
}
