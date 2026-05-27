using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler_View.GUIViews.GUIStructures;
using Octopath_Traveler_View.ResultViews;
using Octopath_Traveler.Exceptions;
using OctopathTravelerGUI;

namespace Octopath_Traveler;

public class Game
{
    private ISetupView _setupView;
    private GameState _state = new();
    private BattleController _battleController;
    public Game(View view, string teamsFolder)
    {
        ConsoleViewFactory viewFactory = new ConsoleViewFactory(view, teamsFolder);
        _setupView = viewFactory.CreateSetupView();
        _battleController = new BattleController(_state, viewFactory);
    }

    public Game(OTGUI window)
    {
        GUIGameState guiGameState = new GUIGameState(_state);
        GUIViewFactory viewFactory = new GUIViewFactory(window, guiGameState);
        _setupView = viewFactory.CreateSetupView();
        _battleController = new BattleController(_state, viewFactory);
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
        catch (InvalidTeamsException exception)
        {
            _setupView.ShowInvalidTeamMessage();
            _battleController.IsGameStillGoing = false;
        }
    }

    private void GameSetup()
    {
        TeamsInfoParser teamsInfoParser = new TeamsInfoParser(_setupView.GetTeamsSetupInfo());
        ParsedTeamsInfo parsedTeamsInfo = teamsInfoParser.Parse();
        TeamsBuilder teamsBuilder = new TeamsBuilder(_state, parsedTeamsInfo);
        teamsBuilder.Build();
        GameStateUpdater.ResetNextTurnQueue(_state);
    }
}