using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Skills;

namespace Octopath_Traveler.Actions;

public class UseSkillAction : CombatAction
{
    public UseSkillAction(GameState gameState, MainConsoleView view)
        : base(gameState, view){}
    
    public override void Execute()
    {
        TravelerSkillInfo selectedSkillInfo = SelectSkill();
        TravelerSkillFactory travelerSkillFactory = new TravelerSkillFactory();
        ISkill skillToUse = travelerSkillFactory.Create(selectedSkillInfo);
        skillToUse.Use(_gameState, _view);
    }

    private TravelerSkillInfo SelectSkill()
    {
        _view.ShowAvailableSkills();

        int selectedIndex = Utils.ReadPlayerInput(_view) - 1;
        return _gameState.CurrentTraveler.AvailableSkills[selectedIndex];
    }
    
}