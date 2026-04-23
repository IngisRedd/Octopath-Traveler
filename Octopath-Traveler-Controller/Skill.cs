using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Skills;

public class Skill
{
    private ITargetSelector _targetSelector;
    private ISkillEffect _skillEffect;
    private CombatUnit _currentUnit;
    private MainConsoleView _view;

    public Skill(ITargetSelector targetSelector, ISkillEffect skillEffect, CombatUnit currentUnit, MainConsoleView view)
    {
        _targetSelector = targetSelector;
        _skillEffect = skillEffect;
        _currentUnit = currentUnit;
        _view = view;
    }

    public void Use()
    {
        _targetSelector.Select();
        if (_currentUnit is Traveler)
        {
            int BPToUse = _view.AskForBPToUseIfAvailable();
        }
        _skillEffect.Apply();
    }
}