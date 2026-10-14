using Microsoft.EntityFrameworkCore;
using Hostel.WebAPI.Data;
using Hostel.WebAPI.DTOs;
using Hostel.WebAPI.Models;
namespace Hostel.WebAPI.Services
{
    public interface ILezajService
    {
        Task<IEnumerable<LezajDto>> GetAllAsync();
        Task<LezajDto?> GetByIdAsync(int id);
        Task<LezajDto> CreateAsync(CreateLezajDto dto);
        Task<LezajDto?> UpdateAsync(int id, UpdateLezajDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<LezajDto>> GetDostupniAsync();
        Task<IEnumerable<LezajDto>> FilterAsync(TipSobe? tip, decimal? cijenaOd, decimal? cijenaDo, bool? dostupan);
    }
    public class LezajService : ILezajService
    {
        private readonly ApplicationDbContext _ctx;
        public LezajService(ApplicationDbContext ctx) => _ctx = ctx;
        private static LezajDto ToDto(Lezaj l) => new LezajDto{Id=l.Id,Naziv=l.Naziv,BrojSobe=l.BrojSobe,CijenaPoNoci=l.CijenaPoNoci,Dostupan=l.Dostupan,TipSobe=l.TipSobe.ToString(),Opis=l.Opis,DatumDodavanja=l.DatumDodavanja,BrojRezervacija=l.Rezervacije.Count};
        public async Task<IEnumerable<LezajDto>> GetAllAsync() => (await _ctx.Lezaji.Include(l=>l.Rezervacije).ToListAsync()).Select(ToDto);
        public async Task<LezajDto?> GetByIdAsync(int id) { var l=await _ctx.Lezaji.Include(l=>l.Rezervacije).FirstOrDefaultAsync(l=>l.Id==id); return l==null?null:ToDto(l); }
        public async Task<LezajDto> CreateAsync(CreateLezajDto dto)
        {
            var l=new Lezaj{Naziv=dto.Naziv,BrojSobe=dto.BrojSobe,CijenaPoNoci=dto.CijenaPoNoci,Dostupan=dto.Dostupan,TipSobe=dto.TipSobe,Opis=dto.Opis,DatumDodavanja=DateTime.Now};
            _ctx.Lezaji.Add(l); await _ctx.SaveChangesAsync(); return ToDto(l);
        }
        public async Task<LezajDto?> UpdateAsync(int id, UpdateLezajDto dto)
        {
            var l=await _ctx.Lezaji.FindAsync(id); if(l==null)return null;
            l.Naziv=dto.Naziv;l.BrojSobe=dto.BrojSobe;l.CijenaPoNoci=dto.CijenaPoNoci;l.Dostupan=dto.Dostupan;l.TipSobe=dto.TipSobe;l.Opis=dto.Opis;
            await _ctx.SaveChangesAsync(); return ToDto(l);
        }
        public async Task<bool> DeleteAsync(int id) { var l=await _ctx.Lezaji.FindAsync(id); if(l==null)return false; _ctx.Lezaji.Remove(l); await _ctx.SaveChangesAsync(); return true; }
        public async Task<IEnumerable<LezajDto>> GetDostupniAsync() => (await _ctx.Lezaji.Include(l=>l.Rezervacije).Where(l=>l.Dostupan).OrderBy(l=>l.CijenaPoNoci).ToListAsync()).Select(ToDto);
        public async Task<IEnumerable<LezajDto>> FilterAsync(TipSobe? tip, decimal? cijenaOd, decimal? cijenaDo, bool? dostupan)
        {
            var q=_ctx.Lezaji.Include(l=>l.Rezervacije).AsQueryable();
            if(tip.HasValue)q=q.Where(l=>l.TipSobe==tip.Value);
            if(cijenaOd.HasValue)q=q.Where(l=>l.CijenaPoNoci>=cijenaOd.Value);
            if(cijenaDo.HasValue)q=q.Where(l=>l.CijenaPoNoci<=cijenaDo.Value);
            if(dostupan.HasValue)q=q.Where(l=>l.Dostupan==dostupan.Value);
            return (await q.ToListAsync()).Select(ToDto);
        }
    }
}
