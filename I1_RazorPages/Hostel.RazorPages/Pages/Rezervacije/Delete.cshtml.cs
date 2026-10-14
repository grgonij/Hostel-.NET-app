using Microsoft.AspNetCore.Mvc; using Microsoft.AspNetCore.Mvc.RazorPages;
using Hostel.RazorPages.Data; using Hostel.RazorPages.Models;
namespace Hostel.RazorPages.Pages.Rezervacije
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DeleteModel(ApplicationDbContext context) => _context = context;
        public Rezervacija? Rezervacija { get; set; }
        public async Task<IActionResult> OnGetAsync(int id) { Rezervacija = await _context.Rezervacije.FindAsync(id); if (Rezervacija == null) return NotFound(); return Page(); }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var rez = await _context.Rezervacije.FindAsync(id);
            if (rez != null) { _context.Rezervacije.Remove(rez); await _context.SaveChangesAsync(); TempData["Poruka"] = $"Rezervacija za '{rez.ImeGosta}' obrisana."; }
            return RedirectToPage("Index");
        }
    }
}
