using System.Text.Json;

namespace DiscordStatus;
class ConfigAccess{
    private const string Path = "config.json";
    private AppConfig config = new();
    
    public IReadOnlyList<ActivityEntry> Activities => config.Activities;

    public void Load()
    {
        if (File.Exists(Path))
            config = JsonSerializer.Deserialize<AppConfig>(File.ReadAllText(Path)) ?? new();
    }

    public bool Add(ActivityEntry entry)
    {
        if (config.Activities.Any(a => a.ApplicationId == entry.ApplicationId))
        {
            return false;
        }

        config.Activities.Add(entry);
        Save();
        return true;
    }

    public void Remove(int index)
    {
        config.Activities.RemoveAt(index);
        Save();
    }

    private void Save()
    {
        string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true});
        File.WriteAllText(Path, json);
    }
}

public class ActivityEntry
{
    public string Name { get; set; } = "";
    public string ApplicationId { get; set; } = "";
}
public class AppConfig
{
    public List<ActivityEntry> Activities { get; set; } = new();
}