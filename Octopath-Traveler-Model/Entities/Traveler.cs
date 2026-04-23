namespace Octopath_Traveler_Model;

public class Traveler : CombatUnit
{
    public int MaxSP { get; set; }
    public int CurrentSP { get; set; }
    public List<string> Weapons { get; set; }
    public List<TravelerSkillInfo> Skills { get; set; } = new();
    public List<TravelerSkillInfo> AvailableSkills => Skills.Where(skill => skill.SP <= CurrentSP).ToList();
    public List<string> PassiveSkills { get; set; }
    
    public int BP { get; set; }
    public bool AreThereAnyBPLeft => (BP > 0);

}