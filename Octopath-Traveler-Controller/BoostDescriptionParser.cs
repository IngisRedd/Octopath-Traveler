namespace Octopath_Traveler;

public static class BoostDescriptionParser
{
    
    public static decimal ParseBonusPercentage(string boostText)
    {
        if (string.IsNullOrEmpty(boostText))
        {
            return 0m;
        }

        string startMarker = "en un ";
        string endMarker = "%";

        int startIndex = boostText.IndexOf(startMarker) + startMarker.Length;
        int endIndex = boostText.IndexOf(endMarker);
        
        int length = endIndex - startIndex;
        string percentageString = boostText.Substring(startIndex, length);
        
        return Convert.ToDecimal(percentageString);
    }   
    
    public static int ParseConditionDurationBonus(string boostText)
    {
        if (string.IsNullOrEmpty(boostText))
        {
            return 0;
        }

        string startMarker = "Aumenta la duración en ";
        string endMarker = " rondas";

        int startIndex = boostText.IndexOf(startMarker);
        int endIndex = boostText.IndexOf(endMarker);

        // If the string doesn't match our expected pattern, fail safely by returning 0
        if (startIndex == -1 || endIndex == -1 || endIndex <= startIndex)
        {
            return 0;
        }

        startIndex = startIndex + startMarker.Length;
        int length = endIndex - startIndex;

        string numberString = boostText.Substring(startIndex, length);

        return Convert.ToInt32(numberString);
    }
}