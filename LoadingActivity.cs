using System.Text.Json;
using DiscordRPC;

namespace DiscordStatus;

class LoadingActivity{

    public static void LoadActivity(){
        if (Program.appIds.Count == 0){
            Console.WriteLine("No activities to load");
            return;
        }

        var appId = GetActivityToLoad();
        var description = GetactivityDescription();
        InstantiatePresence(appId, description);
        return;
    }

    private static string GetActivityToLoad(){
        Console.WriteLine("Select an activity:");
        //write out activities in the form
        //[x] activity_name

        int appIndex =  Program.ReadChoice(1, Program.appIds.Count());
        return (Program.appIds[appIndex - 1]);
    }

    private static string GetactivityDescription(){
        Console.WriteLine("Describe your current activity.");
        
        string description = Console.ReadLine() ?? "";

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

        Console.WriteLine("Projecting status. Press enter to end.");
        Console.ReadLine();
        client.Dispose();
    }
}