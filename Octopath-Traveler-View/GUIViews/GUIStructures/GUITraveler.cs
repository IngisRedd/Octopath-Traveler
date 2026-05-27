using OctopathTravelerGUI.Models.Interfaces;

namespace Octopath_Traveler_View.GUIViews.GUIStructures;

public class GUITraveler : ITraveler
{
    public string Name { get; set; }
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int SP { get; set; }
    public int MaxSP { get; set; }
    public int BoostPoints { get; set; }
    
    public GUITraveler(string name, int hp, int maxHp, int sp, int maxSp, int boostPoints)
    {
        Name = name;
        HP = hp;
        MaxHP = maxHp;
        SP = sp;
        MaxSP = maxSp;
        BoostPoints = boostPoints;
    }
}