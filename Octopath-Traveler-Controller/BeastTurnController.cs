using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;
using Octopath_Traveler.TurnControllers;

namespace Octopath_Traveler;

public class BeastTurnController : ITurnController
{
    private GameState _gameState;
    private IRoundView _roundView;
    ICombatActionView _combatActionView;

    public BeastTurnController(GameState gameState, IRoundView roundView, ICombatActionView combatActionView)
    {
        _gameState = gameState;
        _roundView = roundView;
        _combatActionView = combatActionView;
    }

    public void Execute()
    {
        SkillInfo beastSkillInfo = _gameState.CurrentBeast.Skill;

        ITargetSelector skillTargetSelector = TargetSelectorFactory.Create(beastSkillInfo, _gameState, _roundView);
        SkillEffectChain skillEffect = SkillEffectFactory.Create(beastSkillInfo, _gameState);

        skillTargetSelector.Select();
        skillEffect.ApplyEffects();

        _combatActionView.ShowCombatActionResults();
    }
}