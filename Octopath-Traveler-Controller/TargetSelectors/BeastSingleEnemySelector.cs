using Octopath_Traveler_Model;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public class BeastSingleEnemySelector : ITargetSelector
{
    private GameState _gameState;
    private Stat _stat;
    private SelectionType _selectionType;

    public BeastSingleEnemySelector(GameState gameState, Stat stat, SelectionType selectionType)
    {
        _gameState = gameState;
        _stat = stat;
        _selectionType = selectionType;
    }
    
    public void Select()
    {
        IEnumerable<Traveler> travelers = _gameState.TravelerTeam.AliveUnits;
        
        Func<Traveler, int> selector = _stat switch
        {
            Stat.HP => t => t.CurrentHP,
            Stat.PhysAtk => t => t.PhysAtk,
            Stat.PhysDef => t => t.PhysDef,
            Stat.ElemAtk => t => t.ElemAtk,
            Stat.ElemDef => t => t.ElemDef,
            Stat.Speed => t => t.Speed,
            _ => throw new ArgumentOutOfRangeException()
        };

        Traveler selectedTarget = _selectionType == SelectionType.Highest
            ? travelers.MaxBy(selector)
            : travelers.MinBy(selector);
        
        _gameState.CombatTargets.Add(selectedTarget);
    }
}