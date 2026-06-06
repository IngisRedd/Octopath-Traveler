using Octopath_Traveler_Model;
using Octopath_Traveler.Skills;
using Octopath_Traveler.TargetSelectors;

namespace Octopath_Traveler;

public class ByStatSingleEnemySelector : BaseSelector
{
    private Stat _stat;
    private SelectionType _selectionType;

    public ByStatSingleEnemySelector(GameState gameState, Stat stat, SelectionType selectionType)
        : base(gameState)
    {
        _stat = stat;
        _selectionType = selectionType;
    }
    
    protected override void OnSelect()
    {
        Combatants avaliableTargets = GetTargets();
        
        Func<CombatUnit, int> selector = _stat switch
        {
            Stat.HP => t => t.CurrentHP,
            Stat.PhysAtk => t => t.PhysAtk,
            Stat.PhysDef => t => t.PhysDef,
            Stat.ElemAtk => t => t.ElemAtk,
            Stat.ElemDef => t => t.ElemDef,
            Stat.Speed => t => t.Speed,
            _ => throw new ArgumentOutOfRangeException()
        };

        CombatUnit selectedTarget = _selectionType == SelectionType.Highest
            ? avaliableTargets.MaxBy(selector)
            : avaliableTargets.MinBy(selector);
        
        _gameState.CombatTargets.Add(selectedTarget);
    }

    private Combatants GetTargets()
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