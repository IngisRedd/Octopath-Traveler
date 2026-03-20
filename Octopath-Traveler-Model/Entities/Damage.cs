namespace Octopath_Traveler_Model;

public class Damage
{
    public int Value { get; set; }
    public string Type { get; set; }

    public Damage(int value, string type)
    {
        Value = value;
        Type = type;
    }
}