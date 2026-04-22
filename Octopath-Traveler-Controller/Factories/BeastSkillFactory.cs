using Octopath_Traveler_Model;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public class BeastSkillFactory : SkillFactory
{
    public override ISkill Create(SkillInfo skillInfo)
    {
        BeastSkillInfo beastSkillInfo = (BeastSkillInfo)skillInfo;

        if (IsItADamagingSkill(beastSkillInfo))
        {
            if (IsSkillSingleTarget(beastSkillInfo))
            {
                Stat stat = ParseStat(beastSkillInfo.Description);
                SelectionType selectionType = ParseSelection(beastSkillInfo.Description);
                return new SingleTargetDamagingBeastSkill(beastSkillInfo, stat, selectionType);
            }
            return new MultiTargetDamagingBeastSkill(beastSkillInfo);
        }
        else
        {
            throw new Exception("Error! Unknown skill type!.");
        }
    }

    private bool IsSkillSingleTarget(BeastSkillInfo beastSkillInfo)  // Repeated code
        => beastSkillInfo.Target == SkillTarget.Single;

    
    private SelectionType ParseSelection(string text)
    {
        if (text.Contains("menor"))
            return SelectionType.Lowest;

        if (text.Contains("mayor"))
            return SelectionType.Highest;

        return SelectionType.Highest; // or throw, depending on strictness
    }
    
    private readonly Dictionary<string, Stat> StatMap = new()
    {
        { "HP", Stat.HP },
        { "Speed", Stat.Speed },
        { "Phys Atk", Stat.PhysAtk },
        { "Phys Def", Stat.PhysDef },
        { "Elem Atk", Stat.ElemAtk },
        { "Elem Def", Stat.ElemDef }
    };

    private Stat ParseStat(string text)
    {
        foreach (var keyValuePair in StatMap)
        {
            if (text.Contains(keyValuePair.Key))
                return keyValuePair.Value;
        }

        throw new ArgumentException("Stat not recognized in description");
    }
}