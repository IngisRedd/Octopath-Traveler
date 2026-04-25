using System.Text.Json;
using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public static class Utils
{
    public static Dictionary<string, T> LoadJsonDataByName<T>(
        string jsonFilePath,
        Func<T, string> keySelector)
    {
        string json = File.ReadAllText(jsonFilePath);
        List<T> items = JsonSerializer.Deserialize<List<T>>(json);
        return items.ToDictionary(keySelector);
    }
    
    public static DamageType ParseDamageType(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return DamageType.None;

        if (Enum.TryParse(input, true, out DamageType result))
            return result;

        return DamageType.None;
    }
    
    public static void SetLast<T>(List<T> list, T item)
    {
        if (list.Count == 0) return;
        list[^1] = item;
    }
    
    public static void MoveItemInIndexToEnd<T>(List<T> list, int index)
    {
        if (index < 0 || index >= list.Count) return;

        var item = list[index];
        list.RemoveAt(index);
        list.Add(item);
    }
}