using Octopath_Traveler_Model;

namespace Octopath_Traveler.GameSetup;

public static class PassiveSkillFactory
{
    // Este método está pensado como una fábrica rudimentaria y por eso es tan largo.
    // La idea es que evolucione harto para la E3.
    public static void ApplyPassiveSkillBonus(string passiveSkill, Traveler traveler)
    {
        int elementalAugmentationBonus = 50;
        int summonStrengthBonus = 50;
        int haleAndHeartyBonus = 500;
        int fleefootBonus = 50;
        int innerStregthBonus = 50;
        
        if (passiveSkill == "Elemental Augmentation")
        {
            traveler.ElemAtk += elementalAugmentationBonus;
        }
        if (passiveSkill == "Summon Strength")
        {
            traveler.PhysAtk += summonStrengthBonus;
        }
        if (passiveSkill == "Hale and Hearty")
        {
            traveler.MaxHP += haleAndHeartyBonus;
            traveler.CurrentHP = traveler.MaxHP;
        }
        if (passiveSkill == "Fleefoot")
        {
            traveler.Speed += fleefootBonus;
        }
        if (passiveSkill == "Inner Strength")
        {
            traveler.MaxSP += innerStregthBonus;
            traveler.CurrentSP = traveler.MaxSP;
        }
    }
}