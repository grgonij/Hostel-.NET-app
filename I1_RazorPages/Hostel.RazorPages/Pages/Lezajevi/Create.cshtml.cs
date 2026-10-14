using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hostel.RazorPages.Data;
using Hostel.RazorPages.Models;
namespace Hostel.RazorPages.Pages.Lezajevi
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CreateModel(ApplicationDbContext context) => _context = context;
        [BindProperty] public Lezaj Lezaj { get; set; } = new();
        public IActionResult OnGet() => Page();
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            Lezaj.DatumDodavanja = DateTime.Now;
            _context.Lezaji.Add(Lezaj);
            await _context.SaveChangesAsync();
            TempData["Poruka"] = $"Ležaj '{Lezaj.Naziv}' uspješno je dodan!";
            return RedirectToPage("Index");
        }
    }
}
