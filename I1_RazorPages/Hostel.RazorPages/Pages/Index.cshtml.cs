using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hostel.RazorPages.Data;
using Hostel.RazorPages.Models;
namespace Hostel.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) => _context = context;
        public int UkupnoLezajeva { get; set; }
        public int DostupnoLezajeva { get; set; }
        public int ZauzetihLezajeva { get; set; }
        public int AktivnihRezervacija { get; set; }
        public List<Lezaj> DostupniLezajevi { get; set; } = new();
        public async Task OnGetAsync()
        {
            UkupnoLezajeva = await _context.Lezaji.CountAsync();
            DostupnoLezajeva = await _context.Lezaji.CountAsync(l => l.Dostupan);
            ZauzetihLezajeva = UkupnoLezajeva - DostupnoLezajeva;
            AktivnihRezervacija = await _context.Rezervacije.CountAsync(r => r.Status == StatusRezervacije.Aktivna);
            DostupniLezajevi = await _context.Lezaji.Where(l => l.Dostupan).Take(6).ToListAsync();
        }
    }
}
