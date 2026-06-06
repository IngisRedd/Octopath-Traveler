using Octopath_Traveler_Model;

namespace Octopath_Traveler_View.ResultViews;

public class ResurrectionResultView : IResultView
{
    private View _view;
    private SkillResultInfo _result;

    public ResurrectionResultView(View view, SkillResultInfo result)
    {
        _view = view;
        _result = result;
    }
    
    public void Render()
    {
        string targetName = _result.Target.Name;
        _view.WriteLine($"{targetName} revive");
    }
}