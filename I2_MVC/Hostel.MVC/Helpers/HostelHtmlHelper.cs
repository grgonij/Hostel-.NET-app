using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hostel.MVC.Models;
namespace Hostel.MVC.Helpers
{
    public static class HostelHtmlHelper
    {
        public static IHtmlContent StatusBadge(this IHtmlHelper helper, StatusRezervacije status)
        {
            var (css, icon, tekst) = status switch {
                StatusRezervacije.Aktivna => ("bg-success","bi-check-circle","Aktivna"),
                StatusRezervacije.Otkazana => ("bg-danger","bi-x-circle","Otkazana"),
                StatusRezervacije.Završena => ("bg-secondary","bi-archive","Završena"),
                _ => ("bg-light text-dark","bi-question-circle",status.ToString())
            };
            return new HtmlString($"<span class=\"badge {css}\"><i class=\"bi {icon} me-1\"></i>{tekst}</span>");
        }
        public static IHtmlContent TipSobeBadge(this IHtmlHelper helper, TipSobe tip)
        {
            var (css, tekst) = tip switch {
                TipSobe.Privatna => ("bg-primary","Privatna"),
                TipSobe.Zajednička => ("bg-info text-dark","Zajednička"),
                TipSobe.Mješovita => ("bg-warning text-dark","Mješovita"),
                _ => ("bg-secondary",tip.ToString())
            };
            return new HtmlString($"<span class=\"badge {css}\">{tekst}</span>");
        }
        public static IHtmlContent FormatCijena(this IHtmlHelper helper, decimal cijena) =>
            new HtmlString($"<strong class=\"text-primary\">{cijena:F2} €</strong>");
        public static IHtmlContent DostupnostIkona(this IHtmlHelper helper, bool dostupan) =>
            dostupan ? new HtmlString("<span class=\"badge bg-success\"><i class=\"bi bi-check-circle me-1\"></i>Dostupan</span>")
                     : new HtmlString("<span class=\"badge bg-danger\"><i class=\"bi bi-x-circle me-1\"></i>Zauzet</span>");
    }
}
