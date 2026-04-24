using Octopath_Traveler_Model;

namespace Octopath_Traveler_View;

public abstract class BaseConsoleView
{
    protected View _view;
    protected GameState _gameState;

    public BaseConsoleView(View view, GameState gameState)
    {
        _view = view;
        _gameState = gameState;
    }
    
    protected void PrintHorizontalRule()
    {
        _view.WriteLine("----------------------------------------");
    }

}