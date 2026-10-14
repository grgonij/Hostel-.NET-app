# Hostel-.NET-app

## O projektu

HostelHub je web aplikacija za upravljanje ležajevima i rezervacijama u hostelu,
izrađena kao projektni zadatak iz kolegija .NET Programiranje.


## Tehnologije

- **ASP.NET Core 10.0**
- **Entity Framework Core** + SQLite
- **ASP.NET Core Identity** (autentifikacija)
- **Swagger / OpenAPI** (API dokumentacija)
- **Docker** (containerizacija)
- **Bootstrap 5.3** + Bootstrap Icons

---

## Pokretanje

### Preduvjeti
- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### I1 — Razor Pages
```bash
cd I1_RazorPages/Hostel.RazorPages
dotnet run
# http://localhost:5000
```

### I2 — MVC + Identity
```bash
cd I2_MVC/Hostel.MVC
dotnet run
# http://localhost:5000/Home/Index
```

### I3 — Web API
```bash
cd I3_WebAPI/Hostel.WebAPI
dotnet run
# Swagger: http://localhost:5000
```

---

## Funkcionalnosti

### Razor Pages (I1)
- Pregled, dodavanje, uređivanje i brisanje ležajeva
- Filtriranje po tipu sobe, cijeni i dostupnosti
- Sortiranje po nazivu, cijeni i broju sobe
- Upravljanje rezervacijama

### MVC + Identity (I2)
- Sva CRUD funkcionalnost iz I1
- Registracija i prijava korisnika (ASP.NET Core Identity)
- Zaštićene akcije (`[Authorize]`)
- Servisni sloj (`ILezajService`, `IRezervacijaService`, `IKorisnikService`)
- Pregled aktivnih i nadolazećih rezervacija
- "Moje rezervacije" za prijavljenog korisnika

### Web API (I3)
| Metoda | Endpoint | Opis |
|--------|----------|------|
| GET | `/api/lezaji` | Svi ležajevi |
| POST | `/api/lezaji` | Kreiraj ležaj |
| PUT | `/api/lezaji/{id}` | Ažuriraj ležaj |
| DELETE | `/api/lezaji/{id}` | Obriši ležaj |
| GET | `/api/lezaji/dostupni` | Dostupni ležajevi |
| GET | `/api/lezaji/filter` | Filtriraj ležajeve |
| GET | `/api/rezervacije` | Sve rezervacije |
| POST | `/api/rezervacije` | Kreiraj rezervaciju |
| GET | `/api/rezervacije/filter` | Filtriraj po statusu |
