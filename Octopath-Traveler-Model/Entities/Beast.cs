namespace Octopath_Traveler_Model;

public class Beast : CombatUnit
{
    public string SkillName { get; set; }
    public BeastSkillInfo Skill { get; set; }
    public int MaxShields { get; set; }
    private int _currentShields { get; set; }
    public int CurrentShields {
        get => _currentShields;
        set => _currentShields = Math.Max(0, value);
    }
    public List<DamageType> Weaknesses { get; set; }
    
    public bool IsWeakToDamageType(DamageType damageType)
        => Weaknesses.Contains(damageType);

}