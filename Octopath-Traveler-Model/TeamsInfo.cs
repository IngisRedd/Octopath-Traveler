using System.Runtime.CompilerServices;

namespace Octopath_Traveler_Model;

public class TeamsInfo
{
    private List<string> TravelerDescriptions = new();
    public List<string> BeastNames = new();
    public List<string> TravelerNames = new();
    public Dictionary<string, List<string>> TravelerSkills = new();
    public Dictionary<string, List<string>> TravelerPassiveSkills = new();
    public TeamsInfo(string pathToFile)
    {
        SplitTravelerAndBeastNames(pathToFile);
        SplitTravelerNamesAndSkills();
        Console.WriteLine("POOP");
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
                TravelerDescriptions.Add(line);
            }
        }
    }
    
    private void SplitTravelerNamesAndSkills()
    {
        foreach (string traveler in TravelerDescriptions)
        {
            int skillsStart = traveler.IndexOf('(');
            int passiveSkillsStart = traveler.IndexOf('[');

            int endOfNameIndex = GetEndOfNamePosition(skillsStart, passiveSkillsStart, traveler.Length);
            string travelerName = traveler.Substring(0, endOfNameIndex);
            TravelerNames.Add(travelerName);
            
            if (TravelerHasSkills(skillsStart))
            {
                int skillsEnd = traveler.IndexOf(')', skillsStart);
                List<string> skillsList = SplitSkillsIntoList(traveler, skillsStart, skillsEnd);            
        
                TravelerSkills[travelerName] = skillsList;
            }
            if (TravelerHasSkills(passiveSkillsStart))
            {
                int skillsEnd = traveler.IndexOf(']', passiveSkillsStart);
                List<string> skillsList = SplitSkillsIntoList(traveler, passiveSkillsStart, skillsEnd);            
        
                TravelerPassiveSkills[travelerName] = skillsList;
            }

            
        }
        
    }

    private int GetEndOfNamePosition(int skillsStart, int passiveSkillsStart, int stringLength)
    {
        List<int> specialIndexes = new List<int> { skillsStart, passiveSkillsStart };
        IEnumerable<int> existingSpecialIndexes = specialIndexes.Where(i => i != -1);
        IEnumerable<int> specialIndexesEmptyCaseHandled = existingSpecialIndexes.DefaultIfEmpty(stringLength);
        return specialIndexesEmptyCaseHandled.Min();
    }
    
    private bool TravelerHasSkills(int skillStartPosition) => skillStartPosition != -1;
    
    private List<string> SplitSkillsIntoList(string traveler, int skillsStart, int skillsEnd)
    {
        string skillsText = traveler.Substring(skillsStart + 1, skillsEnd - skillsStart - 1);
        string[] splitSkills = skillsText.Split(',');
        return splitSkills.ToList();
    }


}