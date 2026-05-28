using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.Actions;

public class UseSkillAction : CombatAction
{
    ICombatActionView _combatActionView;

    public UseSkillAction(GameState gameState, IRoundView view, ICombatActionView combatActionView)
        : base(gameState, view)
    {
        _combatActionView = combatActionView;
    }
    
    public override void Execute()
    {
        TravelerSkillInfo selectedSkillInfo = SelectSkill();

        ITargetSelector skillTargetSelector = TargetSelectorFactory.Create(selectedSkillInfo, _gameState, _view);
        SkillEffectsChain skillEffects = SkillEffectFactory.Create(selectedSkillInfo, _gameState, _view);

        skillTargetSelector.Select();
        
        _gameState.CurrentTraveler.CurrentSP -= selectedSkillInfo.SP;
        int bpToUse = _view.AskForBPToUseIfAvailable();

        skillEffects.ApplyEffects();

        _combatActionView.ShowCombatActionResults();
    }

    private TravelerSkillInfo SelectSkill()
    {
        return _view.SelectFromAvailableSkills();
    }
    
}