using System.Text.Json;

namespace DiscordStatus;

class AddingActivity{
    static readonly HttpClient http = new();
    public static void AddActivity(ConfigAccess configuration){
        Console.WriteLine($"\nEnter your activity ID.");
        string activityToAdd = Console.ReadLine() ?? "";
        while (activityToAdd.Length == 0 || !activityToAdd.All(char.IsDigit)){
            Console.WriteLine("Please input a valid number.");
            activityToAdd = Console.ReadLine() ?? "";
        }
        
        string name = GetAppName(activityToAdd) ?? "";
        if (name.Trim() == ""){
            Console.WriteLine("Unable to find app name. Enter one now.");
            name = Console.ReadLine() ?? "Unnamed";
        }
        
        var entry = new ActivityEntry { Name = name, ApplicationId = activityToAdd, Description = "" };
        
        if (configuration.Add(entry))
            Console.WriteLine("Successfully added application");
        else
            Console.WriteLine("Application is already added");
    }

    static string? GetAppName(string appId){
        try{
           string json = http.GetStringAsync(
                $"https://discord.com/api/v10/applications/{appId}/rpc")
                .GetAwaiter().GetResult();

            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("name").GetString(); 
        }
        catch{
            return null;
        }
    }
}