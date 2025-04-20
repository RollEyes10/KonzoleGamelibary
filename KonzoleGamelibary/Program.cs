using System;
using Microsoft.Data.Sqlite;

class Program
{
    static void Main()
    {
        string connectionString = "Data Source=games.db";

        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        // Vytvoření tabulky, pokud neexistuje
        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Games (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Genre TEXT,
                ReleaseYear INTEGER
            );";
        tableCmd.ExecuteNonQuery();

        while (true)
        {
            Console.WriteLine("\n== HERNÍ KNIHOVNA ==");
            Console.WriteLine("1. Přidat novou hru");
            Console.WriteLine("2. Vypsat všechny hry");
            Console.WriteLine("3. Vyhledat hru podle názvu");
            Console.WriteLine("4. Smazat hru");
            Console.WriteLine("5. Upravit hru");
            Console.WriteLine("6. Konec");
            Console.Write("Vyber akci: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddGame(connection);
                    break;
                case "2":
                    ListGames(connection);
                    break;
                case "3":
                    SearchGame(connection);
                    break;
                case "4":
                    DeleteGame(connection);
                    break;
                case "5":
                    UpdateGame(connection);
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Neplatná volba.");
                    break;
            }
        }
    }

    static void AddGame(SqliteConnection connection)
    {
        Console.Write("Název hry: ");
        var name = Console.ReadLine();
        Console.Write("Žánr: ");
        var genre = Console.ReadLine();
        Console.Write("Rok vydání: ");
        var year = Console.ReadLine();

        var insertCmd = connection.CreateCommand();
        insertCmd.CommandText = @"INSERT INTO Games (Name, Genre, ReleaseYear) VALUES ($name, $genre, $year);";
        insertCmd.Parameters.AddWithValue("$name", name);
        insertCmd.Parameters.AddWithValue("$genre", genre);
        insertCmd.Parameters.AddWithValue("$year", year);
        insertCmd.ExecuteNonQuery();

        Console.WriteLine("✅ Hra přidána.");
    }

    static void ListGames(SqliteConnection connection)
    {
        var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM Games;";
        using var reader = selectCmd.ExecuteReader();

        Console.WriteLine("\n📚 Seznam her:");
        while (reader.Read())
        {
            Console.WriteLine($"[{reader.GetInt32(0)}] {reader.GetString(1)} | {reader.GetString(2)} | {reader.GetInt32(3)}");
        }
    }

    static void SearchGame(SqliteConnection connection)
    {
        Console.Write("Zadej název hry k vyhledání: ");
        var name = Console.ReadLine();

        var searchCmd = connection.CreateCommand();
        searchCmd.CommandText = "SELECT * FROM Games WHERE Name LIKE $name;";
        searchCmd.Parameters.AddWithValue("$name", "%" + name + "%");

        using var reader = searchCmd.ExecuteReader();

        Console.WriteLine("\n🔍 Výsledky hledání:");
        while (reader.Read())
        {
            Console.WriteLine($"[{reader.GetInt32(0)}] {reader.GetString(1)} | {reader.GetString(2)} | {reader.GetInt32(3)}");
        }
    }

    static void DeleteGame(SqliteConnection connection)
    {
        Console.Write("Zadej ID hry ke smazání: ");
        var id = Console.ReadLine();

        var deleteCmd = connection.CreateCommand();
        deleteCmd.CommandText = "DELETE FROM Games WHERE Id = $id;";
        deleteCmd.Parameters.AddWithValue("$id", id);
        int rowsAffected = deleteCmd.ExecuteNonQuery();

        if (rowsAffected > 0)
            Console.WriteLine("🗑️ Hra smazána.");
        else
            Console.WriteLine("⚠️ Hra nebyla nalezena.");
    }

    static void UpdateGame(SqliteConnection connection)
    {
        Console.Write("Zadej ID hry k úpravě: ");
        var id = Console.ReadLine();

        Console.Write("Nový název hry: ");
        var name = Console.ReadLine();
        Console.Write("Nový žánr: ");
        var genre = Console.ReadLine();
        Console.Write("Nový rok vydání: ");
        var year = Console.ReadLine();

        var updateCmd = connection.CreateCommand();
        updateCmd.CommandText = @"
            UPDATE Games 
            SET Name = $name, Genre = $genre, ReleaseYear = $year 
            WHERE Id = $id;";
        updateCmd.Parameters.AddWithValue("$name", name);
        updateCmd.Parameters.AddWithValue("$genre", genre);
        updateCmd.Parameters.AddWithValue("$year", year);
        updateCmd.Parameters.AddWithValue("$id", id);

        int rowsAffected = updateCmd.ExecuteNonQuery();

        if (rowsAffected > 0)
            Console.WriteLine("✅ Hra upravena.");
        else
            Console.WriteLine("⚠️ Hra nebyla nalezena.");
    }
}
