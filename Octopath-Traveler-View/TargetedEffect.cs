using Octopath_Traveler_Model;

namespace Octopath_Traveler_View;

public class TargetedEffect
{
    public SkillEffectResult Result { get; }
    public int Index { get; }

    public TargetedEffect(SkillEffectResult result, int index)
    {
        Result = result;
        Index = index;
    }
}