using Octopath_Traveler_Model;
using Octopath_Traveler_View.ResultViews;

namespace Octopath_Traveler_View;

public static class SkillResultViewFactory
{
    public static ISkillResultView Create(View view, SkillResultInfo resultInfo)
    {
        if (resultInfo.Type == ResultType.Damage)
        {
            return new DamageResultView(view, resultInfo);
        }

        if (resultInfo.Type == ResultType.Heal)
        {
            return new HealingResultView(view, resultInfo);
        }

        if (resultInfo.Type == ResultType.Revive)
        {
            return new ResurrectionResultView(view, resultInfo);
        }

        if (resultInfo.Type == ResultType.ApplyStatusEffect)
        {
            return new ApplyStatusEffectResultView(view, resultInfo);
        }

        throw new ArgumentException($"Unknown result type: {resultInfo.Type}!.");
    }
}