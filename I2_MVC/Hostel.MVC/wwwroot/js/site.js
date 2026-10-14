function autoZatvoriAlerte() {
    setTimeout(function () {
        document.querySelectorAll('.alert.alert-dismissible').forEach(a => new bootstrap.Alert(a).close());
    }, 5000);
}
function potvrdiObrisanje(naziv) {
    return confirm('Jeste li sigurni da želite obrisati "' + naziv + '"? Ova radnja je nepovratna.');
}
function izracunajNoci() {
    var prijava = document.getElementById('DatumPrijave');
    var odjava = document.getElementById('DatumOdjave');
    var info = document.getElementById('infoNoci');
    if (!prijava || !odjava || !info) return;
    if (prijava.value && odjava.value) {
        var razlika = (new Date(odjava.value) - new Date(prijava.value)) / 86400000;
        if (razlika > 0) { info.textContent = 'Broj noći: ' + razlika; info.style.display = 'block'; info.className = 'text-success small mt-1'; }
        else { info.textContent = 'Datum odjave mora biti nakon datuma prijave!'; info.style.display = 'block'; info.className = 'text-danger small mt-1'; }
    }
}
document.addEventListener('DOMContentLoaded', function () {
    autoZatvoriAlerte();
    var p = document.getElementById('DatumPrijave');
    var o = document.getElementById('DatumOdjave');
    if (p) p.addEventListener('change', izracunajNoci);
    if (o) o.addEventListener('change', izracunajNoci);
});
