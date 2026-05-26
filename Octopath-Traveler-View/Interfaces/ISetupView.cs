using Octopath_Traveler;

namespace Octopath_Traveler_View;

public interface ISetupView
{
    public TeamsSetupInfo GetTeamsSetupInfo();
    public void ShowInvalidTeamMessage();
}