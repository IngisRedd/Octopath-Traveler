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
            TeamsInfoParser teamsInfoParser = new TeamsInfoParser(_setupConsoleView.GetTeamsSetupInfo());
            ParsedTeamsInfo parsedTeamsInfo = teamsInfoParser.Parse();
                TeamsBuilder teamsBuilder = new TeamsBuilder(_state, parsedTeamsInfo);
            teamsBuilder.Build();
            GameStateUpdater.ResetNextTurnQueue(_state);
        }
        catch (InvalidOperationException exception)
        {
            _setupConsoleView.ShowInvalidTeamMessage();
            _battleController.IsGameStillGoing = false;
        }
    }
    
}