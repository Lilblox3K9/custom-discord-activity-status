using System.Text.Json;
using DiscordRPC;

namespace DiscordStatus;

class AddingActivity
{

    public static void AddActivity(){
        Console.WriteLine("Enter your activity ID.");
        string activityToAdd = Console.ReadLine() ?? "";
        while (activityToAdd.Length == 0 || !activityToAdd.All(char.IsDigit)){
            Console.WriteLine("Please input a valid number.");
            activityToAdd = Console.ReadLine() ?? "";
        }
        
        Program.AddId(activityToAdd);
        return;
    }
}