using Octopath_Traveler_Model;

namespace Octopath_Traveler_View.ResultViews;

public class ConsoleViewFactory : IViewFactory
{
    private View _view; 
    private string _teamsFolder;

    public ConsoleViewFactory(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
    }
    
    public ISetupView CreateSetupView()
        => new SetupConsoleView(_view, _teamsFolder);
    
    public ICombatActionView CreateCombatActionView(GameState gameState)
        => new CombatActionConsoleView(_view, gameState);
    
    public IRoundView CreateRoundView(GameState gameState)
        => new RoundConsoleView(_view, gameState);
}