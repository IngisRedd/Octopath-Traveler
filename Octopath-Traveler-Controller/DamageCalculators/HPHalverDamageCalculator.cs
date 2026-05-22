using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public class HPHalverDamageCalculator : IDamageCalculator
{
    private CombatUnit _target { get; }
    private decimal _value { get; set; }

    public HPHalverDamageCalculator(CombatUnit target)
    {
        _target = target;
    }

    public Damage Calculate()
    {
        _value = _target.CurrentHP / 2;
        return new Damage(_value, DamageType.None);
    }
}