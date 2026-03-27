namespace Octopath_Traveler_Model;

public class Damage
{
    private double _value { get; set; }
    public int Value { get => (int)_value; }
    public string Type { get; }

    public Damage(double value, string type)
    {
        Type = type;
        _value = value;
        
    }
}