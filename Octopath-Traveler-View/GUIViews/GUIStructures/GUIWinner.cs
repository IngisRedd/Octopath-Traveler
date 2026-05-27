using OctopathTravelerGUI.Models.Enums;
using OctopathTravelerGUI.Models.Interfaces;

namespace Octopath_Traveler_View.GUIViews.GUIStructures;

public class GUIWinner : IWinner
{
    public WinnerOption WinnerOption { get; set; }
    public IEnumerable<string> Team { get; set; }

    public GUIWinner(WinnerOption winnerOption, IEnumerable<string> team)
    {
        WinnerOption = winnerOption;
        Team = team;
    }
}