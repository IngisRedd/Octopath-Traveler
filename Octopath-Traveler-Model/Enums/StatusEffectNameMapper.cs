namespace Octopath_Traveler_Model;

public static class StatusEffectNameMapper
{
    private static readonly Dictionary<StatusType, string> DisplayNames = new Dictionary<StatusType, string>
    {
        { StatusType.Slow, "menor prioridad de turno" },

        { StatusType.IncreasedPhysAtk, "Increased Physical Attack" },
        { StatusType.IncreasedElemAtk, "Increased Elemental Attack" },
        { StatusType.IncreasedPhysDef, "Increased Physical Defense" },
        { StatusType.IncreasedElemDef, "Increased Elemental Defense" },
        { StatusType.IncreasedSpeed, "Increased Speed" },

        { StatusType.DecreasedPhysAtk, "Decreased Physical Attack" },
        { StatusType.DecreasedElemAtk, "Decreased Elemental Attack" },
        { StatusType.DecreasedPhysDef, "Decreased Physical Defense" },
        { StatusType.DecreasedElemDef, "Decreased Elemental Defense" },
        { StatusType.DecreasedSpeed, "Decreased Speed" },

        { StatusType.Poison, "Poison" },
        { StatusType.Silence, "Silence" },
        { StatusType.Unconscious, "Unconscious" },
        { StatusType.Sleep, "Sleep" },
        { StatusType.Terror, "Terror" }
    };

    public static string GetName(StatusType status)
    {
        if (DisplayNames.ContainsKey(status))
        {
            return DisplayNames[status];
        }

        throw new ArgumentOutOfRangeException(nameof(status), $"Display string mapping missing for status condition: '{status}'");
    }
}