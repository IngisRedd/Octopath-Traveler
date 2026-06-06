using System.Data;
using Octopath_Traveler_Model;

namespace Octopath_Traveler_View.GUIViews.GUIStructures;

public static class GUIGameStateUpdater
{
    public static void Update(GUIGameState guiGameState, GameState realGameState)
    {
        guiGameState.Travelers = GetGUITravelerTeam(realGameState.TravelerTeam);
        guiGameState.Beasts = GetGUIBeastTeam(realGameState.BeastTeam);
        guiGameState.CurrentRoundTurns = GetTurnsQueueInString(realGameState.CurrentTurnQueue);
        guiGameState.NextRoundTurns = GetTurnsQueueInString(realGameState.NextTurnQueue);
    }

    private static List<GUITraveler> GetGUITravelerTeam(Combatants travelerTeam)
    {
        List<GUITraveler> guiTravelers = new List<GUITraveler>();
        foreach (Traveler realTraveler in travelerTeam)
        {
            GUITraveler newGUITraveler = new GUITraveler(
                realTraveler.Name,
                realTraveler.CurrentHP,
                realTraveler.MaxHP,
                realTraveler.CurrentSP,
                realTraveler.MaxSP,
                realTraveler.BP);
            
            guiTravelers.Add(newGUITraveler);
        }
        return guiTravelers;
    }
    
    private static List<GUIBeast> GetGUIBeastTeam(Combatants beastTeam)
    {
        List<GUIBeast> guiBeasts = new List<GUIBeast>();
        foreach (Beast realBeast in beastTeam)
        {
            GUIBeast newGUIBeast = new GUIBeast(
                realBeast.Name,
                realBeast.CurrentHP,
                realBeast.MaxHP,
                realBeast.CurrentShields);
            
            guiBeasts.Add(newGUIBeast);
        }
        return guiBeasts;
    }

    private static List<string> GetTurnsQueueInString(TurnQueue turnQueue)
    {
        List<string> turnsQueueInString = new List<string>();
        foreach (CombatUnit unit in turnQueue)
        {
            turnsQueueInString.Add(unit.Name);
        }
        return turnsQueueInString;
    }
}

