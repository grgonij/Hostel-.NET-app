using Microsoft.AspNetCore.Mvc; using Microsoft.AspNetCore.Mvc.RazorPages; using Microsoft.EntityFrameworkCore;
using Hostel.RazorPages.Data; using Hostel.RazorPages.Models;
namespace Hostel.RazorPages.Pages.Lezajevi
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context) => _context = context;
        [BindProperty] public Lezaj Lezaj { get; set; } = new();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var lezaj = await _context.Lezaji.FindAsync(id);
            if (lezaj == null) return NotFound();
            Lezaj = lezaj; return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            _context.Attach(Lezaj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            TempData["Poruka"] = $"Ležaj '{Lezaj.Naziv}' ažuriran!";
            return RedirectToPage("Index");
        }
    }
}
