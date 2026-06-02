using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler;

public static class TravelerSkillInfoConfigurator
{
    public static void Configure(SkillInfo selectedSkillInfo, IRoundView view)
    {
        if (selectedSkillInfo.Name == "Nightmare Chimera")
        {
            List<DamageType> weapons = new List<DamageType>
            {
                DamageType.Sword, DamageType.Spear, DamageType.Dagger, DamageType.Axe, DamageType.Bow, DamageType.Stave
            };
            selectedSkillInfo.Type = view.SelectWeapon(weapons);
        }

    }
}