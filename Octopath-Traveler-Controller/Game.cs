using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler;

public class Game
{
    private MainConsoleView _view;
    private GameState _state = new();
    private BattleController _battleController;
    public Game(View view, string teamsFolder)
    {
        _view = new MainConsoleView(view, _state, teamsFolder);
        _battleController = new BattleController(_view, _state);
    }

    public void Play()
    {
        try
        {
            TeamsInfo teamsInfo = new TeamsInfo(_view.GetTeamsFilePath());
            TeamsBuilder teamsBuilder = new TeamsBuilder(_state, teamsInfo);
            teamsBuilder.Build();
            _state.ResetNextTurnQueue();
        }
        catch (InvalidOperationException exception)
        {
            _view.ShowInvalidTeamMessage();
            _battleController.IsGameStillGoing = false;
        }

        while (_battleController.IsGameStillGoing)
        {
            _battleController.RunBattleRound();
        }
    }
}