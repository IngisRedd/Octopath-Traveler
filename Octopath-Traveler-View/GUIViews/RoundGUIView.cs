using Octopath_Traveler_Model;
using Octopath_Traveler_View.GUIViews;
using Octopath_Traveler_View.GUIViews.GUIStructures;
using OctopathTravelerGUI;
using OctopathTravelerGUI.Models.Enums;
using OctopathTravelerGUI.Models.Interfaces;

namespace Octopath_Traveler_View;

public class RoundGUIView : IRoundView
{
    private OTGUI _window;
    private GameState _realGameState;
    private GUIGameState _guiGameState;

    public RoundGUIView(OTGUI window, GameState gameState, GUIGameState guiGameState)
    {
        _window = window;
        _realGameState = gameState;
        _guiGameState = guiGameState;
    }
            
    public void StartOfRoundUpdate()
    {
        _guiGameState.Update();
        _window.Update(_guiGameState);
    }
    
    public void StartOfTurnUpdate()
    {
        _guiGameState.Update();
        _window.Update(_guiGameState);
    }
    
    public CombatActionType SelectTravelerCombatAction()
    {
        _guiGameState.Option = Option.Action;
        _guiGameState.Options = ["Ataque básico", "Usar Habilidad", "Defender", "Huir"];
        _window.Update(_guiGameState);
        
        IClickedElement clickedElement;
        do {
            clickedElement = _window.GetClickedElement();
        } while (clickedElement.Type != ClickElementType.Button);
        
        return RoundGUIParser.ParseCombatAction(clickedElement.Text);
    }
    
    public DamageType SelectWeapon(List<DamageType> weapons)
    {
        _guiGameState.Option = Option.Weapon;
        _guiGameState.Options = weapons.ConvertAll(weaponType => weaponType.ToString());
        _window.Update(_guiGameState);
        
        IClickedElement clickedElement;
        do {
            clickedElement = _window.GetClickedElement();
        } while (clickedElement.Type != ClickElementType.Button);
        
        return RoundGUIParser.ParseWeapon(clickedElement.Text);
    }
    
    public Beast SelectEnemyBeastTarget()
    {
        List<Beast> availableTargets = _realGameState.BeastTeam.AliveUnits;
        List<CombatUnit> combatUnits = availableTargets.ConvertAll(beast => (CombatUnit)beast);
        return (Beast)SelectTarget(combatUnits);
    }

    public Traveler SelectTravelerAllyTarget(List<Traveler> allies)
    {
        List<CombatUnit> combatUnits = allies.ConvertAll(traveler => (CombatUnit)traveler);
        return (Traveler)SelectTarget(combatUnits);
    }

    private CombatUnit SelectTarget(List<CombatUnit> availableTargets)
    {
        _guiGameState.Option = Option.Target;
        List<string> availableTargetNames = availableTargets.ConvertAll(target => target.Name);
        _guiGameState.Options = availableTargetNames;
        _window.Update(_guiGameState);
        
        IClickedElement clickedElement;
        do {
            clickedElement = _window.GetClickedElement();
        } while (!availableTargetNames.Contains(clickedElement.Text));
        
        return availableTargets.FirstOrDefault(t => t.Name == clickedElement.Text);
    }
    
    public int AskForBPToUseIfAvailable()
    {
        if (!_realGameState.CurrentTraveler.AreThereAnyBPLeft)
            return 0;

        _guiGameState.Option = Option.BoostPoints;
        IEnumerable<int> bpRange = Enumerable.Range(0, _realGameState.CurrentTraveler.BP + 1);
        IEnumerable<string> bpRangeInString = bpRange.Select(i => i.ToString());
        _guiGameState.Options = bpRangeInString;
        _window.Update(_guiGameState);
        
        IClickedElement clickedElement;
        do {
            clickedElement = _window.GetClickedElement();
        } while (clickedElement.Type != ClickElementType.Button);
        
        return int.Parse(clickedElement.Text);
    }
    
    public TravelerSkillInfo SelectFromAvailableSkills()
    {
        _guiGameState.Option = Option.Skill;
        List<TravelerSkillInfo> availableSkills = _realGameState.CurrentTraveler.AvailableSkills;
        List<string> skillNames = availableSkills.ConvertAll(travelerSkill => travelerSkill.Name);
        _guiGameState.Options = skillNames;
        _window.Update(_guiGameState);
        
        IClickedElement clickedElement;
        do {
            clickedElement = _window.GetClickedElement();
        } while (clickedElement.Type != ClickElementType.Button);
        
        return availableSkills.FirstOrDefault(t => t.Name == clickedElement.Text);
    }
    
    public void ShowFleeMessage()
    {
        ShowEndOfGameMessage(WinnerOption.RunAway);
    }

    private void ShowEndOfGameMessage(WinnerOption winnerOption)
    {
        List<Traveler> travelerTeam = _realGameState.TravelerTeam.Units;
        List<string> travelerTeamInString = travelerTeam.ConvertAll(traveler => traveler.Name);
        GUIWinner winner = new GUIWinner(winnerOption, travelerTeamInString);
        _window.ShowWinner(winner);
    }
    
    public void ShowVictoryMessage()
    {
        ShowEndOfGameMessage(WinnerOption.Travelers);
    }

    public void ShowLostGameMessage()
    {
        ShowEndOfGameMessage(WinnerOption.Beasts);
    }
}