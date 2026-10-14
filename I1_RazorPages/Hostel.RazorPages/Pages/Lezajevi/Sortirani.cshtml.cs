using Microsoft.AspNetCore.Mvc; using Microsoft.AspNetCore.Mvc.RazorPages; using Microsoft.EntityFrameworkCore;
using Hostel.RazorPages.Data; using Hostel.RazorPages.Models;
namespace Hostel.RazorPages.Pages.Lezajevi
{
    public class SortiraniModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public SortiraniModel(ApplicationDbContext context) => _context = context;
        public List<Lezaj> Lezaji { get; set; } = new();
        [BindProperty(SupportsGet = true)] public string Kljuc { get; set; } = "naziv";
        public async Task OnGetAsync()
        {
            Lezaji = Kljuc switch {
                "cijena" => await _context.Lezaji.OrderBy(l => l.CijenaPoNoci).ToListAsync(),
                "soba" => await _context.Lezaji.OrderBy(l => l.BrojSobe).ToListAsync(),
                _ => await _context.Lezaji.OrderBy(l => l.Naziv).ToListAsync()
            };
        }
    }
}
