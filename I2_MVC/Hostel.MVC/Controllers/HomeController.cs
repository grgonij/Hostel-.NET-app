using Microsoft.AspNetCore.Mvc;
using Hostel.MVC.Services;
namespace Hostel.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILezajService _lezajService;
        private readonly IRezervacijaService _rezervacijaService;
        private readonly IKorisnikService _korisnikService;
        public HomeController(ILezajService l, IRezervacijaService r, IKorisnikService k) { _lezajService=l; _rezervacijaService=r; _korisnikService=k; }
        public async Task<IActionResult> Index()
        {
            var svi = await _lezajService.GetAllAsync();
            var dostupni = await _lezajService.GetDostupniAsync();
            var aktivne = await _rezervacijaService.GetAktivneAsync();
            var nadolazece = await _rezervacijaService.GetNadolazecePrijaveAsync(7);
            ViewBag.UkupnoLezajeva = svi.Count();
            ViewBag.DostupnoLezajeva = dostupni.Count();
            ViewBag.AktivnihRezervacija = aktivne.Count();
            ViewBag.NadolazecePrijave = nadolazece.Count();
            ViewBag.DostupniLezaji = dostupni.Take(3);
            return View();
        }
        public IActionResult Privacy() => View();
    }
}
