using Hostel.WebAPI.Models;
namespace Hostel.WebAPI.DTOs
{
    public class LezajDto { public int Id{get;set;} public string Naziv{get;set;}=string.Empty; public int BrojSobe{get;set;} public decimal CijenaPoNoci{get;set;} public bool Dostupan{get;set;} public string TipSobe{get;set;}=string.Empty; public string? Opis{get;set;} public DateTime DatumDodavanja{get;set;} public int BrojRezervacija{get;set;} }
    public class CreateLezajDto { public string Naziv{get;set;}=string.Empty; public int BrojSobe{get;set;} public decimal CijenaPoNoci{get;set;} public bool Dostupan{get;set;}=true; public TipSobe TipSobe{get;set;}=TipSobe.Zajednička; public string? Opis{get;set;} }
    public class UpdateLezajDto : CreateLezajDto {}
    public class RezervacijaDto { public int Id{get;set;} public string ImeGosta{get;set;}=string.Empty; public string Email{get;set;}=string.Empty; public DateTime DatumPrijave{get;set;} public DateTime DatumOdjave{get;set;} public string Status{get;set;}=string.Empty; public string? Napomena{get;set;} public int LezajId{get;set;} public string? NazivLezaja{get;set;} public int BrojNoci{get;set;} public decimal UkupnaCijena{get;set;} }
    public class CreateRezervacijaDto { public string ImeGosta{get;set;}=string.Empty; public string Email{get;set;}=string.Empty; public DateTime DatumPrijave{get;set;} public DateTime DatumOdjave{get;set;} public StatusRezervacije Status{get;set;}=StatusRezervacije.Aktivna; public string? Napomena{get;set;} public int LezajId{get;set;} }
    public class UpdateRezervacijaDto : CreateRezervacijaDto {}
}
