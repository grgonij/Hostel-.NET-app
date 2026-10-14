using Microsoft.AspNetCore.Identity;
namespace Hostel.MVC.Services
{
    public interface IKorisnikService
    {
        Task<IdentityUser?> GetTrenutniKorisnikAsync();
        string? GetTrenutniKorisnikId();
        bool JePrijavljen();
    }
}
