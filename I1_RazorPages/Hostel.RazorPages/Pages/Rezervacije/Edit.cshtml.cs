using Microsoft.AspNetCore.Mvc; using Microsoft.AspNetCore.Mvc.RazorPages; using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; using Hostel.RazorPages.Data; using Hostel.RazorPages.Models;
namespace Hostel.RazorPages.Pages.Rezervacije
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context) => _context = context;
        [BindProperty] public Rezervacija Rezervacija { get; set; } = new();
        public SelectList LezajiSelectList { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var rez = await _context.Rezervacije.FindAsync(id);
            if (rez == null) return NotFound();
            Rezervacija = rez; await UcitajLezajeAsync(); return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { await UcitajLezajeAsync(); return Page(); }
            _context.Attach(Rezervacija).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            TempData["Poruka"] = "Rezervacija ažurirana!"; return RedirectToPage("Index");
        }
        private async Task UcitajLezajeAsync() { var l = await _context.Lezaji.ToListAsync(); LezajiSelectList = new SelectList(l, "Id", "Naziv"); }
    }
}
