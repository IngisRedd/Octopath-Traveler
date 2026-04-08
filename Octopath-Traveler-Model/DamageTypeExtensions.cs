namespace Octopath_Traveler_Model;

public static class DamageTypeExtensions
{
    public static bool IsPhysical(this DamageType type)
    {
        return type switch
        {
            DamageType.Phys or
            DamageType.Sword or
            DamageType.Spear or
            DamageType.Dagger or
            DamageType.Axe or
            DamageType.Bow or
            DamageType.Stave => true,
            _ => false
        };
    }
    
    public static bool IsElemental(this DamageType type)
    {
        return type switch
        {
            DamageType.Elem or
            DamageType.Fire or
            DamageType.Ice or
            DamageType.Lightning or
            DamageType.Wind or
            DamageType.Light or
            DamageType.Darkness => true,
            _ => false
        };
    }
}