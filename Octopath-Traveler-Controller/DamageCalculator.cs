using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public class DamageCalculator
{

    private double _modifier;
    private CombatUnit _attacker { get; }
    private CombatUnit _target { get; }
    private string _type { get; set; }
    private double _value { get; set; }

    public DamageCalculator(double modifier, CombatUnit attacker, CombatUnit target, string type)
    {
        _modifier = modifier;
        _attacker = attacker;
        _target = target;
        _type = type;
    }

    public Damage Calculate()
    {
        _value = _attacker.PhysAtk * _modifier - _target.PhysDef;
        ApplyStatusEffectEffects();
        _value = Math.Max(0, _value);
        
        return new Damage(_value, _type);
    }
    
    private void ApplyStatusEffectEffects()
    {
        double defendBonus = 0.5;
        if (_target.StatusEffects[StatusType.Defend].IsActive)
        {
            _value = _value * defendBonus;
        }
    }
}