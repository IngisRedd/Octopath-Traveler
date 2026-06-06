namespace Octopath_Traveler_Model;

public class SkillResultInfo
{
    public CombatUnit Target;
    public ResultType Type;
    public int Value;
    public DamageType DamageType;
    public bool IsTargetDefending;
    public bool HasEnteredBreakingPoint;
    public StatusType StatusEffectType;

    public SkillResultInfo(CombatUnit target, ResultType type, int value = 0,
        DamageType damageType = 0, bool isTargetDefending = false, bool hasEnteredBreakingPoint = false, StatusType statusEffectType = 0)
    {
        Target = target;
        Type = type;
        Value = value;
        DamageType = damageType;
        IsTargetDefending = isTargetDefending;
        HasEnteredBreakingPoint = hasEnteredBreakingPoint;
        StatusEffectType = statusEffectType;
    }
}