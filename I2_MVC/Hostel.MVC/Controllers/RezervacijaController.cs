using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hostel.MVC.Models;
using Hostel.MVC.Services;
namespace Hostel.MVC.Controllers
{
    public class RezervacijaController : Controller
    {
        private readonly IRezervacijaService _rezervacijaService;
        private readonly ILezajService _lezajService;
        private readonly IKorisnikService _korisnikService;
        public RezervacijaController(IRezervacijaService r, ILezajService l, IKorisnikService k) { _rezervacijaService=r; _lezajService=l; _korisnikService=k; }
        public async Task<IActionResult> Index() { ViewBag.Naslov="Sve rezervacije"; return View(await _rezervacijaService.GetAllAsync()); }
        public async Task<IActionResult> Details(int id) { var r = await _rezervacijaService.GetByIdAsync(id); if(r==null)return NotFound(); return View(r); }
        [Authorize] public async Task<IActionResult> Create(int? lezajId) { await UcitajLezajeAsync(); var r = new Rezervacija{DatumPrijave=DateTime.Today,DatumOdjave=DateTime.Today.AddDays(1)}; if(lezajId.HasValue)r.LezajId=lezajId.Value; return View(r); }
        [HttpPost][ValidateAntiForgeryToken][Authorize]
        public async Task<IActionResult> Create(Rezervacija r) { if(!ModelState.IsValid){await UcitajLezajeAsync();return View(r);} r.UserId=_korisnikService.GetTrenutniKorisnikId(); await _rezervacijaService.CreateAsync(r); TempData["Poruka"]=$"Rezervacija za '{r.ImeGosta}' dodana!"; return RedirectToAction(nameof(Index)); }
        [Authorize] public async Task<IActionResult> Edit(int id) { var r = await _rezervacijaService.GetByIdAsync(id); if(r==null)return NotFound(); await UcitajLezajeAsync(); return View(r); }
        [HttpPost][ValidateAntiForgeryToken][Authorize]
        public async Task<IActionResult> Edit(int id, Rezervacija r) { if(id!=r.Id)return BadRequest(); if(!ModelState.IsValid){await UcitajLezajeAsync();return View(r);} await _rezervacijaService.UpdateAsync(r); TempData["Poruka"]="Rezervacija ažurirana!"; return RedirectToAction(nameof(Index)); }
        [Authorize] public async Task<IActionResult> Delete(int id) { var r = await _rezervacijaService.GetByIdAsync(id); if(r==null)return NotFound(); return View(r); }
        [HttpPost,ActionName("Delete")][ValidateAntiForgeryToken][Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id) { await _rezervacijaService.DeleteAsync(id); TempData["Poruka"]="Rezervacija obrisana."; return RedirectToAction(nameof(Index)); }
        public async Task<IActionResult> Aktivne() { ViewBag.Naslov="Aktivne rezervacije"; return View("Index", await _rezervacijaService.GetAktivneAsync()); }
        public async Task<IActionResult> Nadolazece(int dana=7) { ViewBag.Naslov=$"Nadolazeće prijave (sljedećih {dana} dana)"; return View("Index", await _rezervacijaService.GetNadolazecePrijaveAsync(dana)); }
        [Authorize] public async Task<IActionResult> Moje() { var uid=_korisnikService.GetTrenutniKorisnikId(); if(uid==null)return Unauthorized(); ViewBag.Naslov="Moje rezervacije"; return View("Index", await _rezervacijaService.GetByKorisnikiuAsync(uid)); }
        private async Task UcitajLezajeAsync() { var l = await _lezajService.GetDostupniAsync(); ViewBag.LezajId = new SelectList(l,"Id","Naziv"); }
    }
}
