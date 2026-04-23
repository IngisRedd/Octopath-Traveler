using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler;

public class BeastTurnController
{
    private GameState _gameState;
    private MainConsoleView _view;

    public BeastTurnController(GameState gameState, MainConsoleView view)
    {
        _gameState = gameState;
        _view = view;
    }

    public void Execute()
    {
        BeastSkillInfo beastSkill = _gameState.CurrentBeast.Skill;
        BeastSkillFactory beastSkillFactory = new BeastSkillFactory();
        ISkill skillToUse = beastSkillFactory.Create(beastSkill);
        skillToUse.Use(_gameState, _view);
    }
}