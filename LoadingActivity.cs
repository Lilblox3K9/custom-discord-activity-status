using DiscordRPC;

namespace DiscordStatus;

class LoadingActivity{

    public static void LoadActivity(ConfigAccess configuration){
        if (configuration.Activities.Count == 0){
            Console.WriteLine($"\nNo activities to load");
            return;
        }

        string appId = GetActivityToLoad(configuration);
        string description = GetactivityDescription();
        InstantiatePresence(appId, description);
        return;
    }

    private static string GetActivityToLoad(ConfigAccess configuration){
        Console.WriteLine($"\nSelect an activity:");
        for (int i = 0; i < configuration.Activities.Count; i++)
            Console.WriteLine($"[{i+1}] {configuration.Activities[i].Name}");

        int choice = Program.ReadChoice(1, configuration.Activities.Count);
        return configuration.Activities[choice - 1].ApplicationId;
    }

    private static string GetactivityDescription(){
        Console.WriteLine($"\nDescribe your current activity.");
        
        string description = Console.ReadLine() ?? "";

        while (description.Trim().Length < 2){
            Console.WriteLine("Please enter at least 2 characters");
            description = Console.ReadLine() ?? "";
        }

        return description;
    }

    private static void InstantiatePresence(string appId, string description){
        var client = new DiscordRpcClient(appId);
        client.Initialize();
        
        client.SetPresence(new RichPresence(){
            Details = description,
            Assets = new Assets{
                LargeImageKey = "myimage"
            }
        });

        Console.WriteLine($"\nProjecting status. Press enter to end.");
        Console.ReadLine();
        client.Dispose();
    }
}