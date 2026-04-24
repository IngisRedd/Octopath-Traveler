namespace Octopath_Traveler_Model;

public class CombatActionInfo
{
    public string SkillName { get; set; }
    public List<Damage> Damages = new();
    public List<bool> IsBreakingPointAchieved = new();
    public List<bool> IsTravelerDefending = new();
    public List<int> HealValues = new();
}