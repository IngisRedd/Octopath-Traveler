using Octopath_Traveler_Model;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.TargetSelectors;

public class AlivePartySelector : BaseSelector
{
    public AlivePartySelector(GameState gameState)
        : base(gameState){}
    
    protected override void OnSelect()
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
