using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public static class GameStateUpdater
{
    public static void StartOfRoundQueueUpdate(GameState gameState)
    {
        gameState.CurrentTurnQueue = gameState.NextTurnQueue.ToList();
        ResetNextTurnQueue(gameState);
        
        gameState.CurrentUnit = gameState.CurrentTurnQueue[0];
    }

    public static void ResetNextTurnQueue(GameState gameState)
    {
        gameState.NextTurnQueue.Clear();
        gameState.NextTurnQueue.AddRange(gameState.TravelerTeam.AliveUnits);
        gameState.NextTurnQueue.AddRange(gameState.BeastTeam.AliveUnits);
        gameState.NextTurnQueue.Sort((unit1, unit2) => unit2.Speed.CompareTo(unit1.Speed));
    }
    
    public static void EndOfTurnUpdateTurnQueues(GameState gameState)
    {
        gameState.CurrentTurnQueue.RemoveAt(0);
        
        gameState.CurrentTurnQueue = gameState.CurrentTurnQueue.Where(unit => unit.IsAlive).ToList();
        gameState.NextTurnQueue = gameState.NextTurnQueue.Where(unit => unit.IsAlive).ToList();
    }
    
    public static void UpdateCurrentUnit(GameState gameState)
    {
        gameState.CurrentUnit = gameState.CurrentTurnQueue[0];
    }

    public static void UpdateStatusEffectDuration(GameState gameState)
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