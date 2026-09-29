# ITRental API - System Wypożyczalni Sprzętu IT

Aplikacja zaliczeniowa Web API zrealizowana w ASP.NET Core (.NET 8.0), oparta na architekturze trójwarstwowej.

## Funkcjonalności:
- **Baza danych SQLite z EF Core** - trwała persystencja danych.
- **Soft Delete** - sprzęt nie jest usuwany fizycznie, lecz ukrywany za pomocą flagi `IsDeleted` i odfiltrowywany.
- **Paginacja wyników** - stronicowanie listy sprzętu (parametry `pageNumber` i `pageSize`).
- **Global Error Handling (Middleware)** - wyłapywanie wyjątków domenowych (np. próba dodania duplikatu) i zamiana ich na kody HTTP 400/404.
- **Swagger UI** - dokumentacja interfejsu z opisami XML.

## Instrukcja Uruchomienia:
1. Upewnij się, że masz zainstalowane środowisko .NET SDK.
2. Otwórz folder główny w terminalu.
3. Wpisz komendę:
   `dotnet run`
4. Otwórz przeglądarkę i przejdź pod adres podany w konsoli, dopisując końcówkę `/swagger` (np. http://localhost:5147/swagger).


