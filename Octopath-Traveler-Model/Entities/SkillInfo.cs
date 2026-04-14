namespace Octopath_Traveler_Model;

public class SkillInfo
{
    public string Name { get; set; }
    public DamageType Type { get; set; }
    public string Description { get; set; }
    public double Modifier { get; set; }
    public SkillTarget Target { get; set; }
}