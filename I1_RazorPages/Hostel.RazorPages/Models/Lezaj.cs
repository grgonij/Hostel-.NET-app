using System.ComponentModel.DataAnnotations;
namespace Hostel.RazorPages.Models
{
    public class Lezaj
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Naziv je obavezan.")][StringLength(100)][Display(Name = "Naziv ležaja")]
        public string Naziv { get; set; } = string.Empty;
        [Required][Display(Name = "Broj sobe")]
        public int BrojSobe { get; set; }
        [Required][Range(0.01, 10000)][Display(Name = "Cijena po noći (€)")]
        public decimal CijenaPoNoci { get; set; }
        [Display(Name = "Dostupan")]
        public bool Dostupan { get; set; } = true;
        [Display(Name = "Tip sobe")]
        public TipSobe TipSobe { get; set; } = TipSobe.Zajednička;
        [StringLength(500)][Display(Name = "Opis")]
        public string? Opis { get; set; }
        [Display(Name = "Datum dodavanja")]
        public DateTime DatumDodavanja { get; set; } = DateTime.Now;
        public ICollection<Rezervacija> Rezervacije { get; set; } = new List<Rezervacija>();
    }
    public enum TipSobe { Zajednička, Privatna, Mješovita }
}
