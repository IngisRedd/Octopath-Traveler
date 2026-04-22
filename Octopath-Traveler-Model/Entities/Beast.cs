namespace Octopath_Traveler_Model;

public class Beast : CombatUnit
{
    public string SkillName { get; set; }
    public BeastSkillInfo Skill { get; set; }
    public int MaxShields { get; set; }
    public int CurrentShields { get; set; }
    public List<DamageType> Weaknesses { get; set; }
    
    public bool IsWeakToDamageType(DamageType damageType)
        => Weaknesses.Contains(damageType);

}