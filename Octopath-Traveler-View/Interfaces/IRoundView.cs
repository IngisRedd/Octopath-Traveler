using Octopath_Traveler_Model;

namespace Octopath_Traveler_View;

public interface IRoundView
{
    public void StartOfRoundUpdate();
    public void StartOfTurnUpdate();
    public CombatActionType SelectTravelerCombatAction();
    public DamageType SelectWeapon(List<DamageType> weapons);
    public Beast SelectEnemyBeastTarget();
    public Traveler SelectTravelerAllyTarget(Combatants allies);
    public int AskForBPToUseIfAvailable();
    public TravelerSkillInfo SelectFromAvailableSkills();
    public void ShowFleeMessage();
    public void ShowVictoryMessage();
    public void ShowLostGameMessage();

}