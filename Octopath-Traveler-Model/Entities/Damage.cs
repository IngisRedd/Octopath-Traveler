namespace Octopath_Traveler_Model;

public class Damage
{
    private decimal _value { get; set; }
    public int Value { get => (int)Math.Floor(_value); }
    public decimal ValueInDecimal { get => _value; }
    public DamageType Type { get; }
    public bool SkipsDefend = false;

    public Damage(decimal value, DamageType type)
    {
        Type = type;
        _value = value;
    }
}