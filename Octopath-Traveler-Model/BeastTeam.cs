namespace Octopath_Traveler_Model;

public class BeastTeam
{
    public List<Beast> Units = new();
    public List<Beast> AliveUnits => Units.Where(beast => beast.CurrentHP > 0).ToList();
    public List<Beast> DeadUnits => Units.Where(beast => beast.CurrentHP == 0).ToList();

}