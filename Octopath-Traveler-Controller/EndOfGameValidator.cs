using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Exceptions;

namespace Octopath_Traveler;

public static class EndOfGameValidator
{
    public static void CheckIfGameIsOver(GameState gameState, GameConsoleView view)
    {
        if (AreAllBeastsDefeated(gameState))
        {
            view.ShowVictoryMessage();
            throw new GameOverException("All enemies defeated");
        }
        if (AreAllTravelersDefeated(gameState))
        {
            view.ShowLostGameMessage();
            throw new GameOverException("All travelers in team defeated");
        }
    }

    private static bool AreAllBeastsDefeated(GameState gameState)
        => gameState.BeastTeam.AliveUnits.Count <= 0;
    private  static bool AreAllTravelersDefeated(GameState gameState)
        => gameState.TravelerTeam.AliveUnits.Count <= 0;
}