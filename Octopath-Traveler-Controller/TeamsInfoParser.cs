using System.Runtime.CompilerServices;
using Octopath_Traveler;

namespace Octopath_Traveler_Model;

public class TeamsInfoParser
{
    private TeamsInfo _teamsInfo = new();
    private List<string> _travelerDescriptions = new();

    public TeamsInfo ParseFileData(string pathToFile)
    {
        SplitTravelerAndBeastNames(pathToFile);
        SplitTravelerNamesAndSkills();
        return _teamsInfo;
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
                _teamsInfo.BeastNames.Add(line);
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
            int endOfNameIndex = GetEndOfNamePosition(travelerDescription);
            string travelerName = ParseTravelerName(travelerDescription);
            _teamsInfo.TravelerNames.Add(travelerName);

            int skillsStart = travelerDescription.IndexOf('(');
            int skillsEnd = travelerDescription.IndexOf(')');
            int passiveSkillsStart = travelerDescription.IndexOf('[');
            int passiveSkillsEnd = travelerDescription.IndexOf(']');
            
            _teamsInfo.TravelerSkills[travelerName] = new List<string>();
            _teamsInfo.TravelerPassiveSkills[travelerName] = new List<string>();
            if (TravelerHasSkills(skillsStart))
            {
                List<string> skillsList = SplitSkillsIntoList(travelerDescription, skillsStart, skillsEnd);            
                _teamsInfo.TravelerSkills[travelerName] = skillsList;
            }
            if (TravelerHasSkills(passiveSkillsStart))
            {
                List<string> passiveSkillsList = SplitSkillsIntoList(travelerDescription, passiveSkillsStart, passiveSkillsEnd);            
                _teamsInfo.TravelerPassiveSkills[travelerName] = passiveSkillsList;
            }
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
    
    private bool TravelerHasSkills(int skillStartPosition) => skillStartPosition != -1;
    
    private List<string> SplitSkillsIntoList(string travelerDescription, int skillsStart, int skillsEnd)
    {
        string skillsText = travelerDescription.Substring(skillsStart + 1, skillsEnd - skillsStart - 1);
        string[] splitSkills = skillsText.Split(',');
        IEnumerable<string> trimmedSkills = splitSkills.Select(skill => skill.Trim());
        
        return trimmedSkills.ToList();
    }


}