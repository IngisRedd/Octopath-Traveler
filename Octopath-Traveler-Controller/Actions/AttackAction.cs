using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.Actions;

public class AttackAction : CombatAction
{
    public AttackAction(GameState gameState, RoundConsoleView view)
        : base(gameState, view){}
    
    public override void Execute()
    {
        DamageType selectedWeapon = _view.SelectWeapon(_gameState.CurrentTraveler.Weapons);
        SkillInfo skillInfo = CreateBasicAttackSkillInfo(selectedWeapon);
        Skill basicAttack = SkillFactory.Create(skillInfo, _gameState, _view);
        
        basicAttack.SelectTarget();
        int BPToUse = _view.AskForBPToUseIfAvailable();

        basicAttack.ApplyEffects();
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