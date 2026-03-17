using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using System.Text.Json;

namespace Octopath_Traveler;

public class TeamsBuilder
{
    private GameState gameState;
    private TeamsInfo _teamsInfo;
    
    public TeamsBuilder(GameState _gameState, TeamsInfo teamsInfo)
    {
        _gameState = _gameState;
        _teamsInfo = teamsInfo;
    }

    public void Build()
    {
        string json = File.ReadAllText("data/characters.json");

        List<TravelerJsonData> travelerJsonData = JsonSerializer.Deserialize<List<TravelerJsonData>>(json);
        
        throw new Exception();
    }
    

    private void CheckForUniqueness(List<string> listToCheck, string itemToAdd)
    {
        if (listToCheck.Contains(itemToAdd))
            throw new InvalidOperationException("Item already exists.");
    }

    private void CheckForMaxCapacity(List<string> listToCheck, int maxSize)
    {
        if (listToCheck.Count >= maxSize)
            throw new InvalidOperationException("Maximum capacity reached.");
    }
}