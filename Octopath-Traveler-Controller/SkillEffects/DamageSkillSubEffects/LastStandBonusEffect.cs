using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public static class LastStandBonusEffect
{
    public static Damage Apply(Damage damage, CombatUnit attacker)
    {
        decimal value = damage.ValueInDecimal;
        int lostHP = attacker.MaxHP - attacker.CurrentHP;
        int lostHPPercentage = (int)(100m * lostHP / attacker.MaxHP);
        decimal damageBonusPerPercentage = 0.03m;
        decimal totalDamageBonus = lostHPPercentage * damageBonusPerPercentage;
        decimal newDamageValue = value * (1 + totalDamageBonus);
        
        return new Damage(newDamageValue, damage.Type);
    }
}