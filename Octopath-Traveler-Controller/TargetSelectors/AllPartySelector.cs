using Octopath_Traveler_Model;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.TargetSelectors;

public class AllPartySelector : ITargetSelector
{
    private GameState _gameState;
    
    public AllPartySelector(GameState gameState)
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
            return _gameState.TravelerTeam.Units;
        }
        else
        {
            return _gameState.BeastTeam.Units;
        }
    }
}
