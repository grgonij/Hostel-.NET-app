using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Hostel.MVC.Models
{
    public class Rezervacija
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Ime gosta je obavezno.")][StringLength(100)][Display(Name="Ime i prezime gosta")]
        public string ImeGosta { get; set; } = string.Empty;
        [Required][EmailAddress(ErrorMessage="Unesite ispravnu e-mail adresu.")][Display(Name="E-mail")]
        public string Email { get; set; } = string.Empty;
        [Required][Display(Name="Datum prijave")][DataType(DataType.Date)] public DateTime DatumPrijave { get; set; }
        [Required][Display(Name="Datum odjave")][DataType(DataType.Date)] public DateTime DatumOdjave { get; set; }
        [Display(Name="Status rezervacije")] public StatusRezervacije Status { get; set; } = StatusRezervacije.Aktivna;
        [StringLength(300)][Display(Name="Napomena")] public string? Napomena { get; set; }
        [Required][Display(Name="Ležaj")] public int LezajId { get; set; }
        [ForeignKey("LezajId")] public Lezaj? Lezaj { get; set; }
        public string? UserId { get; set; }
        [NotMapped] public int BrojNoci => (DatumOdjave - DatumPrijave).Days;
        [NotMapped] public decimal UkupnaCijena => BrojNoci * (Lezaj?.CijenaPoNoci ?? 0);
    }
    public enum StatusRezervacije { Aktivna, Otkazana, Završena }
}
