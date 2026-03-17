using System.Runtime.CompilerServices;

namespace Octopath_Traveler_Model;

public class TeamsInfo
{
    private List<string> TravelerDescriptions = new();
    public List<string> BeastNames = new();
    public Dictionary<string, List<string>> TravelerAndSkills = new();
    public TeamsInfo(string pathToFile)
    {
        SplitTravelerAndBeastNames(pathToFile);
        SplitTravelerNamesAndSkills();
        Console.WriteLine("Gud");
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
                
            string travelerName;
            List<string> abilities = new();

            int skillsStart = traveler.IndexOf('(');

            if (TravelerHasSkills(skillsStart))
            {
                AddTravelerWithSkills(traveler, skillsStart);
            }
            else
            {
                travelerName = traveler.Trim();
                TravelerAndSkills.Add(travelerName, new List<string>());
            }
        }
        
    }
    
    private bool TravelerHasSkills(int skillStartPosition) => skillStartPosition != -1;

    private void AddTravelerWithSkills(string traveler, int skillsStart)
    {
        string travelerName = traveler.Substring(0, skillsStart).Trim();
        List<string> skillsList = SplitSkillsIntoList(traveler, skillsStart);            
        
        TravelerAndSkills.Add(travelerName, skillsList);

    }

    private List<string> SplitSkillsIntoList(string traveler, int skillsStart)
    {
        int skillsEnd = traveler.IndexOf(')', skillsStart);
        string skillsText = traveler.Substring(skillsStart + 1, skillsEnd - skillsStart - 1);
        string[] splitSkills = skillsText.Split(',');
        return splitSkills.ToList();
        
    }


}