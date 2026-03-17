namespace Octopath_Traveler_Model;

public class Beast : CombatUnit
{
    public string Skill { get; set; }
    public int MaxShields { get; set; }
    public int CurrentShields { get; set; }
    public List<string> Weaknesses { get; set; }

}