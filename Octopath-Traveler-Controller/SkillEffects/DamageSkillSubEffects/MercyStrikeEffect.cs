using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public static class MercyStrikeEffect
{
    public static Damage Apply(Damage damage, CombatUnit target)
    {
        int newDamageValue = damage.Value;
        if (IsDamageHigherThanHP(damage.Value, target.CurrentHP))
        {
            newDamageValue = target.CurrentHP - 1;
        }
        if (IsEnemyAlreadyAt1HP(newDamageValue)) 
        {
            newDamageValue = 0;
        }

        return new Damage(newDamageValue, damage.Type);
    }
    
    private static bool IsDamageHigherThanHP(int damage, int currentHP)
        => damage > currentHP;

    private static bool IsEnemyAlreadyAt1HP(int damage)
        => damage < 0;

}