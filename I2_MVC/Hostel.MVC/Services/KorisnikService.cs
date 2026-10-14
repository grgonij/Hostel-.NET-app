using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace Hostel.MVC.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public KorisnikService(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        { _userManager = userManager; _httpContextAccessor = httpContextAccessor; }
        public async Task<IdentityUser?> GetTrenutniKorisnikAsync()
        { var id = GetTrenutniKorisnikId(); return id == null ? null : await _userManager.FindByIdAsync(id); }
        public string? GetTrenutniKorisnikId() => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public bool JePrijavljen() => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
