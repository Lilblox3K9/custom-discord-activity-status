using DiscordRPC;

var client = new DiscordRpcClient("1554800658194894969");
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