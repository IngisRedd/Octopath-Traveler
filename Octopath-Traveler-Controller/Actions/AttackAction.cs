using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.Actions;

public class AttackAction : CombatAction
{
    public AttackAction(GameState gameState, MainConsoleView view)
        : base(gameState, view){}
    
    public override void Execute()
    {
        DamageType selectedWeapon = Utils.ParseDamageType(SelectWeapon());
        SkillInfo skillInfo = CreateSkillInfo(selectedWeapon);
        Skill basicAttack = SkillFactory.Create(skillInfo, _gameState, _view);
        
        basicAttack.SelectTarget();
        int BPToUse = _view.AskForBPToUseIfAvailable();

        basicAttack.ApplyEffects();
        _view.ShowCombatActionResults();
    }

    private string SelectWeapon()
    {
        _view.ShowAvailableWeapons();
        int selectedIndex = _view.ReadPlayerInput() - 1;
        return _gameState.CurrentTraveler.Weapons[selectedIndex];
    }

    private SkillInfo CreateSkillInfo(DamageType selectedWeapon)
    {
        double basicAttackModifier = 1.3;
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