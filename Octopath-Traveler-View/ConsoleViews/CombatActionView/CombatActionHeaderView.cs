using Octopath_Traveler_Model;

namespace Octopath_Traveler_View.ConsoleViews.CombatActionView;

public class CombatActionHeaderView
{
    private readonly View _view;
    private readonly GameState _gameState;

    public CombatActionHeaderView(View view, GameState gameState)
    {
        _view = view;
        _gameState = gameState;
    }

    public void ShowHeader()
    {
        if (WasSkillUsed())
        {
            ShowSkillUsage();
        }
        else
        {
            ShowBasicAttack();
        } 
    }

    private bool WasSkillUsed()
        => _gameState.SkillUsedName != "Basic Attack";
    
    private void ShowSkillUsage()
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine($"{_gameState.CurrentUnit.Name} usa {_gameState.SkillUsedName}");
    }
    
    private void ShowBasicAttack()
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine($"{_gameState.CurrentUnit.Name} ataca");
    }
}