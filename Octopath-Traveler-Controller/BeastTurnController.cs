using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public class BeastTurnController
{
    private GameState _gameState;
    private GameConsoleView _view;

    public BeastTurnController(GameState gameState, GameConsoleView view)
    {
        _gameState = gameState;
        _view = view;
    }

    public void Execute()
    {
        SkillInfo beastSkill = _gameState.CurrentBeast.Skill;
        _gameState.CombatActionInfo.SkillName = beastSkill.Name;
        
        Skill skillToUse = SkillFactory.Create(beastSkill, _gameState, _view);
        skillToUse.Use();
    }
}