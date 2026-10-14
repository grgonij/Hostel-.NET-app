using Hostel.MVC.Models;
namespace Hostel.MVC.Services
{
    public interface ILezajService
    {
        Task<IEnumerable<Lezaj>> GetAllAsync();
        Task<Lezaj?> GetByIdAsync(int id);
        Task CreateAsync(Lezaj lezaj);
        Task UpdateAsync(Lezaj lezaj);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Lezaj>> GetDostupniAsync();
        Task<IEnumerable<Lezaj>> GetByTipuSobeAsync(TipSobe tip);
        Task<IEnumerable<Lezaj>> GetSortiraniPoCijeniAsync();
    }
}
