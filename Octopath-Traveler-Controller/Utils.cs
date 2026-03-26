using Octopath_Traveler_View;

namespace Octopath_Traveler;

public static class Utils
{
    public static int ReadPlayerInput(MainConsoleView view)
    {
        string input = view.AskForPlayerInput();
        return Convert.ToInt32(input);
    }
}