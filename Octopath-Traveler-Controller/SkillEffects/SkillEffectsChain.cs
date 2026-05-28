namespace Octopath_Traveler.Skills;

public class SkillEffectsChain
{
    private List<ISkillEffect> _skillEffects = new List<ISkillEffect>();

    public SkillEffectsChain(List<ISkillEffect> skillEffects)
    {
        _skillEffects = skillEffects;
    }
    
    public void ApplyEffects()
    {
        foreach (ISkillEffect effect in _skillEffects)
        {
            effect.Apply();
        }
    }
}