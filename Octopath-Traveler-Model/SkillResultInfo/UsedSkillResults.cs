namespace Octopath_Traveler_Model;

public class UsedSkillResults
{
    private List<SkillResultInfo> _skillEffectResults = new();
    
    public void Add(SkillResultInfo skillEffectResult)
    {
        _skillEffectResults.Add(skillEffectResult);
    }

    public IEnumerator<SkillResultInfo> GetEnumerator()
    {
        return _skillEffectResults.GetEnumerator();
    }
}