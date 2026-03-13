using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler;

public class Game
{
    private MainConsoleView _view;
    private GameState _state = new();
    public Game(View view, string teamsFolder)
    {
        _view = new MainConsoleView(view, _state, teamsFolder);
    }

    public void Play()
    {
        try
        {
            TeamsInfo teamsInfo = _view.GetTeamsInfo();
            TeamsBuilder teamsBuilder = new TeamsBuilder(_state, teamsInfo);
            teamsBuilder.Build();
        }
        catch (Exception e)
        {
            _view.ShowInvalidTeamMessage();
        }
    }
}