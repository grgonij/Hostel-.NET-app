using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hostel.MVC.Models;
using Hostel.MVC.Services;
namespace Hostel.MVC.Controllers
{
    public class LezajController : Controller
    {
        private readonly ILezajService _lezajService;
        public LezajController(ILezajService s) => _lezajService = s;
        public async Task<IActionResult> Index() { ViewBag.Naslov = "Svi ležajevi"; return View(await _lezajService.GetAllAsync()); }
        public async Task<IActionResult> Details(int id) { var l = await _lezajService.GetByIdAsync(id); if(l==null)return NotFound(); return View(l); }
        [Authorize] public IActionResult Create() => View();
        [HttpPost][ValidateAntiForgeryToken][Authorize]
        public async Task<IActionResult> Create(Lezaj lezaj) { if(!ModelState.IsValid)return View(lezaj); await _lezajService.CreateAsync(lezaj); TempData["Poruka"]=$"Ležaj '{lezaj.Naziv}' dodan!"; return RedirectToAction(nameof(Index)); }
        [Authorize] public async Task<IActionResult> Edit(int id) { var l = await _lezajService.GetByIdAsync(id); if(l==null)return NotFound(); return View(l); }
        [HttpPost][ValidateAntiForgeryToken][Authorize]
        public async Task<IActionResult> Edit(int id, Lezaj lezaj) { if(id!=lezaj.Id)return BadRequest(); if(!ModelState.IsValid)return View(lezaj); await _lezajService.UpdateAsync(lezaj); TempData["Poruka"]=$"Ležaj '{lezaj.Naziv}' ažuriran!"; return RedirectToAction(nameof(Index)); }
        [Authorize] public async Task<IActionResult> Delete(int id) { var l = await _lezajService.GetByIdAsync(id); if(l==null)return NotFound(); return View(l); }
        [HttpPost,ActionName("Delete")][ValidateAntiForgeryToken][Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id) { await _lezajService.DeleteAsync(id); TempData["Poruka"]="Ležaj obrisan."; return RedirectToAction(nameof(Index)); }
        public async Task<IActionResult> Dostupni() { ViewBag.Naslov="Dostupni ležajevi"; return View("Index", await _lezajService.GetDostupniAsync()); }
        public async Task<IActionResult> PoTipu(TipSobe tip) { ViewBag.Naslov=$"Ležajevi - {tip} soba"; return View("Index", await _lezajService.GetByTipuSobeAsync(tip)); }
    }
}
