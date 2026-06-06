using Octopath_Traveler_Model;

namespace Octopath_Traveler_View.ResultViews;

public class DamageResultView : IResultView
{
    private View _view;
    private SkillResultInfo _result;

    public DamageResultView(View view, SkillResultInfo result)
    {
        _view = view;
    }
    
    public void Render()
    {
        CombatUnit target = _result.Target;
        int damage = _result.Value;

        if (_result.IsTargetDefending)
        {
            _view.WriteLine($"{target.Name} se defiende");
        }

        if (target is Beast)
        {
            RenderBeastDamage((Beast)target);
        }
        else
        {
            RenderStandardDamage(target);
        }
    }

    private void RenderBeastDamage(Beast beast)
    {
        if (beast.IsWeakToDamageType(_result.DamageType))
        {
            _view.WriteLine($"{beast.Name} recibe {_result.Value} de daño de tipo {_result.DamageType} con debilidad");
            if (_result.HasEnteredBreakingPoint)
            {
                _view.WriteLine($"{beast.Name} entra en Breaking Point");
            }
        }
        else
        {
            RenderStandardDamage(beast);
        }
    }
    
    private void RenderStandardDamage(CombatUnit target)
    {
        if (_result.DamageType is DamageType.None) 
            _view.WriteLine($"{target.Name} recibe {_result.Value} de daño");
        else if (_result.DamageType is DamageType.Phys)
            _view.WriteLine($"{target.Name} recibe {_result.Value} de daño físico");
        else if (_result.DamageType is DamageType.Elem)
            _view.WriteLine($"{target.Name} recibe {_result.Value} de daño elemental");
        else
            _view.WriteLine($"{target.Name} recibe {_result.Value} de daño de tipo {_result.DamageType}");
    }
}