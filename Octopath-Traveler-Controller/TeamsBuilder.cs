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

        TravelerTeam travelerTeam = BuildTravelerTeam();
        // BeastTeam beastTeam = BuildBeastTeam();
        
        throw new Exception();
    }

    private TravelerTeam BuildTravelerTeam()
    {
        TravelerTeam travelerTeam = new TravelerTeam();

        string json = File.ReadAllText("data/characters.json");
        List<TravelerJsonData> deserializedJsonData = JsonSerializer.Deserialize<List<TravelerJsonData>>(json);
        Dictionary<string, TravelerJsonData> travelerInfo = deserializedJsonData.ToDictionary(
            traveler => traveler.Name,
            traveler => traveler
        );
        
        
        // foreach (KeyValuePair<string,List<string>> pair in _teamsInfo.TravelerAndSkills)
        // {
        //     string travelerName = pair.Key;
        //     List<string> travelerSkills = pair.Value;
        //     TravelerJsonData jsonData = travelerInfo[travelerName];    
        //     
        //     Traveler newTraveler = new Traveler();
        //     
        //     newTraveler.Name = travelerName;
        //     newTraveler.MaxHP = jsonData.Stats["HP"];
        //     newTraveler.CurrentHP = jsonData.Stats["HP"];
        //     newTraveler.PhysAtk = jsonData.Stats["PhysAtk"];
        //     newTraveler.PhysDef = jsonData.Stats["PhysDef"];
        //     newTraveler.ElemAtk = jsonData.Stats["ElemAtk"];
        //     newTraveler.ElemDef = jsonData.Stats["ElemDef"];
        //     newTraveler.Speed = jsonData.Stats["Speed"];
        //     newTraveler.MaxSP = jsonData.Stats["SP"];
        //     newTraveler.CurrentSP = jsonData.Stats["SP"];
        //
        //     newTraveler.Weapons = jsonData.Weapons;
        //     newTraveler.Skills = travelerSkills;4
        //
        //     
        // }
        
        return travelerTeam;
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