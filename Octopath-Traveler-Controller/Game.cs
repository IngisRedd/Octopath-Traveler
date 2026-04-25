using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler;

public class Game
{
    private SetupConsoleView _view;
    private GameState _state = new();
    private BattleController _battleController;
    public Game(View view, string teamsFolder)
    {
        _view = new SetupConsoleView(view, _state, teamsFolder);
        CombatActionConsoleView combatActionConsoleView = new CombatActionConsoleView(view, _state);
        RoundConsoleView roundConsoleView = new RoundConsoleView(view, _state);
        _battleController = new BattleController(_state, _view, roundConsoleView, combatActionConsoleView);
    }

    public void Play()
    {
        GameSetup();
        
        while (_battleController.IsGameStillGoing)
        {
            _battleController.ExecuteBattleRound();
        }
    }

    private void GameSetup()
    {
        try
        {
            TeamsInfoParser teamsInfoParser = new TeamsInfoParser();
            ParsedTeamsInfo parsedTeamsInfo = teamsInfoParser.ParseFileData(_view.GetTeamsFilePath());
            TeamsBuilder teamsBuilder = new TeamsBuilder(_state, parsedTeamsInfo);
            teamsBuilder.Build();
            GameStateUpdater.ResetNextTurnQueue(_state);
        }
        catch (InvalidOperationException exception)
        {
            _view.ShowInvalidTeamMessage();
            _battleController.IsGameStillGoing = false;
        }
    }
}