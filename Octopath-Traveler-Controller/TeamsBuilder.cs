using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler;

public class TeamsBuilder
{
    private GameState gameState;
    private TeamsInfo _teamsInfo;
    
    public TeamsBuilder(GameState _gameState, TeamsInfo teamsInfo)
    {
        _gameState = _gameState;
        _teamsInfo = teamsInfo;
    }

    public void Build()
    {
        throw new Exception();
    }
}