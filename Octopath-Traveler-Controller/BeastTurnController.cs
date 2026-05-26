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
        SkillInfo beastSkill = _gameState.CurrentBeast.Skill;
        
        Skill skillToUse = SkillFactory.Create(beastSkill, _gameState, _roundView);
        skillToUse.Use();
        _combatActionView.ShowCombatActionResults();
    }
}