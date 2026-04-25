using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public class BeastTurnController
{
    private GameState _gameState;
    private RoundConsoleView _view;

    public BeastTurnController(GameState gameState, RoundConsoleView view)
    {
        _gameState = gameState;
        _view = view;
    }

    public void Execute()
    {
        SkillInfo beastSkill = _gameState.CurrentBeast.Skill;
        
        Skill skillToUse = SkillFactory.Create(beastSkill, _gameState, _view);
        skillToUse.Use();
    }
}