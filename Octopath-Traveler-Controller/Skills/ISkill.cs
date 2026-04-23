using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public interface ISkill
{
    public void Use(GameState gameState, MainConsoleView view){}
}