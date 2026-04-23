namespace Octopath_Traveler_Model;

public class Damage
{
    private double _value { get; set; }
    public int Value { get => (int)_value; }
    public DamageType Type { get; }

    public Damage(double value, DamageType type)
    {
        Type = type;
        _value = value;
    }
}