using Microsoft.AspNetCore.Mvc; using Microsoft.AspNetCore.Mvc.RazorPages; using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; using Hostel.RazorPages.Data; using Hostel.RazorPages.Models;
namespace Hostel.RazorPages.Pages.Rezervacije
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CreateModel(ApplicationDbContext context) => _context = context;
        [BindProperty] public Rezervacija Rezervacija { get; set; } = new();
        public SelectList LezajiSelectList { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int? lezajId)
        {
            await UcitajLezajeAsync();
            if (lezajId.HasValue) Rezervacija.LezajId = lezajId.Value;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { await UcitajLezajeAsync(); return Page(); }
            _context.Rezervacije.Add(Rezervacija);
            await _context.SaveChangesAsync();
            TempData["Poruka"] = $"Rezervacija za '{Rezervacija.ImeGosta}' dodana!";
            return RedirectToPage("Index");
        }
        private async Task UcitajLezajeAsync()
        {
            var lezaji = await _context.Lezaji.Where(l => l.Dostupan).ToListAsync();
            LezajiSelectList = new SelectList(lezaji, "Id", "Naziv");
        }
    }
}
