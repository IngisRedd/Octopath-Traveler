namespace Octopath_Traveler_Model;

public class AppliedSkillEffectResults
{
    private List<SkillEffectResult> _skillEffectResults = new();
    
    public void Add(SkillEffectResult skillEffectResult)
    {
        _skillEffectResults.Add(skillEffectResult);
    }

    public IEnumerator<SkillEffectResult> GetEnumerator()
    {
        return _skillEffectResults.GetEnumerator();
    }
    
    public SkillEffectResult LastSkillEffectResult => _skillEffectResults.LastOrDefault();

    public IEnumerable<TResult> Select<TResult>(Func<SkillEffectResult, TResult> selector)
    {
        foreach (var item in _skillEffectResults)
        {
            yield return selector(item);
        }
    }
}