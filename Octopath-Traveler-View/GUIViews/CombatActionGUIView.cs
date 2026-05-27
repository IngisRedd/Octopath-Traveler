using Octopath_Traveler_Model;
using Octopath_Traveler_View.ConsoleViews;
using Octopath_Traveler_View.ConsoleViews.CombatActionView;
using Octopath_Traveler_View.GUIViews.GUIStructures;
using Octopath_Traveler_View.ResultViews;
using Octopath_Traveler;
using OctopathTravelerGUI;

namespace Octopath_Traveler_View;

public class CombatActionGUIView : ICombatActionView
{
    private OTGUI _window;
    private GUIGameState _guiGameState;

    public CombatActionGUIView(OTGUI window, GUIGameState guiGameState)
    {
        _window = window;
        _guiGameState = guiGameState;
    }

    public void ShowCombatActionResults()
    {
        _guiGameState.Update();
       _window.Update(_guiGameState); 
    }
}
    