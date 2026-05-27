using Octopath_Traveler_Model;
using Octopath_Traveler;

namespace Octopath_Traveler_View.GUIViews;

public static class RoundGUIParser
{
    private static readonly Dictionary<string, CombatActionType> TextCombatToActionMap = new Dictionary<string, CombatActionType>
    {
        { "Ataque básico", CombatActionType.Attack },
        { "Usar Habilidad", CombatActionType.UseSkill },
        { "Defender", CombatActionType.Defend },
        { "Huir", CombatActionType.Flee }
    };

    public static CombatActionType ParseCombatAction(string selectedOption)
    {
        return TextCombatToActionMap[selectedOption];
    }
    
    public static DamageType ParseWeapon(string selectedOption)
    {
        return Utils.ParseDamageType(selectedOption);
    }

}