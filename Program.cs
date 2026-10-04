namespace DiscordStatus;

class Program{
    static void Main()
    {
        var configuration = new ConfigAccess();
        configuration.Load();

        bool running = true;
        while (running){
            Menu(configuration);
            
            Console.WriteLine($"\nWould you like to:");
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
    static void Menu(ConfigAccess configuration)
    {
        Console.WriteLine($"\nAre you:");
        Console.WriteLine("[1] Loading an activty");
        Console.WriteLine("[2] Adding an activity");
        Console.WriteLine("[3] Removing an activity");

        switch (ReadChoice(1, 2))
        {
            case 1:
                LoadingActivity.LoadActivity(configuration);
                break;
            case 2:
                AddingActivity.AddActivity(configuration);
                break;
        }
    }
}