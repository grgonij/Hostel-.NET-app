using Microsoft.AspNetCore.Mvc; using Microsoft.AspNetCore.Mvc.RazorPages; using Microsoft.EntityFrameworkCore;
using Hostel.RazorPages.Data; using Hostel.RazorPages.Models;
namespace Hostel.RazorPages.Pages.Lezajevi
{
    public class FiltrirajModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public FiltrirajModel(ApplicationDbContext context) => _context = context;
        public List<Lezaj> Rezultati { get; set; } = new();
        [BindProperty(SupportsGet = true)] public TipSobe? Tip { get; set; }
        [BindProperty(SupportsGet = true)] public decimal? CijenaOd { get; set; }
        [BindProperty(SupportsGet = true)] public decimal? CijenaDo { get; set; }
        [BindProperty(SupportsGet = true)] public bool SamoDostupni { get; set; }
        public async Task OnGetAsync()
        {
            var query = _context.Lezaji.AsQueryable();
            if (Tip.HasValue) query = query.Where(l => l.TipSobe == Tip.Value);
            if (CijenaOd.HasValue) query = query.Where(l => l.CijenaPoNoci >= CijenaOd.Value);
            if (CijenaDo.HasValue) query = query.Where(l => l.CijenaPoNoci <= CijenaDo.Value);
            if (SamoDostupni) query = query.Where(l => l.Dostupan);
            Rezultati = await query.ToListAsync();
        }
    }
}
