namespace DiscordStatus;

class RemovingActivity{
    public static void RemoveActivity(ConfigAccess configuration){
        if (configuration.Activities.Count == 0){
            Console.WriteLine($"\nNo activities to remove");
            return;
        }
        int index = GetActivityToRemove(configuration);
        configuration.Remove(index);
    }

    private static int GetActivityToRemove(ConfigAccess configuration){
        Console.WriteLine($"\nSelect an activity:");
        for (int i = 0; i < configuration.Activities.Count; i++)
            Console.WriteLine($"[{i+1}] {configuration.Activities[i].Name}");

        int choice = Program.ReadChoice(1, configuration.Activities.Count);
        return choice - 1;
    }

}