using Hostel.MVC.Models;
namespace Hostel.MVC.Services
{
    public interface IRezervacijaService
    {
        Task<IEnumerable<Rezervacija>> GetAllAsync();
        Task<Rezervacija?> GetByIdAsync(int id);
        Task CreateAsync(Rezervacija rezervacija);
        Task UpdateAsync(Rezervacija rezervacija);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Rezervacija>> GetAktivneAsync();
        Task<IEnumerable<Rezervacija>> GetByLezajuAsync(int lezajId);
        Task<IEnumerable<Rezervacija>> GetByKorisnikiuAsync(string userId);
        Task<IEnumerable<Rezervacija>> GetNadolazecePrijaveAsync(int danaDo = 7);
    }
}
