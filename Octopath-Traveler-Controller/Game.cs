using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler;

public class Game
{
    private SetupConsoleView _setupConsoleView;
    private GameState _state = new();
    private BattleController _battleController;
    public Game(View view, string teamsFolder)
    {
        _setupConsoleView = new SetupConsoleView(view, _state, teamsFolder);
        CombatActionConsoleView combatActionConsoleView = new CombatActionConsoleView(view, _state);
        RoundConsoleView roundConsoleView = new RoundConsoleView(view, _state);
        _battleController = new BattleController(_state, roundConsoleView, combatActionConsoleView);
    }

    public void Play()
    {
        TryGameSetup();
        
        while (_battleController.IsGameStillGoing)
        {
            _battleController.ExecuteBattleRound();
        }
    }

    private void TryGameSetup()
    {
        try
        {
            GameSetup();
        }
        catch (InvalidOperationException exception)
        {
            _setupConsoleView.ShowInvalidTeamMessage();
            _battleController.IsGameStillGoing = false;
        }
    }

    private void GameSetup()
    {
        TeamsInfoParser teamsInfoParser = new TeamsInfoParser(_setupConsoleView.GetTeamsSetupInfo());
        ParsedTeamsInfo parsedTeamsInfo = teamsInfoParser.Parse();
        TeamsBuilder teamsBuilder = new TeamsBuilder(_state, parsedTeamsInfo);
        teamsBuilder.Build();
        GameStateUpdater.ResetNextTurnQueue(_state);
    }
}