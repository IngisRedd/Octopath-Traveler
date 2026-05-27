using OctopathTravelerGUI.Models.Interfaces;

namespace Octopath_Traveler_View.GUIViews.GUIStructures;

public class GUIBeast : IBeast
{
    public string Name { get; set; }
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int Shields { get; set; }
    
    public GUIBeast(string name, int hp, int maxHp, int shields)
    {
        Name = name;
        HP = hp;
        MaxHP = maxHp;
        Shields = shields;
    }
}