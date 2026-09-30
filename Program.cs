using System.Text.Json;
using DiscordRPC;

string json = File.ReadAllText("config.json");
using JsonDocument doc = JsonDocument.Parse(json);
string appId = doc.RootElement.GetProperty("ApplicationId").GetString() ?? "";

var client = new DiscordRpcClient(appId);
client.Initialize();

Console.WriteLine("Enter your task."); //"Enter your description"
string description = Console.ReadLine() ?? "";

client.SetPresence(new RichPresence(){
    Details = description,
    Assets = new Assets()
    {
        LargeImageKey = "myimage"
    }
});

Console.WriteLine("Projecting status. Press enter to end.");
Console.ReadLine();
client.Dispose();