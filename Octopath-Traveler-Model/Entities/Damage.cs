namespace Octopath_Traveler_Model;

public class Damage
{
    private double _value { get; set; }
    public int Value { get => (int)_value; }
    public string Type { get; }
    private CombatUnit _target { get; }

    public Damage(double modifier, CombatUnit attacker, CombatUnit target, string type)
    {
        Type = type;
        _target = target;
        _value = attacker.PhysAtk * modifier - target.PhysDef;
        
        ApplyStatusEffectEffects();
        _value = Math.Max(0, _value);
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