using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public class ParsedTeamsInfo
{
    public List<string> BeastNames = new();
    public List<string> TravelerNames = new();
    public Dictionary<string, List<string>> TravelerSkills = new();
    public Dictionary<string, List<string>> TravelerPassiveSkills = new();
}