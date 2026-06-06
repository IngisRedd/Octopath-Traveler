using Octopath_Traveler_Model;

namespace Octopath_Traveler_View.ResultViews;

public class ApplyStatusEffectResultView : ISkillResultView
{
    private View _view;
    private SkillResultInfo _result;

    public ApplyStatusEffectResultView(View view, SkillResultInfo result)
    {
        _view = view;
        _result = result;
    }
    
    public void Render()
    {
        string targetName = _result.Target.Name;
        int statusEffectDuration = _result.Value;
        string statusEffectText = StatusEffectNameMapper.GetName(_result.StatusEffectType);
        _view.WriteLine($"{targetName} tendrá {statusEffectText} durante {statusEffectDuration} rondas");
    }
}