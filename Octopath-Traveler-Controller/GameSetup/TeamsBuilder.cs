using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using System.Text.Json;
using Octopath_Traveler.GameSetup;

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
        SkillsBuilder skillsBuilder = new SkillsBuilder(_gameState, _parsedTeamsInfo);
        skillsBuilder.BuildTravelerSkills();
        skillsBuilder.BuildBeastSkills();
    }

    private void BuildTravelerTeam()
    {
        TravelerTeam travelerTeam = new TravelerTeam();
        
        Dictionary<string, TravelerJsonData> allTravelersData = Utils.LoadJsonDataByName<TravelerJsonData>(
            "data/characters.json",
            t => t.Name
        );
        
        foreach (string name in _parsedTeamsInfo.TravelerNames)
        {
            Traveler newTraveler = CreateTraveler(allTravelersData[name]);
            newTraveler.PassiveSkills = _parsedTeamsInfo.TravelerPassiveSkills[name];
            foreach (string skill in newTraveler.PassiveSkills)
            {
                PassiveSkillFactory.ApplyPassiveSkillBonus(skill, newTraveler);
            }
            
            travelerTeam.Units.Add(newTraveler);
            _gameState.AllUnits.Add(newTraveler);
        }
        
        _gameState.TravelerTeam = travelerTeam;
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
        
        Dictionary<string, BeastJsonData> allBeastsData = Utils.LoadJsonDataByName<BeastJsonData>(
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
        List<string> weaknessesInString = data.Weaknesses;
        IEnumerable<DamageType> weaknesses = weaknessesInString.Select(Utils.ParseDamageType);

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
           
            SkillName = data.Skill,
            MaxShields = data.Shields,
            CurrentShields = data.Shields,
            Weaknesses = weaknesses.ToList()
        };
    }
    

}