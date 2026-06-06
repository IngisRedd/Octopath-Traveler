namespace Octopath_Traveler_Model;

public class GameState
{
    public Combatants TravelerTeam;
    public Combatants BeastTeam;
    public Combatants AllUnits = new();
    public CombatUnit CurrentUnit;
    public Traveler CurrentTraveler => (Traveler)CurrentUnit;
    public Beast CurrentBeast => (Beast)CurrentUnit;
    public int RoundCounter = 0;
    public TurnQueue CurrentTurnQueue = new();
    public bool IsTurnStillGoing => CurrentTurnQueue.Count > 0;
    public TurnQueue NextTurnQueue = new();
    
    public Combatants CombatTargets = new();
    public string SkillUsedName;
    public AppliedSkillEffectResults AppliedSkillEffectResults = new();
    public UsedSkillResults UsedSkillResults = new();
    public SkillEffectResult LastSkillEffectResult => AppliedSkillEffectResults.LastSkillEffectResult;

}