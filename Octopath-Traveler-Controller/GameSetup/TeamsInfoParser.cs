using System.Runtime.CompilerServices;
using Octopath_Traveler;

namespace Octopath_Traveler_Model;

public class TeamsInfoParser
{
    private ParsedTeamsInfo _parsedTeamsInfo = new();
    private List<string> _travelerDescriptions = new();

    public ParsedTeamsInfo ParseFileData(string pathToFile)
    {
        SplitTravelerAndBeastNames(pathToFile);
        SplitTravelerNamesAndSkills();
        return _parsedTeamsInfo;
    }

    private void SplitTravelerAndBeastNames(string pathToFile)
    {
        bool isUnitBeast = false;
        foreach (string line in File.ReadLines(pathToFile).Skip(1))
        {
            if (line == "Enemy Team")
            {
                isUnitBeast = true;
                continue;
            }

            if (isUnitBeast)
            {
                _parsedTeamsInfo.BeastNames.Add(line);
            }
            else
            {
                _travelerDescriptions.Add(line);
            }
        }
    }
    
    private void SplitTravelerNamesAndSkills()
    {
        foreach (string travelerDescription in _travelerDescriptions)
        {
            string travelerName = ParseTravelerName(travelerDescription);
            InitializeTravelerInTeamsInfo(travelerName);
            
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
    
    private void InitializeTravelerInTeamsInfo(string travelerName)
    {
        _parsedTeamsInfo.TravelerNames.Add(travelerName);
        _parsedTeamsInfo.TravelerSkills[travelerName] = new List<string>();
        _parsedTeamsInfo.TravelerPassiveSkills[travelerName] = new List<string>();
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