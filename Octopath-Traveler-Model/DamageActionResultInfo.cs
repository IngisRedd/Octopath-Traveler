namespace Octopath_Traveler_Model;

public class DamageActionResultInfo
{
    public List<CombatUnit> Targets;
    public List<Damage> Damages = new();
    public List<bool> IsBreakingPointAchieved = new();
    public List<bool> IsTravelerDefending = new();
}