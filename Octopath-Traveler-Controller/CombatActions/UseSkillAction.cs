using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.Actions;

public class UseSkillAction : CombatAction
{
    ICombatActionView _combatActionView;
    TravelerSkillInfo _skillInfo;

    public UseSkillAction(GameState gameState, IRoundView view, ICombatActionView combatActionView, TravelerSkillInfo skillInfo)
        : base(gameState, view)
    {
        _combatActionView = combatActionView;
        _skillInfo = skillInfo;
    }
    
    public override void Execute()
    {
        ITargetSelector skillTargetSelector = TargetSelectorFactory.Create(_skillInfo, _gameState, _view);
        skillTargetSelector.Select();
        
        _gameState.CurrentTraveler.CurrentSP -= _skillInfo.SP;

        int bpToUse = _view.AskForBPToUseIfAvailable();
        _gameState.CurrentTraveler.UseBP(bpToUse);

        SkillEffectChain skillEffectChain = SkillEffectFactory.Create(_skillInfo, _gameState, bpToUse);
        skillEffectChain.ApplyEffects();
        _gameState.SkillUsedName = _skillInfo.Name;
        
        _combatActionView.ShowCombatActionResults();
    }
}