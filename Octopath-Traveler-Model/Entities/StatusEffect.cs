namespace Octopath_Traveler_Model;

public class StatusEffect
{
    public int Duration = 0;
    public bool IsActive => Duration > 0;
}