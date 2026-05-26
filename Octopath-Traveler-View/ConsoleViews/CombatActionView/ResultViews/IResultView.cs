using Octopath_Traveler_Model;

namespace Octopath_Traveler_View.ResultViews;

public interface IResultView
{
    bool HasContent(SkillEffectResult result, int index);
    void Render(View view, SkillEffectResult result, int index);
}