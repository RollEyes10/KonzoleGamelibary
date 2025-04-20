# 🎮 Game Library Console App (C# + SQLite)

Jednoduchá konzolová aplikace pro správu herní knihovny, napsaná v jazyce **C#** a využívající databázi **SQLite**.  
Umožňuje přidávat, upravovat, mazat a vyhledávat hry v databázi `games.db`, která se vytváří automaticky v adresáři aplikace.

---

## 🛠 Požadavky

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download) nebo vyšší (doporučeno .NET 8)
- Kompatibilní vývojové prostředí (např. Visual Studio, VS Code)

---

## 🚀 Spuštění aplikace

1. Naklonuj repozitář:
   ```bash
   git clone https://github.com/tvoje-jmeno/game-library-console.git
   cd game-library-console
   ```

2. Sestav a spusť:
   ```bash
   dotnet run
   ```

---

## 🧩 Použité technologie

- **C#**
- **SQLite** (`Microsoft.Data.Sqlite` NuGet balíček)
- **ADO.NET** pro práci s databází

---

## 🖥️ Funkce aplikace

- ✅ Přidání nové hry
- 📋 Výpis všech uložených her
- 🔍 Vyhledávání her podle názvu
- ✏️ Úprava existující hry
- 🗑️ Mazání hry podle ID

---

## 📄 Ukázka použití

```
== HERNÍ KNIHOVNA ==
1. Přidat novou hru
2. Vypsat všechny hry
3. Vyhledat hru podle názvu
4. Smazat hru
5. Upravit hru
6. Konec
Vyber akci:
```

---

## 💾 Databáze

Aplikace vytvoří databázový soubor `games.db` v aktuální složce při prvním spuštění.

Tabulka `Games` obsahuje:
- `Id` – primární klíč
- `Name` – název hry
- `Genre` – žánr
- `ReleaseYear` – rok vydání

---

## 📦 Instalace balíčků

Pokud spouštíš projekt v novém prostředí, nainstaluj NuGet balíček:

```bash
dotnet add package Microsoft.Data.Sqlite
```

---

## 📝 Licence

Tento projekt je open-source a je dostupný pod licencí **MIT**.
