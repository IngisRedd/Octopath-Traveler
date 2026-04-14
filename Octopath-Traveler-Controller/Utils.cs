using System.Text.Json;
using Octopath_Traveler_Model;
using Octopath_Traveler_View;

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

    public static int ReadPlayerInput(MainConsoleView view)
    {
        string input = view.AskForPlayerInput();
        return Convert.ToInt32(input);
    }
    
    public static Beast SelectTarget(GameState gameState, MainConsoleView view)
    {
        view.ShowAvailableTargets();
        int selectedIndex = Utils.ReadPlayerInput(view) - 1;
        return gameState.BeastTeam.AliveUnits[selectedIndex];
    }

    public static int AskForBPToUseIfAvailable(GameState gameState, MainConsoleView view)
    {
        if (!gameState.CurrentTraveler.AreThereAnyBPLeft)
            return 0;

        view.AskForBPUsage();
        return Utils.ReadPlayerInput(view);
    }
    
    public static DamageType ParseDamageType(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return DamageType.None;

        if (Enum.TryParse(input, true, out DamageType result))
            return result;

        return DamageType.None;
    }
}