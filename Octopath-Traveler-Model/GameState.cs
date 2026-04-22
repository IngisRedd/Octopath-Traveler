namespace Octopath_Traveler_Model;

public class GameState
{
    public TravelerTeam TravelerTeam;
    public BeastTeam BeastTeam;
    public List<CombatUnit> AllUnits = new();
    public CombatUnit CurrentUnit;
    public Traveler CurrentTraveler => (Traveler)CurrentUnit;
    public Beast CurrentBeast => (Beast)CurrentUnit;
    public int RoundCounter = 0;
    public TurnQueue CurrentTurnQueue = new();
    public TurnQueue NextTurnQueue = new();
}