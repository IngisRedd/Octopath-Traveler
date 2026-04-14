using Octopath_Traveler_Model;

namespace Octopath_Traveler.GameSetup;

public class SkillsBuilder
{
    private GameState _gameState;
    private ParsedTeamsInfo _parsedTeamsInfo;

    public SkillsBuilder(GameState gameState, ParsedTeamsInfo parsedTeamsInfo)
    {
        _gameState = gameState;
        _parsedTeamsInfo = parsedTeamsInfo;
    }

    public void BuildTravelerSkills()
    {
        Dictionary<string, TravelerSkillJsonData> allSkillsData = Utils.LoadJsonDataByName<TravelerSkillJsonData>(
            "data/skills.json",
            t => t.Name
        );

        foreach (Traveler traveler in _gameState.TravelerTeam.Units)
        {
            foreach (string skillName in _parsedTeamsInfo.TravelerSkills[traveler.Name])
            {
                TravelerSkillInfo newSkillInfo = CreateTravelerSkill(allSkillsData[skillName]);
            
                traveler.Skills.Add(newSkillInfo);
            }            
        }
    }
    
    private TravelerSkillInfo CreateTravelerSkill(TravelerSkillJsonData data)
    {
        return new TravelerSkillInfo
        {
            Name = data.Name,
            SP = data.SP,
            Type = Utils.ParseDamageType(data.Type),
            Description = data.Description,
            Target = Enum.Parse<SkillTarget>(data.Target),
            Modifier = data.Modifier,
            Boost = data.Boost
        };
    }
    
    public void BuildBeastSkills()
    {
        Dictionary<string, BeastSkillJsonData> allSkillsData = Utils.LoadJsonDataByName<BeastSkillJsonData>(
            "data/beast_skills.json",
            t => t.Name
        );

        foreach (Beast beast in _gameState.BeastTeam.Units)
        {
            BeastSkillInfo newSkillInfo = CreateBeastSkill(allSkillsData[beast.SkillName]);
            beast.Skill = newSkillInfo;
        }
    }
    
    private BeastSkillInfo CreateBeastSkill(BeastSkillJsonData data)
    {
        return new BeastSkillInfo
        {
            Name = data.Name,
            Type = DetectDamageType(data.Description),
            Modifier = data.Modifier,
            Description = data.Description,
            Target = Enum.Parse<SkillTarget>(data.Target),
            Hits = data.Hits
        };
    }

    private DamageType DetectDamageType(string text)
    {
        if (text.Contains("ataque físico"))
            return DamageType.Phys;

        if (text.Contains("ataque elemental"))
            return DamageType.Elem;

        return DamageType.None;
    }
}