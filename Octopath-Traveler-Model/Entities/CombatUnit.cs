namespace Octopath_Traveler_Model;

public abstract class CombatUnit
{
    public string Name { get; set; }
    public int MaxHP { get; set; }
    private int _hp { get; set; }
    public int CurrentHP {
        get => _hp;
        set => _hp = Math.Max(0, value);
    }
    public bool IsAlive => _hp > 0;
    public int PhysAtk { get; set; }
    public int PhysDef { get; set; }
    public int ElemAtk { get; set; }
    public int ElemDef { get; set; }
    public int Speed { get; set; }

    public Dictionary<StatusType, StatusEffect> StatusEffects { get; set; } = new()
    {
        { StatusType.Defend, new StatusEffect() }
    };

}