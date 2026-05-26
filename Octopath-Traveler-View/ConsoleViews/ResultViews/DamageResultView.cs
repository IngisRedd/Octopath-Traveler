using Octopath_Traveler_Model;

namespace Octopath_Traveler_View.ResultViews;

public class DamageResultView : IResultView
{
    public bool HasContent(SkillEffectResult result, int index)
    {
        return result.Damages[index] != null;
    }

    public void Render(View view, SkillEffectResult result, int index)
    {
        CombatUnit target = result.Targets[index];
        Damage damage = result.Damages[index];

        if (result.IsTravelerDefending[index])
        {
            view.WriteLine($"{target.Name} se defiende");
        }

        if (target is Beast)
        {
            RenderBeastDamage(view, (Beast)target, damage, result.IsBreakingPointAchieved[index]);
        }
        else
        {
            RenderStandardDamage(view, target, damage);
        }
    }

    private void RenderBeastDamage(View view, Beast beast, Damage damage, bool isBreakingPoint)
    {
        if (beast.IsWeakToDamageType(damage.Type))
        {
            view.WriteLine($"{beast.Name} recibe {damage.Value} de daño de tipo {damage.Type} con debilidad");
            if (isBreakingPoint)
            {
                view.WriteLine($"{beast.Name} entra en Breaking Point");
            }
        }
        else
        {
            RenderStandardDamage(view, beast, damage);
        }
    }

    private void RenderStandardDamage(View view, CombatUnit target, Damage damage)
    {
        if (damage.Type is DamageType.None) 
            view.WriteLine($"{target.Name} recibe {damage.Value} de daño");
        else if (damage.Type is DamageType.Phys)
            view.WriteLine($"{target.Name} recibe {damage.Value} de daño físico");
        else if (damage.Type is DamageType.Elem)
            view.WriteLine($"{target.Name} recibe {damage.Value} de daño elemental");
        else
            view.WriteLine($"{target.Name} recibe {damage.Value} de daño de tipo {damage.Type}");
    }
}