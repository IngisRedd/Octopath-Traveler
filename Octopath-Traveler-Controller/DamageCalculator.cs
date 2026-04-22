using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public class DamageCalculator
{

    private double _modifier;
    private CombatUnit _attacker { get; }
    private CombatUnit _target { get; }
    private DamageType _type { get; set; }
    private double _value { get; set; }

    private List<string> elementalTypes = new List<string> { };

    public DamageCalculator(double modifier, CombatUnit attacker, CombatUnit target, DamageType type)
    {
        _modifier = modifier;
        _attacker = attacker;
        _target = target;
        _type = type;
    }

    public Damage Calculate()
    {
        
        if (_type.IsPhysical())
        {
            _value = _attacker.PhysAtk * _modifier - _target.PhysDef;
        }
        else
        {
            _value = _attacker.ElemAtk * _modifier - _target.ElemDef;
        }
        ApplyStatusEffectEffects();
        _value = Math.Max(0, _value);
        
        return new Damage(_value, _type);
    }
    
    private void ApplyStatusEffectEffects()
    {
        double defendModifier = 0.5;
        double breakingPointModifier = 1.5;
        if (_target.StatusEffects[StatusType.Defend].IsActive)
        {
            _value = _value * defendModifier;
        }
        if (_target.StatusEffects[StatusType.BreakingPoint].IsActive)
        {
            _value = _value * breakingPointModifier;
        }
    }

    public static Damage ApplyModifier(Damage damage, double modifier)
    {   
        double newDamageValue = damage.Value * modifier;
        return new Damage(newDamageValue, damage.Type);
    }
}