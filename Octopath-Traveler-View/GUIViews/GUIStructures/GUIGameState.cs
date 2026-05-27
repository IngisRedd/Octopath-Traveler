using Octopath_Traveler_Model;
using OctopathTravelerGUI.Models.Enums;
using OctopathTravelerGUI.Models.Interfaces;

namespace Octopath_Traveler_View.GUIViews.GUIStructures;

public class GUIGameState : IState
{
    public IEnumerable<ITraveler> Travelers { get; set; }
    public IEnumerable<IBeast> Beasts { get; set; }
    public IEnumerable<string> Options { get; set; }
    public Option Option { get; set; }
    public IEnumerable<string> CurrentRoundTurns { get; set; }
    public IEnumerable<string> NextRoundTurns { get; set; }

    private GameState _gameState;

    public GUIGameState(GameState gameState)
    {
        _gameState = gameState;
    }

    public void Update()
    {
        GUIGameStateUpdater.Update(this, _gameState);
    }
}