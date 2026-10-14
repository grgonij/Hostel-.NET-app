using Microsoft.EntityFrameworkCore;
using Hostel.WebAPI.Data;
using Hostel.WebAPI.DTOs;
using Hostel.WebAPI.Models;
namespace Hostel.WebAPI.Services
{
    public interface IRezervacijaService
    {
        Task<IEnumerable<RezervacijaDto>> GetAllAsync();
        Task<RezervacijaDto?> GetByIdAsync(int id);
        Task<RezervacijaDto?> CreateAsync(CreateRezervacijaDto dto);
        Task<RezervacijaDto?> UpdateAsync(int id, UpdateRezervacijaDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<RezervacijaDto>> GetByStatusuAsync(StatusRezervacije status);
        Task<IEnumerable<RezervacijaDto>> GetByLezajuAsync(int lezajId);
    }
    public class RezervacijaService : IRezervacijaService
    {
        private readonly ApplicationDbContext _ctx;
        public RezervacijaService(ApplicationDbContext ctx) => _ctx = ctx;
        private static RezervacijaDto ToDto(Rezervacija r) => new RezervacijaDto{Id=r.Id,ImeGosta=r.ImeGosta,Email=r.Email,DatumPrijave=r.DatumPrijave,DatumOdjave=r.DatumOdjave,Status=r.Status.ToString(),Napomena=r.Napomena,LezajId=r.LezajId,NazivLezaja=r.Lezaj?.Naziv,BrojNoci=(r.DatumOdjave-r.DatumPrijave).Days,UkupnaCijena=(r.DatumOdjave-r.DatumPrijave).Days*(r.Lezaj?.CijenaPoNoci??0)};
        private IQueryable<Rezervacija> WithIncludes() => _ctx.Rezervacije.Include(r=>r.Lezaj);
        public async Task<IEnumerable<RezervacijaDto>> GetAllAsync() => (await WithIncludes().OrderByDescending(r=>r.DatumPrijave).ToListAsync()).Select(ToDto);
        public async Task<RezervacijaDto?> GetByIdAsync(int id) { var r=await WithIncludes().FirstOrDefaultAsync(r=>r.Id==id); return r==null?null:ToDto(r); }
        public async Task<RezervacijaDto?> CreateAsync(CreateRezervacijaDto dto)
        {
            if(!await _ctx.Lezaji.AnyAsync(l=>l.Id==dto.LezajId))return null;
            var r=new Rezervacija{ImeGosta=dto.ImeGosta,Email=dto.Email,DatumPrijave=dto.DatumPrijave,DatumOdjave=dto.DatumOdjave,Status=dto.Status,Napomena=dto.Napomena,LezajId=dto.LezajId};
            _ctx.Rezervacije.Add(r); await _ctx.SaveChangesAsync();
            var saved=await WithIncludes().FirstOrDefaultAsync(x=>x.Id==r.Id); return saved==null?null:ToDto(saved);
        }
        public async Task<RezervacijaDto?> UpdateAsync(int id, UpdateRezervacijaDto dto)
        {
            var r=await _ctx.Rezervacije.FindAsync(id); if(r==null)return null;
            r.ImeGosta=dto.ImeGosta;r.Email=dto.Email;r.DatumPrijave=dto.DatumPrijave;r.DatumOdjave=dto.DatumOdjave;r.Status=dto.Status;r.Napomena=dto.Napomena;r.LezajId=dto.LezajId;
            await _ctx.SaveChangesAsync(); var saved=await WithIncludes().FirstOrDefaultAsync(x=>x.Id==id); return saved==null?null:ToDto(saved);
        }
        public async Task<bool> DeleteAsync(int id) { var r=await _ctx.Rezervacije.FindAsync(id); if(r==null)return false; _ctx.Rezervacije.Remove(r); await _ctx.SaveChangesAsync(); return true; }
        public async Task<IEnumerable<RezervacijaDto>> GetByStatusuAsync(StatusRezervacije status) => (await WithIncludes().Where(r=>r.Status==status).OrderBy(r=>r.DatumPrijave).ToListAsync()).Select(ToDto);
        public async Task<IEnumerable<RezervacijaDto>> GetByLezajuAsync(int lezajId) => (await WithIncludes().Where(r=>r.LezajId==lezajId).OrderByDescending(r=>r.DatumPrijave).ToListAsync()).Select(ToDto);
    }
}
