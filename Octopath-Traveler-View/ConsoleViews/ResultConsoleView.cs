using Octopath_Traveler_Model;
using Octopath_Traveler_View.Interfaces;

namespace Octopath_Traveler_View.ConsoleViews;

public class ResultConsoleView : BaseConsoleView, IResultView
{
    public ResultConsoleView(View view, GameState gameState)
        : base(view, gameState)
    {
        _view = view;
        _gameState = gameState;
    }
}