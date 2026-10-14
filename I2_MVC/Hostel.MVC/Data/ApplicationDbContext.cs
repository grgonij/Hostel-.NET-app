using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hostel.MVC.Models;
namespace Hostel.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Lezaj> Lezaji { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Lezaj>().HasData(
                new Lezaj { Id=1, Naziv="Ležaj A1", BrojSobe=101, CijenaPoNoci=15m, Dostupan=true, TipSobe=TipSobe.Zajednička, Opis="Zajednička soba.", DatumDodavanja=new DateTime(2024,1,1) },
                new Lezaj { Id=2, Naziv="Ležaj B3", BrojSobe=203, CijenaPoNoci=25m, Dostupan=true, TipSobe=TipSobe.Privatna, Opis="Privatna soba.", DatumDodavanja=new DateTime(2024,1,15) },
                new Lezaj { Id=3, Naziv="Ležaj C2", BrojSobe=305, CijenaPoNoci=18m, Dostupan=false, TipSobe=TipSobe.Mješovita, Opis="Mješovita soba.", DatumDodavanja=new DateTime(2024,2,1) }
            );
        }
    }
}
