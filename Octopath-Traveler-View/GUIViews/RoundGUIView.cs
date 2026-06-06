using Octopath_Traveler_Model;
using Octopath_Traveler_View.GUIViews;
using Octopath_Traveler_View.GUIViews.GUIStructures;
using Octopath_Traveler.Exceptions;
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
        List<string> weaponsInString = weapons.ConvertAll(weaponType => weaponType.ToString());

        weaponsInString.Add("Cancelar");
        _guiGameState.Options = weaponsInString;
        _window.Update(_guiGameState);
        
        IClickedElement clickedElement;
        do {
            clickedElement = _window.GetClickedElement();
        } while (clickedElement.Type != ClickElementType.Button);
        ValidateSelectionCanceling(clickedElement.Text);
        
        return RoundGUIParser.ParseWeapon(clickedElement.Text);
    }
    
    private void ValidateSelectionCanceling(string selection)
    {
        if (selection == "Cancelar")
        {
            throw new SelectionCanceledException("Selection canceled");
        }
    }

    public Beast SelectEnemyBeastTarget()
    {
        Combatants availableTargets = _realGameState.BeastTeam.AliveUnits;
        return (Beast)SelectTarget(availableTargets);
    }

    public Traveler SelectTravelerAllyTarget(Combatants allies)
    {
        return (Traveler)SelectTarget(allies);
    }

    private CombatUnit SelectTarget(Combatants availableTargets)
    {
        _guiGameState.Option = Option.Target;
        string[] availableTargetNames = availableTargets.UnitNames;
        availableTargetNames = availableTargetNames.Append("Cancelar").ToArray();
        _guiGameState.Options = availableTargetNames;

        _window.Update(_guiGameState);
        
        IClickedElement clickedElement;
        do {
            clickedElement = _window.GetClickedElement();
        } while (!availableTargetNames.Contains(clickedElement.Text));
        ValidateSelectionCanceling(clickedElement.Text);
        
        return availableTargets.FirstOrDefault(t => t.Name == clickedElement.Text);
    }
    
    public int AskForBPToUseIfAvailable()
    {
        if (!_realGameState.CurrentTraveler.AreThereAnyBPLeft)
            return 0;

        _guiGameState.Option = Option.BoostPoints;
        int bpAvailableToUse = Math.Min(_realGameState.CurrentTraveler.BP, 3);
        IEnumerable<int> bpRange = Enumerable.Range(0, bpAvailableToUse + 1);
        List<string> bpRangeInString = bpRange.Select(i => i.ToString()).ToList();
        bpRangeInString.Add("Cancelar");
        _guiGameState.Options = bpRangeInString;
        _window.Update(_guiGameState);
        
        IClickedElement clickedElement;
        do {
            clickedElement = _window.GetClickedElement();
        } while (clickedElement.Type != ClickElementType.Button);
        ValidateSelectionCanceling(clickedElement.Text);

        return int.Parse(clickedElement.Text);
    }
    
    public TravelerSkillInfo SelectFromAvailableSkills()
    {
        _guiGameState.Option = Option.Skill;
        List<TravelerSkillInfo> availableSkills = _realGameState.CurrentTraveler.AvailableSkills;
        List<string> skillNames = availableSkills.ConvertAll(travelerSkill => travelerSkill.Name);
        skillNames.Add("Cancelar");
        _guiGameState.Options = skillNames;
        _window.Update(_guiGameState);
        
        IClickedElement clickedElement;
        do {
            clickedElement = _window.GetClickedElement();
        } while (clickedElement.Type != ClickElementType.Button);
        ValidateSelectionCanceling(clickedElement.Text);

        return availableSkills.FirstOrDefault(t => t.Name == clickedElement.Text);
    }
    
    public void ShowFleeMessage()
    {
        ShowEndOfGameMessage(WinnerOption.RunAway);
    }

    private void ShowEndOfGameMessage(WinnerOption winnerOption)
    {
        Combatants travelerTeam = _realGameState.TravelerTeam;
        string[] travelerTeamNames = travelerTeam.UnitNames;
        GUIWinner winner = new GUIWinner(winnerOption, travelerTeamNames);
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