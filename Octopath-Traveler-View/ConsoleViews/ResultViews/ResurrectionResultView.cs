using Octopath_Traveler_Model;

namespace Octopath_Traveler_View.ResultViews;

public class ResurrectionResultView : IResultView
{
    public bool HasContent(SkillEffectResult result, int index)
    {
        return result.IsTravelerResurrected[index];
    }

    public void Render(View view, SkillEffectResult result, int index)
    {
        string targetName = result.Targets[index].Name;
        view.WriteLine($"{targetName} revive");
    }
}