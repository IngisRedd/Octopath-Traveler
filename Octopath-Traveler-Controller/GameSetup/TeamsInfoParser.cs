using System.Runtime.CompilerServices;
using Octopath_Traveler;

namespace Octopath_Traveler_Model;

public class TeamsInfoParser
{
    private ParsedTeamsInfo _parsedTeamsInfo = new();
    private List<string> _travelerDescriptions = new();

    public TeamsInfoParser(TeamsSetupInfo teamsSetupInfo)
    {
        _parsedTeamsInfo.BeastNames = teamsSetupInfo.BeastNames;
        _travelerDescriptions = teamsSetupInfo.TravelerDescriptions;
    }
    
    public ParsedTeamsInfo Parse()
    {
        SplitTravelerNamesAndSkills();
        return _parsedTeamsInfo;
    }
    
    private void SplitTravelerNamesAndSkills()
    {
        foreach (string travelerDescription in _travelerDescriptions)
        {
            string travelerName = ParseTravelerName(travelerDescription);
            _parsedTeamsInfo.AddTraveler(travelerName);

            ParseSkillsIfTravelerHasThem(travelerName, travelerDescription);
            ParsePassiveSkillsIfTravelerHasThem(travelerName, travelerDescription);
        }
    }

    private string ParseTravelerName(string travelerDescription)
    {
        int endOfNameIndex = GetEndOfNamePosition(travelerDescription);
        string travelerName = travelerDescription.Substring(0, endOfNameIndex);
        return travelerName;
    }
    
    private int GetEndOfNamePosition(string travelerDescription)
    {
        int skillsStart = travelerDescription.IndexOf('(');
        int passiveSkillsStart = travelerDescription.IndexOf('[');

        List<int> skillIndexes = new List<int> { skillsStart, passiveSkillsStart };
        IEnumerable<int> shiftedSkillIndexes = skillIndexes.Select(x => x - 1);
        IEnumerable<int> existingSkillIndexes = shiftedSkillIndexes.Where(i => i > -2);
        IEnumerable<int> specialIndexesEmptyCaseHandled = existingSkillIndexes.DefaultIfEmpty(travelerDescription.Length);
        return specialIndexesEmptyCaseHandled.Min();
    }
    
    private void ParseSkillsIfTravelerHasThem(string travelerName, string travelerDescription)
    {
        int skillsStart = travelerDescription.IndexOf('(');
        int skillsEnd = travelerDescription.IndexOf(')');
        if (TravelerHasSkills(skillsStart))
        {
            List<string> skillsList = SplitSkillsIntoList(travelerDescription, skillsStart, skillsEnd);            
            _parsedTeamsInfo.TravelerSkills[travelerName] = skillsList;
        }
    }

    private void ParsePassiveSkillsIfTravelerHasThem(string travelerName, string travelerDescription)
    {
        int passiveSkillsStart = travelerDescription.IndexOf('[');
        int passiveSkillsEnd = travelerDescription.IndexOf(']');
        if (TravelerHasSkills(passiveSkillsStart))
        {
            List<string> passiveSkillsList = SplitSkillsIntoList(travelerDescription, passiveSkillsStart, passiveSkillsEnd);            
            _parsedTeamsInfo.TravelerPassiveSkills[travelerName] = passiveSkillsList;
        }
    }
    
    private bool TravelerHasSkills(int skillStartPosition) => skillStartPosition != -1;
    
    private List<string> SplitSkillsIntoList(string travelerDescription, int skillsStart, int skillsEnd)
    {
        string skillsText = travelerDescription.Substring(skillsStart + 1, skillsEnd - skillsStart - 1);
        string[] splitSkills = skillsText.Split(',');
        IEnumerable<string> trimmedSkills = splitSkills.Select(skill => skill.Trim());
        
        return trimmedSkills.ToList();
    }
    
}