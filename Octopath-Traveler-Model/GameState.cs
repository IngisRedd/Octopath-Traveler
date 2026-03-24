namespace Octopath_Traveler_Model;

public class GameState
{
    public TravelerTeam TravelerTeam;
    public BeastTeam BeastTeam;
    public List<CombatUnit> AllUnits = new();
    public CombatUnit CurrentUnit;
    public Traveler CurrentTraveler => (Traveler)CurrentUnit;
    public int RoundCounter = 0;
    public List<CombatUnit> CurrentTurnQueue = new();
    public List<CombatUnit> NextTurnQueue = new();

    public void StartOfRoundQueueUpdate()
    {
        CurrentTurnQueue = NextTurnQueue.ToList();
        ResetNextTurnQueue();
        
        CurrentUnit = CurrentTurnQueue[0];
    }

    public void ResetNextTurnQueue()
    {
        NextTurnQueue.Clear();
        NextTurnQueue.AddRange(TravelerTeam.AliveUnits);
        NextTurnQueue.AddRange(BeastTeam.AliveUnits);
        NextTurnQueue.Sort((unit1, unit2) => unit2.Speed.CompareTo(unit1.Speed));
    }
    
    public void EndOfTurnUpdateTurnQueues()
    {
        CurrentTurnQueue.RemoveAt(0);
        
        CurrentTurnQueue = CurrentTurnQueue.Where(unit => unit.IsAlive).ToList();
        NextTurnQueue = NextTurnQueue.Where(unit => unit.IsAlive).ToList();
    }
    
    public void UpdateCurrentUnit()
    {
        CurrentUnit = CurrentTurnQueue[0];
    }

    public void UpdateStatusEffectDuration()
    {
        foreach (CombatUnit unit in AllUnits)
        {
            foreach (StatusEffect statusEffect in unit.StatusEffects.Values)
            {
                if (statusEffect.IsActive)
                {
                    statusEffect.Duration--;
                }
            }
        }
    }

}