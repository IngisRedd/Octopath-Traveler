namespace Octopath_Traveler;

public static class TeamsValidator
{
    private const int MaxTravelers = 4;
    private const int MaxSkills = 8;
    private const int MaxPassiveSkills = 4;
    private const int MaxBeasts = 5;
    
    public static void Validate(ParsedTeamsInfo parsedTeamsInfo)
    {
        ValidateList(parsedTeamsInfo.TravelerNames, MaxTravelers);
        foreach (string traveler in parsedTeamsInfo.TravelerNames)
        {
            ValidateList(parsedTeamsInfo.TravelerSkills[traveler], MaxSkills);
            ValidateList(parsedTeamsInfo.TravelerPassiveSkills[traveler], MaxPassiveSkills);
        }
        ValidateList(parsedTeamsInfo.BeastNames, MaxBeasts);
    }

    private static void ValidateList(List<string> list, int maxSize)
    {
        CheckForUniqueness(list);
        CheckForMaxCapacity(list, maxSize);
    }
    
    private static void CheckForUniqueness(List<string> listToCheck)
    {
        if (listToCheck.Count != listToCheck.Distinct().Count())
            throw new InvalidOperationException("There are repeated items in list.");
    }

    private static void CheckForMaxCapacity(List<string> listToCheck, int maxSize)
    {
        if (listToCheck.Count > maxSize)
            throw new InvalidOperationException("Maximum capacity reached.");
    }
}