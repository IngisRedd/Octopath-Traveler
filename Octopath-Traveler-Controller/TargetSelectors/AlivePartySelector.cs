using Octopath_Traveler_Model;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.TargetSelectors;

public class AlivePartySelector : ITargetSelector
{
    private GameState _gameState;
    
    public AlivePartySelector(GameState gameState)
    {
        _gameState = gameState;
    }
    
    public void Select()
    {
        IEnumerable<CombatUnit> units = GetAlivePartyMembers();
        _gameState.CombatTargets.AddRange(units);
    }

    private IEnumerable<CombatUnit> GetAlivePartyMembers()
    {
        if (_gameState.CurrentUnit is Traveler)
        {
            return _gameState.TravelerTeam.AliveUnits;
        }
        else
        {
            return _gameState.BeastTeam.AliveUnits;
        }
    }
}
