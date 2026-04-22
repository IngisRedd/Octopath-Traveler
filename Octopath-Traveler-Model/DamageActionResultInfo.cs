namespace Octopath_Traveler_Model;

public class DamageActionResultInfo
{
    public List<CombatUnit> Targets;
    public List<Damage> Damages = new List<Damage>();
    public List<bool> IsBreakingPointAchieved = new List<bool>();
}