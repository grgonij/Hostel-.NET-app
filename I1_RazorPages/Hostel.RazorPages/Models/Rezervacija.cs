using System.ComponentModel.DataAnnotations;
namespace Hostel.RazorPages.Models
{
    public class Rezervacija
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ime gosta je obavezno.")][StringLength(100)][Display(Name = "Ime i prezime gosta")]
        public string ImeGosta { get; set; } = string.Empty;
        [Required][EmailAddress(ErrorMessage = "Unesite ispravnu e-mail adresu.")][Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;
        [Required][Display(Name = "Datum prijave")]
        public DateTime DatumPrijave { get; set; }
        [Required][Display(Name = "Datum odjave")]
        public DateTime DatumOdjave { get; set; }
        [Required][Display(Name = "Ležaj")]
        public int LezajId { get; set; }
        public Lezaj? Lezaj { get; set; }
        [Display(Name = "Status rezervacije")]
        public StatusRezervacije Status { get; set; } = StatusRezervacije.Aktivna;
    }
    public enum StatusRezervacije { Aktivna, Otkazana, Završena }
}
