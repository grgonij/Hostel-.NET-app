using System.ComponentModel.DataAnnotations;
namespace Hostel.WebAPI.Models
{
    public class Lezaj {
        public int Id { get; set; }
        [Required][StringLength(100)] public string Naziv { get; set; } = string.Empty;
        [Required] public int BrojSobe { get; set; }
        [Required][Range(0.01,10000)] public decimal CijenaPoNoci { get; set; }
        public bool Dostupan { get; set; } = true;
        public TipSobe TipSobe { get; set; } = TipSobe.Zajednička;
        [StringLength(500)] public string? Opis { get; set; }
        public DateTime DatumDodavanja { get; set; } = DateTime.Now;
        public ICollection<Rezervacija> Rezervacije { get; set; } = new List<Rezervacija>();
    }
    public enum TipSobe { Zajednička, Privatna, Mješovita }
    public class Rezervacija {
        public int Id { get; set; }
        [Required][StringLength(100)] public string ImeGosta { get; set; } = string.Empty;
        [Required][EmailAddress] public string Email { get; set; } = string.Empty;
        [Required] public DateTime DatumPrijave { get; set; }
        [Required] public DateTime DatumOdjave { get; set; }
        public StatusRezervacije Status { get; set; } = StatusRezervacije.Aktivna;
        [StringLength(300)] public string? Napomena { get; set; }
        [Required] public int LezajId { get; set; }
        public Lezaj? Lezaj { get; set; }
    }
    public enum StatusRezervacije { Aktivna, Otkazana, Završena }
}
