using System.Text.Json;
using DiscordRPC;

namespace DiscordStatus;

class Program
{
    static AppConfig config = new();
    public static List<string> appIds = new();

    static void Main()
    {
        string json = File.ReadAllText("config.json");
        config = JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
        appIds = config.ApplicationIds;

        bool running = true;
        while (running){
            Menu();
            
            Console.WriteLine("Would you like to:");
            Console.WriteLine("[1] Return to main menu");
            Console.WriteLine("[2] Close the application");

            if (ReadChoice(1, 2) == 2)
                running = false;
        }
    }

    public static int ReadChoice(int min, int max)
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int choice ) && choice >= min && choice <= max)
                return choice;
                
            Console.WriteLine($"Enter a number from {min} to {max}");
        }
    }
    public static void AddId(string appId){
        if (config.ApplicationIds.Contains(appId)){
            Console.WriteLine("Application is already added.");
            return;
        }
        else{
            config.ApplicationIds.Add(appId);
            File.WriteAllText("config.json", JsonSerializer.Serialize(config));
            Console.WriteLine("Application has been added successfully.");
            return;
        }
    }
    static void Menu()
    {
        Console.WriteLine("Are you:");
        Console.WriteLine("[1] Loading an acitivty");
        Console.WriteLine("[2] Adding an activity");
        Console.WriteLine("[3] Removing an activity");

        switch (ReadChoice(1, 2))
        {
            case 1:
                LoadingActivity.LoadActivity();
                break;
            case 2:
                AddingActivity.AddActivity();
                break;
        }
    }
}

public class AppConfig
{
    public List<string> ApplicationIds { get; set; } = new();
}