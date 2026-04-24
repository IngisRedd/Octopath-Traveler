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
        _gameState.CombatActionInfo.SkillName = selectedSkillInfo.Name;

        Skill skillToUse = SkillFactory.Create(selectedSkillInfo, _gameState, _view);

        skillToUse.SelectTarget();

        _gameState.CurrentTraveler.CurrentSP -= selectedSkillInfo.SP;
        int BPToUse = _view.AskForBPToUseIfAvailable();
        
        skillToUse.ApplyEffects();
        _view.ShowCombatActionResults();
    }

    private TravelerSkillInfo SelectSkill()
    {
        _view.ShowAvailableSkills();

        int selectedIndex = _view.ReadPlayerInput() - 1;
        return _gameState.CurrentTraveler.AvailableSkills[selectedIndex];
    }
    
}