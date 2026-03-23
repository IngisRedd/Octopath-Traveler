using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using System.Text.Json;

namespace Octopath_Traveler;

public class TeamsBuilder
{
    private GameState _gameState;
    private TeamsInfo _teamsInfo;
    
    public TeamsBuilder(GameState gameState, TeamsInfo teamsInfo)
    {
        _gameState = gameState;
        _teamsInfo = teamsInfo;
    }

    public void Build()
    {
        ValidateTeams();
        BuildTravelerTeam();
        BuildBeastTeam();
        BuildTravelerSkills();
    }

    private void ValidateTeams()
    {
        CheckForUniqueness(_teamsInfo.TravelerNames);
        int travelerTeamMax = 4;
        CheckForMaxCapacity(_teamsInfo.TravelerNames, travelerTeamMax);

        foreach (string traveler in _teamsInfo.TravelerNames)
        {
            CheckForUniqueness(_teamsInfo.TravelerSkills[traveler]);
            int skillsMax = 8;
            CheckForMaxCapacity(_teamsInfo.TravelerSkills[traveler], skillsMax);                        
            
            CheckForUniqueness(_teamsInfo.TravelerPassiveSkills[traveler]);
            int passiveSkillsMax = 4;
            CheckForMaxCapacity(_teamsInfo.TravelerPassiveSkills[traveler], passiveSkillsMax);                        
        }
        
        CheckForUniqueness(_teamsInfo.BeastNames);
        int beastTeamMax = 5;
        CheckForMaxCapacity(_teamsInfo.BeastNames, beastTeamMax);
    }

    private void BuildTravelerTeam()
    {
        TravelerTeam travelerTeam = new TravelerTeam();

        string jsonFilePath = "data/characters.json";
        string json = File.ReadAllText(jsonFilePath);
        List<TravelerJsonData> deserializedJsonData = JsonSerializer.Deserialize<List<TravelerJsonData>>(json);
        Dictionary<string, TravelerJsonData> allTravelersInfo = deserializedJsonData.ToDictionary(
            traveler => traveler.Name,
            traveler => traveler
        );
        
        
        foreach (string travelerName in _teamsInfo.TravelerNames)
        {
            TravelerJsonData jsonData = allTravelersInfo[travelerName];    
            
            Traveler newTraveler = new Traveler();
            
            newTraveler.Name = travelerName;
            newTraveler.MaxHP = jsonData.Stats["HP"];
            newTraveler.CurrentHP = jsonData.Stats["HP"];
            newTraveler.PhysAtk = jsonData.Stats["PhysAtk"];
            newTraveler.PhysDef = jsonData.Stats["PhysDef"];
            newTraveler.ElemAtk = jsonData.Stats["ElemAtk"];
            newTraveler.ElemDef = jsonData.Stats["ElemDef"];
            newTraveler.Speed = jsonData.Stats["Speed"];
            newTraveler.MaxSP = jsonData.Stats["SP"];
            newTraveler.CurrentSP = jsonData.Stats["SP"];
        
            newTraveler.Weapons = jsonData.Weapons;
            newTraveler.PassiveSkills = _teamsInfo.TravelerPassiveSkills[travelerName];
            int initialBP = 1;
            newTraveler.BP = initialBP;
            
            travelerTeam.Units.Add(newTraveler);
            _gameState.AllUnits.Add(newTraveler);
        }
        
        _gameState.TravelerTeam = travelerTeam;
    }
    
    private void BuildBeastTeam()
    {
        BeastTeam beastTeam = new BeastTeam();

        string jsonFilePath = "data/enemies.json";
        string json = File.ReadAllText(jsonFilePath);
        List<BeastJsonData> deserializedJsonData = JsonSerializer.Deserialize<List<BeastJsonData>>(json);
        Dictionary<string, BeastJsonData> allBeastsInfo = deserializedJsonData.ToDictionary(
            beast => beast.Name,
            beast => beast
        );
        
        
        foreach (string beastName in _teamsInfo.BeastNames)
        {
            BeastJsonData jsonData = allBeastsInfo[beastName];    
            
            Beast newBeast = new Beast();
            
            newBeast.Name = beastName;
            newBeast.MaxHP = jsonData.Stats["HP"];
            newBeast.CurrentHP = jsonData.Stats["HP"];
            newBeast.PhysAtk = jsonData.Stats["PhysAtk"];
            newBeast.PhysDef = jsonData.Stats["PhysDef"];
            newBeast.ElemAtk = jsonData.Stats["ElemAtk"];
            newBeast.ElemDef = jsonData.Stats["ElemDef"];
            newBeast.Speed = jsonData.Stats["Speed"];
        
            newBeast.Skill = jsonData.Skill;
            newBeast.MaxShields = jsonData.Shields;
            newBeast.CurrentShields = jsonData.Shields;
            newBeast.Weaknesses = jsonData.Weaknesses;
            
            beastTeam.Units.Add(newBeast);
            _gameState.AllUnits.Add(newBeast);
        }
        
        _gameState.BeastTeam = beastTeam;
    }
    
    private void BuildTravelerSkills()
    {
        string jsonFilePath = "data/skills.json";
        string json = File.ReadAllText(jsonFilePath);
        List<TravelerSkillJsonData> deserializedJsonData = JsonSerializer.Deserialize<List<TravelerSkillJsonData>>(json);
        Dictionary<string, TravelerSkillJsonData> allSkillsInfo = deserializedJsonData.ToDictionary(
            skill => skill.Name,
            skill => skill
        );

        foreach (Traveler traveler in _gameState.TravelerTeam.Units)
        {
            foreach (string skillName in _teamsInfo.TravelerSkills[traveler.Name])
            {
                TravelerSkillJsonData jsonData = allSkillsInfo[skillName];    
            
                Skill newSkill = new Skill();
            
                newSkill.Name = skillName;
                newSkill.SP = jsonData.SP;
                newSkill.Type = jsonData.Type;
                newSkill.Description = jsonData.Description;
                newSkill.Target = jsonData.Target;
                newSkill.Modifier = jsonData.Modifier;
                newSkill.Boost = jsonData.Boost;
            
                traveler.Skills.Add(newSkill);
            }            
        }
    }

    private void CheckForUniqueness(List<string> listToCheck)
    {
        if (listToCheck.Count != listToCheck.Distinct().Count())
            throw new InvalidOperationException("There are repeated items in list.");
    }

    private void CheckForMaxCapacity(List<string> listToCheck, int maxSize)
    {
        if (listToCheck.Count > maxSize)
            throw new InvalidOperationException("Maximum capacity reached.");
    }
}