namespace Octopath_Traveler_Model;

public class TravelerTeam
{
    public List<Traveler> Units = new(); 
    public List<Traveler> AliveUnits => Units.Where(traveler => traveler.CurrentHP > 0).ToList();
    public Traveler HealthiestUnit => Units.MaxBy(traveler => traveler.CurrentHP);

    public void IncreaseBPs()
    {
        int maxBP = 5;
        foreach (Traveler traveler in Units)
        {
            if (traveler.BP < maxBP)
            {
                traveler.BP++;
            }
        }
    }
}