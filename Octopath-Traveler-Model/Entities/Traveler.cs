namespace Octopath_Traveler_Model;

public class Traveler : CombatUnit
{
    public int MaxSP { get; set; }
    private int _sp { get; set; }
    public int CurrentSP {
        get => _sp;
        set => _sp = Math.Min(Math.Max(0, value), MaxSP);
    }

    public List<DamageType> Weapons { get; set; }
    public List<TravelerSkillInfo> Skills { get; set; } = new();
    public List<TravelerSkillInfo> AvailableSkills => Skills.Where(skill => skill.SP <= CurrentSP).ToList();
    public List<string> PassiveSkills { get; set; }
    
    public int BP { get; set; }
    public bool AreThereAnyBPLeft => (BP > 0);
    public bool UsedBPLastTurn = false;

    public void UseBP(int bpToUse)
    {
        BP -= bpToUse;
        if (bpToUse > 0)
        {
            UsedBPLastTurn = true;
        }
    }

}