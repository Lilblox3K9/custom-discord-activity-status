using DiscordRPC;

namespace DiscordStatus;

class LoadingActivity{
    public static void LoadActivity(ConfigAccess configuration){
        if (configuration.Activities.Count == 0){
            Console.WriteLine($"\nNo activities to load");
            return;
        }

        ActivityEntry activityEntry = GetActivityToLoad(configuration);
        string description = GetactivityDescription(activityEntry);

        configuration.SetDescription(activityEntry, description);
        InstantiatePresence(activityEntry.ApplicationId, description);
        return;
    }

    private static ActivityEntry GetActivityToLoad(ConfigAccess configuration){
        Console.WriteLine($"\nSelect an activity:");
        for (int i = 0; i < configuration.Activities.Count; i++)
            Console.WriteLine($"[{i+1}] {configuration.Activities[i].Name}");

        int choice = Program.ReadChoice(1, configuration.Activities.Count);
        return configuration.Activities[choice - 1];
    }

    private static string GetactivityDescription(ActivityEntry activityEntry){
        if (activityEntry.Description != ""){
            Console.WriteLine($"\nWhich description would you like to use:");
            Console.WriteLine($"[1] {activityEntry.Description}");
            Console.WriteLine("[2] Write a new one");

            int choice = Program.ReadChoice(1, 2);
            if (choice == 1){
                return activityEntry.Description;
            }
        }

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