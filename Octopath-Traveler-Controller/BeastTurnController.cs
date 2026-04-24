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
        _gameState.CombatTargets.Clear();
        SkillInfo beastSkill = _gameState.CurrentBeast.Skill;
        Skill skillToUse = SkillFactory.Create(beastSkill, _gameState, _view);
        _view.ShowSkillUsage(beastSkill.Name);
        skillToUse.Use();
        _view.ShowFinalHPForAllTargets(); ///Malisismo, hay que quitar de alguna maneraa, tal vez pasando la info a la view para que la view haga magia
    }
}