using System.Runtime.InteropServices;
using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;
using Octopath_Traveler.Skills;
using Octopath_Traveler.TurnControllers;

namespace Octopath_Traveler;

public static class TurnControllerFactory
{
    public static ITurnController Create(GameState gameState, RoundConsoleView roundConsoleView, CombatActionConsoleView combatActionConsoleView)
    {
        if (gameState.CurrentUnit is Traveler)
        {
            return new TravelerTurnController(gameState, roundConsoleView, combatActionConsoleView);
        }
        if (gameState.CurrentUnit is Beast)
        {
            return new BeastTurnController(gameState, roundConsoleView, combatActionConsoleView);
        }
        throw new ArgumentException($"Unknown turn controller type: {gameState.CurrentUnit.GetType().Name}!");
    }
}