using Octopath_Traveler_Model;
using Octopath_Traveler;
using OctopathTravelerGUI;

namespace Octopath_Traveler_View;

public class SetupGUIView : ISetupView
{
    OTGUI _window;

    public SetupGUIView(OTGUI window)
    {
        _window = window;
    }
    
    public TeamsSetupInfo GetTeamsSetupInfo()
    {
        TeamsSetupInfo teamsSetupInfo = new TeamsSetupInfo();
        teamsSetupInfo.TravelerDescriptions = _window.GetTravelersTeam().ToList();
        teamsSetupInfo.BeastNames = _window.GetBeastsTeam().ToList();
        
        return teamsSetupInfo;
    }

    public void ShowInvalidTeamMessage()
    {
        _window.ShowInvalidTeams();
    }
}