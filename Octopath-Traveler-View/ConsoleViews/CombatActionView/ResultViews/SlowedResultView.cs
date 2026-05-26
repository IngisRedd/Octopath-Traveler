using Octopath_Traveler_Model;

namespace Octopath_Traveler_View.ResultViews;

public class SlowedResultView : IResultView
{
    public bool HasContent(SkillEffectResult result, int index)
    {
        return result.TurnsSlowedTarget[index] != null;
    }

    public void Render(View view, SkillEffectResult result, int index)
    {
        string targetName = result.Targets[index].Name;
        int? slowedTurns = result.TurnsSlowedTarget[index];
        view.WriteLine($"{targetName} tendrá menor prioridad de turno durante {slowedTurns} rondas");
    }
}