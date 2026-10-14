using Microsoft.EntityFrameworkCore;
using Hostel.RazorPages.Models;
namespace Hostel.RazorPages.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Lezaj> Lezaji { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Lezaj>().HasData(
                new Lezaj { Id = 1, Naziv = "Ležaj A1", BrojSobe = 101, CijenaPoNoci = 15.00m, Dostupan = true, TipSobe = TipSobe.Zajednička, Opis = "Udoban ležaj u zajedničkoj sobi s 6 kreveta.", DatumDodavanja = new DateTime(2024, 1, 1) },
                new Lezaj { Id = 2, Naziv = "Ležaj B3", BrojSobe = 203, CijenaPoNoci = 25.00m, Dostupan = true, TipSobe = TipSobe.Privatna, Opis = "Privatna soba za jednu osobu.", DatumDodavanja = new DateTime(2024, 1, 15) },
                new Lezaj { Id = 3, Naziv = "Ležaj C2", BrojSobe = 305, CijenaPoNoci = 18.00m, Dostupan = false, TipSobe = TipSobe.Mješovita, Opis = "Ležaj u mješovitoj sobi s 4 kreveta.", DatumDodavanja = new DateTime(2024, 2, 1) }
            );
        }
    }
}
