using Octopath_Traveler_Model;
using Octopath_Traveler.TargetSelectors;

namespace Octopath_Traveler.Skills;

public class AllEnemiesSelector : BaseSelector
{
    public AllEnemiesSelector(GameState gameState)
        : base(gameState){}
    
    protected override void OnSelect()
    {
        IEnumerable<CombatUnit> units = GetAliveUnits();
        _gameState.CombatTargets.AddRange(units);
    }

    private IEnumerable<CombatUnit> GetAliveUnits()
    {
        if (_gameState.CurrentUnit is Traveler)
        {
            return _gameState.BeastTeam.AliveUnits;
        }
        else
        {
            return _gameState.TravelerTeam.AliveUnits;
        }
    }
}
