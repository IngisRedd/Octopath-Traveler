using Octopath_Traveler_Model;

namespace Octopath_Traveler_View.ResultViews;

public class HealingResultView : IResultView
{
    public bool HasContent(SkillEffectResult result, int index)
    {
        return result.HealValues[index] != null;
    }

    public void Render(View view, SkillEffectResult result, int index)
    {
        string targetName = result.Targets[index].Name;
        int? healValue = result.HealValues[index];
        view.WriteLine($"{targetName} recupera {healValue} de vida");
    }
}