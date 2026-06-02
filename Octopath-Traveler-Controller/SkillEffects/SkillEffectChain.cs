namespace Octopath_Traveler.Skills;

public class SkillEffectChain
{
    private List<ISkillEffect> _skillEffects;

    public SkillEffectChain(List<ISkillEffect> skillEffects)
    {
        _skillEffects = skillEffects;
    }

    public void Add(ISkillEffect skillEffect)
    {
        _skillEffects.Add(skillEffect);
    }

    public void AddRange(List<ISkillEffect> skillEffects)
    {
        _skillEffects.AddRange(skillEffects);
    }

    
    public void ApplyEffects()
    {
        foreach (ISkillEffect effect in _skillEffects)
        {
            effect.Apply();
        }
    }
}