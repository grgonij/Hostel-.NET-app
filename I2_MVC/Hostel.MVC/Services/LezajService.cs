using Microsoft.EntityFrameworkCore;
using Hostel.MVC.Data;
using Hostel.MVC.Models;
namespace Hostel.MVC.Services
{
    public class LezajService : ILezajService
    {
        private readonly ApplicationDbContext _context;
        public LezajService(ApplicationDbContext context) => _context = context;
        public async Task<IEnumerable<Lezaj>> GetAllAsync() => await _context.Lezaji.Include(l => l.Rezervacije).ToListAsync();
        public async Task<Lezaj?> GetByIdAsync(int id) => await _context.Lezaji.Include(l => l.Rezervacije).FirstOrDefaultAsync(l => l.Id == id);
        public async Task CreateAsync(Lezaj lezaj) { lezaj.DatumDodavanja = DateTime.Now; _context.Lezaji.Add(lezaj); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Lezaj lezaj) { _context.Lezaji.Update(lezaj); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var l = await _context.Lezaji.FindAsync(id); if (l != null) { _context.Lezaji.Remove(l); await _context.SaveChangesAsync(); } }
        public async Task<bool> ExistsAsync(int id) => await _context.Lezaji.AnyAsync(l => l.Id == id);
        public async Task<IEnumerable<Lezaj>> GetDostupniAsync()
{
    var lezaji = await _context.Lezaji.Include(l=>l.Rezervacije)
        .Where(l => l.Dostupan)
        .ToListAsync();
    return lezaji.OrderBy(l => l.CijenaPoNoci);
}
        public async Task<IEnumerable<Lezaj>> GetByTipuSobeAsync(TipSobe tip) => await _context.Lezaji.Include(l=>l.Rezervacije).Where(l => l.TipSobe == tip).ToListAsync();
        public async Task<IEnumerable<Lezaj>> GetSortiraniPoCijeniAsync()
{
    var lezaji = await _context.Lezaji.Include(l=>l.Rezervacije)
        .ToListAsync();
    return lezaji.OrderBy(l => l.CijenaPoNoci);
}
    }
}
