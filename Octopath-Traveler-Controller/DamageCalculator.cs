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
        ApplyWeaknessAndBreakingPoint();
        ApplyStatusEffectEffects();
        _value = Math.Max(0, _value);
        
        return new Damage(_value, _type);
    }

    private void ApplyWeaknessAndBreakingPoint()
    {
        double weaknessModifier = 0.5;
        double breakingPointModifier = 0.5;
        
        int TargetIsWeak = 0;
        if (_target is Beast)
        {
            Beast beast = (Beast)_target;
            TargetIsWeak = Convert.ToInt32(beast.IsWeakToDamageType(_type));
        }
        
        bool targetIsInBP = _target.StatusEffects[StatusType.BreakingPoint].IsActive;
        int targetIsInBPToInt = Convert.ToInt32(targetIsInBP);
        
        double totalModifier = 1 + TargetIsWeak * weaknessModifier + targetIsInBPToInt * breakingPointModifier;
        _value = _value * totalModifier;
    }
    
    private void ApplyStatusEffectEffects()
    {
        double defendModifier = 0.5;
        if (_target.StatusEffects[StatusType.Defend].IsActive)
        {
            _value = _value * defendModifier;
        }
    }
}