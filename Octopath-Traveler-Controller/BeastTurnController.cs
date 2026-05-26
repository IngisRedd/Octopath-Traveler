using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;
using Octopath_Traveler.TurnControllers;

namespace Octopath_Traveler;

public class BeastTurnController : ITurnController
{
    private GameState _gameState;
    private RoundConsoleView _view;
    CombatActionConsoleView _combatActionConsoleView;

    public BeastTurnController(GameState gameState, RoundConsoleView view, CombatActionConsoleView combatActionConsoleView)
    {
        _gameState = gameState;
        _view = view;
        _combatActionConsoleView = combatActionConsoleView;
    }

    public void Execute()
    {
        SkillInfo beastSkill = _gameState.CurrentBeast.Skill;
        
        Skill skillToUse = SkillFactory.Create(beastSkill, _gameState, _view);
        skillToUse.Use();
        _combatActionConsoleView.ShowCombatActionResults();
    }
}