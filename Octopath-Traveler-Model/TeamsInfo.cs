using System.Runtime.CompilerServices;

namespace Octopath_Traveler_Model;

public class TeamsInfo
{
    private List<string> _travelerDescriptions = new();
    public List<string> BeastNames = new();
    public List<string> TravelerNames = new();
    public Dictionary<string, List<string>> TravelerSkills = new();
    public Dictionary<string, List<string>> TravelerPassiveSkills = new();
    public TeamsInfo(string pathToFile)
    {
        SplitTravelerAndBeastNames(pathToFile);
        SplitTravelerNamesAndSkills();
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
                BeastNames.Add(line);
            }
            else
            {
                _travelerDescriptions.Add(line);
            }
        }
    }
    
    private void SplitTravelerNamesAndSkills()
    {
        foreach (string traveler in _travelerDescriptions)
        {
            int skillsStart = traveler.IndexOf('(');
            int passiveSkillsStart = traveler.IndexOf('[');

            int endOfNameIndex = GetEndOfNamePosition(traveler);
            string travelerName = traveler.Substring(0, endOfNameIndex);
            TravelerNames.Add(travelerName);
            
            if (TravelerHasSkills(skillsStart))
            {
                int skillsEnd = traveler.IndexOf(')', skillsStart);
                List<string> skillsList = SplitSkillsIntoList(traveler, skillsStart, skillsEnd);            
        
                TravelerSkills[travelerName] = skillsList;
            }
            else
            {
                TravelerSkills[travelerName] = new List<string>();
            }
            if (TravelerHasSkills(passiveSkillsStart))
            {
                int skillsEnd = traveler.IndexOf(']', passiveSkillsStart);
                List<string> skillsList = SplitSkillsIntoList(traveler, passiveSkillsStart, skillsEnd);            
        
                TravelerPassiveSkills[travelerName] = skillsList;
            }
            else
            {
                TravelerPassiveSkills[travelerName] = new List<string>();
            }
        }
        
    }

    private int GetEndOfNamePosition(string travelerDescription)
    {
        int skillsStart = travelerDescription.IndexOf('(');
        int passiveSkillsStart = travelerDescription.IndexOf('[');

        List<int> specialIndexes = new List<int> { skillsStart, passiveSkillsStart };
        IEnumerable<int> shiftedSpecialIndexes = specialIndexes.Select(x => x - 1);
        IEnumerable<int> existingSpecialIndexes = shiftedSpecialIndexes.Where(i => i > -2);
        IEnumerable<int> specialIndexesEmptyCaseHandled = existingSpecialIndexes.DefaultIfEmpty(travelerDescription.Length);
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