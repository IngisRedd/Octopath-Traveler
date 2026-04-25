namespace Octopath_Traveler_Model;

public class TravelerTeam
{
    public List<Traveler> Units = new(); 
    public List<Traveler> AliveUnits => Units.Where(traveler => traveler.CurrentHP > 0).ToList();
    public List<Traveler> DeadUnits => Units.Where(traveler => traveler.CurrentHP == 0).ToList();
}