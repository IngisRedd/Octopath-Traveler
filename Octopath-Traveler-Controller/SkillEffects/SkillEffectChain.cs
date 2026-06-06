using Octopath_Traveler_Model;

namespace Octopath_Traveler.Skills;

public class SkillEffectChain
{
    private GameState _gameState;
    private List<ISkillEffect> _skillEffects;

    public SkillEffectChain(GameState _gameState, List<ISkillEffect> skillEffects)
    {
        _gameState = _gameState;
        _skillEffects = skillEffects;
    }
    
    public void ApplyEffects()
    {
        foreach (CombatUnit target in _gameState.CombatTargets)
        {
            foreach (ISkillEffect effect in _skillEffects)
            {
                effect.ApplyTo(target);
            }
        }
    }
}