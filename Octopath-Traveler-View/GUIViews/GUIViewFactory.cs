using Octopath_Traveler_Model;
using Octopath_Traveler_View.GUIViews.GUIStructures;
using OctopathTravelerGUI;

namespace Octopath_Traveler_View.ResultViews;

public class GUIViewFactory : IViewFactory
{
    private OTGUI _window; 
    private GUIGameState _guiGameState;

    public GUIViewFactory(OTGUI window, GUIGameState guiGameState)
    {
        _window = window;
        _guiGameState = guiGameState;
    }
    
    public ISetupView CreateSetupView()
        => new SetupGUIView(_window);
    
    public ICombatActionView CreateCombatActionView(GameState gameState)
        => new CombatActionGUIView(_window, _guiGameState);
    
    public IRoundView CreateRoundView(GameState gameState)
        => new RoundGUIView(_window, gameState, _guiGameState);
}