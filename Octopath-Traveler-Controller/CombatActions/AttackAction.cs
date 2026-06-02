using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.Actions;

public class AttackAction : CombatAction
{
    private ICombatActionView _combatActionView;
    public AttackAction(GameState gameState, IRoundView view, ICombatActionView combatActionView)
        : base(gameState, view)
    {
        _combatActionView = combatActionView;
    }
    
    public override void Execute()
    {
        DamageType selectedWeapon = _view.SelectWeapon(_gameState.CurrentTraveler.Weapons);
        SkillInfo skillInfo = CreateBasicAttackSkillInfo(selectedWeapon);
        
        ITargetSelector skillTargetSelector = TargetSelectorFactory.Create(skillInfo, _gameState, _view);

        skillTargetSelector.Select();
        
        int bpToUse = _view.AskForBPToUseIfAvailable();
        if (bpToUse > 0)
        {
            _gameState.CurrentTraveler.UseBP(bpToUse);
        }
        
        SkillEffectChain skillEffectChain = SkillEffectFactory.Create(skillInfo, _gameState, bpToUse);
        skillEffectChain.ApplyEffects();
        _combatActionView.ShowCombatActionResults();
    }
    
    private SkillInfo CreateBasicAttackSkillInfo(DamageType selectedWeapon)
    {
        decimal basicAttackModifier = 1.3m;

        return new SkillInfo
        {
            Name = "Basic Attack",
            Type = selectedWeapon,
            Description = "",
            Modifier = basicAttackModifier,
            Target = SkillTarget.Single
        };
    }
 }