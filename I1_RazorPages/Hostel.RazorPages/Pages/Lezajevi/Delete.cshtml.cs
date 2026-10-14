using Microsoft.AspNetCore.Mvc; using Microsoft.AspNetCore.Mvc.RazorPages;
using Hostel.RazorPages.Data; using Hostel.RazorPages.Models;
namespace Hostel.RazorPages.Pages.Lezajevi
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DeleteModel(ApplicationDbContext context) => _context = context;
        public Lezaj? Lezaj { get; set; }
        public async Task<IActionResult> OnGetAsync(int id) { Lezaj = await _context.Lezaji.FindAsync(id); if (Lezaj == null) return NotFound(); return Page(); }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var lezaj = await _context.Lezaji.FindAsync(id);
            if (lezaj != null) { _context.Lezaji.Remove(lezaj); await _context.SaveChangesAsync(); TempData["Poruka"] = $"Ležaj '{lezaj.Naziv}' obrisan."; }
            return RedirectToPage("Index");
        }
    }
}
