using Microsoft.EntityFrameworkCore;
using Hostel.MVC.Data;
using Hostel.MVC.Models;
namespace Hostel.MVC.Services
{
    public class RezervacijaService : IRezervacijaService
    {
        private readonly ApplicationDbContext _context;
        public RezervacijaService(ApplicationDbContext context) => _context = context;
        public async Task<IEnumerable<Rezervacija>> GetAllAsync() => await _context.Rezervacije.Include(r=>r.Lezaj).OrderByDescending(r=>r.DatumPrijave).ToListAsync();
        public async Task<Rezervacija?> GetByIdAsync(int id) => await _context.Rezervacije.Include(r=>r.Lezaj).FirstOrDefaultAsync(r=>r.Id==id);
        public async Task CreateAsync(Rezervacija r) { _context.Rezervacije.Add(r); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Rezervacija r) { _context.Rezervacije.Update(r); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var r = await _context.Rezervacije.FindAsync(id); if(r!=null){_context.Rezervacije.Remove(r);await _context.SaveChangesAsync();} }
        public async Task<bool> ExistsAsync(int id) => await _context.Rezervacije.AnyAsync(r=>r.Id==id);
        public async Task<IEnumerable<Rezervacija>> GetAktivneAsync() => await _context.Rezervacije.Include(r=>r.Lezaj).Where(r=>r.Status==StatusRezervacije.Aktivna).OrderBy(r=>r.DatumPrijave).ToListAsync();
        public async Task<IEnumerable<Rezervacija>> GetByLezajuAsync(int lezajId) => await _context.Rezervacije.Include(r=>r.Lezaj).Where(r=>r.LezajId==lezajId).ToListAsync();
        public async Task<IEnumerable<Rezervacija>> GetByKorisnikiuAsync(string userId) => await _context.Rezervacije.Include(r=>r.Lezaj).Where(r=>r.UserId==userId).OrderByDescending(r=>r.DatumPrijave).ToListAsync();
        public async Task<IEnumerable<Rezervacija>> GetNadolazecePrijaveAsync(int danaDo=7) => await _context.Rezervacije.Include(r=>r.Lezaj).Where(r=>r.Status==StatusRezervacije.Aktivna&&r.DatumPrijave>=DateTime.Today&&r.DatumPrijave<=DateTime.Today.AddDays(danaDo)).OrderBy(r=>r.DatumPrijave).ToListAsync();
    }
}
