namespace Octopath_Traveler_Model;

public class Traveler : CombatUnit
{
    public int MaxSP { get; set; }
    public int CurrentSP { get; set; }
    public List<string> Weapons { get; set; }
    public List<string> Skills { get; set; }
    public List<string> PassiveSkills { get; set; }
    
    public int BP { get; set; }
}