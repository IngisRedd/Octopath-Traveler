namespace Octopath_Traveler;

public static class SkillDescriptionParser
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
    
    public static int ParseValueBeforeMarker(string boostText, string afterValueMarker)
    {
        if (string.IsNullOrEmpty(boostText))
        {
            return 0;
        }
        int markerIndex = boostText.IndexOf(afterValueMarker);

        int currentIndex = markerIndex - 1;
        
        while (IsCharacterANumber(currentIndex, boostText))
        {
            currentIndex = currentIndex - 1;
        }

        int numberStartIndex = currentIndex + 1;
        int numberLength = markerIndex - numberStartIndex;

        if (numberLength <= 0)
        {
            return 0;
        }

        string numberString = boostText.Substring(numberStartIndex, numberLength);
        
        return Convert.ToInt32(numberString);
    }

    private static bool IsCharacterANumber(int currentIndex, string boostText)
        => currentIndex >= 0 && char.IsDigit(boostText[currentIndex]);
}