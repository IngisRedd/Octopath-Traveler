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
        TeamsValidator.Validate(_parsedTeamsInfo);
        BuildTravelerTeam();
        BuildBeastTeam();
        BuildTravelerSkills();
    }

    private void BuildTravelerTeam()
    {
        TravelerTeam travelerTeam = new TravelerTeam();
        
        Dictionary<string, TravelerJsonData> allTravelersData = LoadJsonDataByName<TravelerJsonData>(
            "data/characters.json",
            t => t.Name
        );
        
        foreach (string name in _parsedTeamsInfo.TravelerNames)
        {
            Traveler newTraveler = CreateTraveler(allTravelersData[name]);
            newTraveler.PassiveSkills = _parsedTeamsInfo.TravelerPassiveSkills[name];
            
            travelerTeam.Units.Add(newTraveler);
            _gameState.AllUnits.Add(newTraveler);
        }
        
        _gameState.TravelerTeam = travelerTeam;
    }
    
    public Dictionary<string, T> LoadJsonDataByName<T>(
        string jsonFilePath,
        Func<T, string> keySelector)
    {
        string json = File.ReadAllText(jsonFilePath);
        List<T> items = JsonSerializer.Deserialize<List<T>>(json);
        return items.ToDictionary(keySelector);
    }
    
    private Traveler CreateTraveler(TravelerJsonData data)
    {
        int initialBP = 1;

        return new Traveler
        {
            Name = data.Name,
            MaxHP = data.Stats["HP"],
            CurrentHP = data.Stats["HP"],
            PhysAtk = data.Stats["PhysAtk"],
            PhysDef = data.Stats["PhysDef"],
            ElemAtk = data.Stats["ElemAtk"],
            ElemDef = data.Stats["ElemDef"],
            Speed = data.Stats["Speed"],
            MaxSP = data.Stats["SP"],
            CurrentSP = data.Stats["SP"],
            
            Weapons = data.Weapons,
            BP = initialBP
        };
    }
    
    private void BuildBeastTeam()
    {
        BeastTeam beastTeam = new BeastTeam();
        
        Dictionary<string, BeastJsonData> allBeastsData = LoadJsonDataByName<BeastJsonData>(
            "data/enemies.json",
            t => t.Name
        );
        
        foreach (string beastName in _parsedTeamsInfo.BeastNames)
        {
            Beast newBeast = CreateBeast(allBeastsData[beastName]);
            
            beastTeam.Units.Add(newBeast);
            _gameState.AllUnits.Add(newBeast);
        }
        
        _gameState.BeastTeam = beastTeam;
    }
    
    private Beast CreateBeast(BeastJsonData data)
    {
        return new Beast
        {
            Name = data.Name,
            MaxHP = data.Stats["HP"],
            CurrentHP = data.Stats["HP"],
            PhysAtk = data.Stats["PhysAtk"],
            PhysDef = data.Stats["PhysDef"],
            ElemAtk = data.Stats["ElemAtk"],
            ElemDef = data.Stats["ElemDef"],
            Speed = data.Stats["Speed"],
           
            Skill = data.Skill,
            MaxShields = data.Shields,
            CurrentShields = data.Shields,
            Weaknesses = data.Weaknesses
        };
    }
    
    private void BuildTravelerSkills()
    {
        Dictionary<string, TravelerSkillJsonData> allSkillsData = LoadJsonDataByName<TravelerSkillJsonData>(
            "data/skills.json",
            t => t.Name
        );

        foreach (Traveler traveler in _gameState.TravelerTeam.Units)
        {
            foreach (string skillName in _parsedTeamsInfo.TravelerSkills[traveler.Name])
            {
                Skill newSkill = CreateSkill(allSkillsData[skillName]);
            
                traveler.Skills.Add(newSkill);
            }            
        }
    }
    
    private Skill CreateSkill(TravelerSkillJsonData data)
    {
        return new Skill
        {
            Name = data.Name,
            SP = data.SP,
            Type = data.Type,
            Description = data.Description,
            Target = data.Target,
            Modifier = data.Modifier,
            Boost = data.Boost
        };
    }
}