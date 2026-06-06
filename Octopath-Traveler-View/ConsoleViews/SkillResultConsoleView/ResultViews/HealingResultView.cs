using Octopath_Traveler_Model;

namespace Octopath_Traveler_View.ResultViews;

public class HealingResultView : IResultView
{
    private View _view;
    private SkillResultInfo _result;

    public HealingResultView(View view, SkillResultInfo result)
    {
        _view = view;
        _result = result;
    }
    
    public void Render()
    {  
      string targetName = _result.Target.Name;
        int? healValue = _result.Value;
        _view.WriteLine($"{targetName} recupera {healValue} de vida");
    }
}