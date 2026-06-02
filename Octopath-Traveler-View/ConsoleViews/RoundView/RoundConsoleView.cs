using Octopath_Traveler_Model;
using Octopath_Traveler.Exceptions;

namespace Octopath_Traveler_View;

public class RoundConsoleView : BaseConsoleView, IRoundView
{
    private RoundConsolePrinter _printer;

    public RoundConsoleView(View view, GameState gameState)
        : base(view, gameState)
    {
        _printer = new RoundConsolePrinter(view, gameState);
    }
            
    public void StartOfRoundUpdate()
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine($"INICIA RONDA {_gameState.RoundCounter}");
    }
    
    public void StartOfTurnUpdate()
    {
        _printer.ShowAllUnitInformation();
        _printer.ShowTurnQueues();
    }
    
    public CombatActionType SelectTravelerCombatAction()
    {
        _printer.ShowTravelerActions();
        int playerInput = ReadPlayerIntInput();
        return (CombatActionType)playerInput;
    }
    
    private int ReadPlayerIntInput()
    {
        string input = _view.ReadLine();
        return Convert.ToInt32(input);
    }
    
    public DamageType SelectWeapon(List<DamageType> weapons)
    {
        IEnumerable<string> deParsedWeapons = weapons.Select(weapon => weapon.ToString());
        _printer.ShowWeapons(deParsedWeapons);
        int selectedIndex = ReadPlayerIntInput() - 1;

        ValidateSelectionCanceling(selectedIndex, weapons.Count);
        return weapons[selectedIndex];
    }
    
    private void ValidateSelectionCanceling(int selectedIndex, int optionsCount)
    {
        if (selectedIndex == optionsCount)
        {
            throw new SelectionCanceledException("Selection canceled");
        }
    }

    
    public Beast SelectEnemyBeastTarget()
    {
        _printer.ShowAvailableEnemyBeastTargets();
        int selectedIndex = ReadPlayerIntInput() - 1;

        List<Beast> aliveBeasts = _gameState.BeastTeam.AliveUnits;
        ValidateSelectionCanceling(selectedIndex, aliveBeasts.Count);
        return _gameState.BeastTeam.AliveUnits[selectedIndex];
    }

    public Traveler SelectTravelerAllyTarget(List<Traveler> allies)
    {
        _printer.ShowAvailableAllyTravelerTargets(allies);
        int selectedIndex = ReadPlayerIntInput() - 1;

        ValidateSelectionCanceling(selectedIndex, allies.Count);
        return allies[selectedIndex];
    }
    
    public int AskForBPToUseIfAvailable()
    {
        if (!_gameState.CurrentTraveler.AreThereAnyBPLeft)
            return 0;

        _printer.AskForBPUsage();
        int playerInput = ReadPlayerIntInput();
        while (playerInput > _gameState.CurrentTraveler.BP)
        {
            _printer.ShowInsufficientBPMessage(playerInput);
            _printer.AskForBPUsage();
            playerInput = ReadPlayerIntInput();
        }       
        
        return playerInput;
    }
    
    public TravelerSkillInfo SelectFromAvailableSkills()
    {
        _printer.ShowAvailableSkills();
        int selectedIndex = ReadPlayerIntInput() - 1;
        
        List<TravelerSkillInfo> availableSkills = _gameState.CurrentTraveler.AvailableSkills;
        ValidateSelectionCanceling(selectedIndex, availableSkills.Count);
        return _gameState.CurrentTraveler.AvailableSkills[selectedIndex];
    }
    
    public void ShowFleeMessage()
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine("El equipo de viajeros ha huido!");
        ShowLostGameMessage();
    }
    
    public void ShowVictoryMessage()
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine("Gana equipo del jugador");
    }

    public void ShowLostGameMessage()
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine("Gana equipo del enemigo");
    }
}