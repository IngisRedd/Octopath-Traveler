using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public static class GameStateUpdater
{
    public static void PerformStartOfRoundUpdates(GameState gameState)
    {
        gameState.RoundCounter++;
        StartOfRoundQueueUpdate(gameState);
    }
    
    public static void StartOfRoundQueueUpdate(GameState gameState)
    {
        gameState.CurrentTurnQueue = gameState.NextTurnQueue.Copy();
        ResetNextTurnQueue(gameState);
        
        gameState.CurrentUnit = gameState.CurrentTurnQueue[0];
    }

    public static void ResetNextTurnQueue(GameState gameState)
    {
        gameState.NextTurnQueue.Clear();
        gameState.NextTurnQueue.AddRange(gameState.TravelerTeam.Units);
        gameState.NextTurnQueue.AddRange(gameState.BeastTeam.Units);
        gameState.NextTurnQueue.RemoveAll(unit => !unit.IsGoingToActNextTurn);
        
        foreach (CombatUnit unit in gameState.NextTurnQueue)
        {
            if (unit.IsRecoveringFromBreakingPointNextRound)
            {
                gameState.NextTurnQueue.ApplyPriority(unit, TurnPriorityLevel.OutOfBreakingPoint);
            }
        }
    }
    
    public static void UpdateCurrentUnit(GameState gameState)
    {
        gameState.CurrentUnit = gameState.CurrentTurnQueue[0];
    }

    public static void EndOfTurnUpdate(GameState gameState)
    {
        EndOfTurnUpdateTurnQueues(gameState);
        gameState.SkillEffectResults = new();;
    }
    
    private static void EndOfTurnUpdateTurnQueues(GameState gameState)
    {
        gameState.CurrentTurnQueue.RemoveAt(0);
        
        gameState.CurrentTurnQueue.RemoveAll(unit => !unit.IsAlive);
        gameState.NextTurnQueue.RemoveAll(unit => !unit.IsAlive);
    }
    
    public static void PerformEndOfRoundUpdates(GameState gameState)
    {
        IncreaseTravelerBPs(gameState);
        
        ResetShieldsOfBeastsRecoveringFromBreakingPoint(gameState);
        UpdateStatusEffectDuration(gameState);
    }

    private static void IncreaseTravelerBPs(GameState gameState)
    {
        int maxBPs = 5;
        foreach (Traveler traveler in gameState.TravelerTeam.AliveUnits)
        {
            if (traveler.BP < maxBPs)
            {
                traveler.BP++;
            }
        }
    }

    private static void ResetShieldsOfBeastsRecoveringFromBreakingPoint(GameState gameState)
    {
        foreach (CombatUnit unit in gameState.NextTurnQueue)
        {
            if (unit.IsRecoveringFromBreakingPointNextRound)
            {
                Beast beast = (Beast)unit;
                beast.CurrentShields = beast.MaxShields;
            }
        }
    }
    
    private static void UpdateStatusEffectDuration(GameState gameState)
    {
        foreach (CombatUnit unit in gameState.AllUnits)
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