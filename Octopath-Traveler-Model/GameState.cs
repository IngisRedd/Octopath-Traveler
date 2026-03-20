namespace Octopath_Traveler_Model;

public class GameState
{
    public TravelerTeam TravelerTeam;
    public BeastTeam BeastTeam;
    public CombatUnit CurrentUnit;
    public int RoundCounter = 0;
    public List<CombatUnit> TurnQueue = new();
    public List<CombatUnit> NextTurnQueue = new();

    public void ResetTurnQueues()
    {
        TurnQueue.AddRange(TravelerTeam.AliveUnits);
        TurnQueue.AddRange(BeastTeam.AliveUnits);
        TurnQueue.Sort((unit1, unit2) => unit2.Speed.CompareTo(unit1.Speed));
        
        NextTurnQueue = TurnQueue.ToList();
        
        CurrentUnit = TurnQueue[0];
    }

    public void UpdateTurnQueue()
    {
        TurnQueue.RemoveAt(0);
    }
    public void UpdateCurrentUnit()
    {
        CurrentUnit = TurnQueue[0];
    }

}