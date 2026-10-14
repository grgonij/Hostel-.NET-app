using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hostel.MVC.Data;
using Hostel.MVC.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=hostel_mvc.db"));
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<ILezajService, LezajService>();
builder.Services.AddScoped<IRezervacijaService, RezervacijaService>();
builder.Services.AddScoped<IKorisnikService, KorisnikService>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }
app.UseHttpsRedirection(); app.UseStaticFiles(); app.UseRouting();
app.UseAuthentication(); app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
using (var scope = app.Services.CreateScope()) { scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.EnsureCreated(); }
app.Run();
