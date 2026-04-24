using Octopath_Traveler_Model;

namespace Octopath_Traveler.Skills;

public class AllEnemiesSelector : ITargetSelector
{
    private GameState _gameState;
    
    public AllEnemiesSelector(GameState gameState)
    {
        _gameState = gameState;
    }
    
    public void Select()
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
