using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using System.Text.Json;

namespace Octopath_Traveler;

public class TeamsBuilder
{
    private GameState _gameState;
    private ParsedTeamsInfo _parsedTeamsInfo;
    
    public TeamsBuilder(GameState gameState, ParsedTeamsInfo parsedTeamsInfo)
    {
        _gameState = gameState;
        _parsedTeamsInfo = parsedTeamsInfo;
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
        CheckForUniqueness(_parsedTeamsInfo.TravelerNames);
        int travelerTeamMax = 4;
        CheckForMaxCapacity(_parsedTeamsInfo.TravelerNames, travelerTeamMax);

        foreach (string traveler in _parsedTeamsInfo.TravelerNames)
        {
            CheckForUniqueness(_parsedTeamsInfo.TravelerSkills[traveler]);
            int skillsMax = 8;
            CheckForMaxCapacity(_parsedTeamsInfo.TravelerSkills[traveler], skillsMax);                        
            
            CheckForUniqueness(_parsedTeamsInfo.TravelerPassiveSkills[traveler]);
            int passiveSkillsMax = 4;
            CheckForMaxCapacity(_parsedTeamsInfo.TravelerPassiveSkills[traveler], passiveSkillsMax);                        
        }
        
        CheckForUniqueness(_parsedTeamsInfo.BeastNames);
        int beastTeamMax = 5;
        CheckForMaxCapacity(_parsedTeamsInfo.BeastNames, beastTeamMax);
    }

    private void BuildTravelerTeam()
    {
        TravelerTeam travelerTeam = new TravelerTeam();
        
        Dictionary<string, TravelerJsonData> allTravelersInfo = LoadDictionaryFromJsonFile<TravelerJsonData>(
            "data/characters.json",
            t => t.Name
        );
        
        foreach (string travelerName in _parsedTeamsInfo.TravelerNames)
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
            newTraveler.PassiveSkills = _parsedTeamsInfo.TravelerPassiveSkills[travelerName];
            int initialBP = 1;
            newTraveler.BP = initialBP;
            
            travelerTeam.Units.Add(newTraveler);
            _gameState.AllUnits.Add(newTraveler);
        }
        
        _gameState.TravelerTeam = travelerTeam;
    }
    
    public Dictionary<string, T> LoadDictionaryFromJsonFile<T>(
        string jsonFilePath,
        Func<T, string> keySelector)
    {
        string json = File.ReadAllText(jsonFilePath);
        List<T> items = JsonSerializer.Deserialize<List<T>>(json);
        return items.ToDictionary(keySelector);
    }
    
    private void BuildBeastTeam()
    {
        BeastTeam beastTeam = new BeastTeam();
        
        Dictionary<string, BeastJsonData> allBeastsInfo = LoadDictionaryFromJsonFile<BeastJsonData>(
            "data/enemies.json",
            t => t.Name
        );
        
        foreach (string beastName in _parsedTeamsInfo.BeastNames)
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
        Dictionary<string, TravelerSkillJsonData> allSkillsInfo = LoadDictionaryFromJsonFile<TravelerSkillJsonData>(
            "data/skills.json",
            t => t.Name
        );

        foreach (Traveler traveler in _gameState.TravelerTeam.Units)
        {
            foreach (string skillName in _parsedTeamsInfo.TravelerSkills[traveler.Name])
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