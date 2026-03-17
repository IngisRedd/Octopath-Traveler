namespace Octopath_Traveler_Model;

public class BeastJsonData
{
    public string Name { get; set; }
    public Dictionary<string,int> Stats { get; set; }
    public string Skill { get; set; }
    public int Shields { get; set; }
    public List<string> Weaknesses { get; set; }

}