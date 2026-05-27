using Octopath_Traveler_Model;

namespace Octopath_Traveler_View;

public interface IViewFactory
{
    public ISetupView CreateSetupView();
    public ICombatActionView CreateCombatActionView(GameState gameState);
    public IRoundView CreateRoundView(GameState gameState);
}