using Octopath_Traveler_Model;

namespace Octopath_Traveler_View;

public class RoundConsoleView : BaseConsoleView
{
    private RoundConsolePrinter _printer;

    public RoundConsoleView(View view, GameState gameState)
        : base(view, gameState)
    {
        _printer = new RoundConsolePrinter(view, gameState);
    }
            
    public void ShowRoundStart()
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine($"INICIA RONDA {_gameState.RoundCounter}");
    }
    
    public void ShowTurnInfo()
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
    
    public DamageType SelectWeapon(List<DamageType> weapons)
    {
        IEnumerable<string> deParsedWeapons = weapons.Select(weapon => weapon.ToString());
        _printer.ShowWeapons(deParsedWeapons);
        int selectedIndex = ReadPlayerIntInput() - 1;
        return weapons[selectedIndex];
    }
    
    public Beast SelectEnemyBeastTarget()
    {
        _printer.ShowAvailableEnemyBeastTargets();
        int selectedIndex = ReadPlayerIntInput() - 1;
        return _gameState.BeastTeam.AliveUnits[selectedIndex];
    }
    
    private int ReadPlayerIntInput()
    {
        string input = _view.ReadLine();
        return Convert.ToInt32(input);
    }

    public Traveler SelectTravelerAllyTarget(List<Traveler> allies)
    {
        _printer.ShowAvailableAllyTravelerTargets(allies);
        int selectedIndex = ReadPlayerIntInput() - 1;
        return allies[selectedIndex];
    }
    
    public int AskForBPToUseIfAvailable()
    {
        if (!_gameState.CurrentTraveler.AreThereAnyBPLeft)
            return 0;

        AskForBPUsage();
        return ReadPlayerIntInput();
    }
    
    private void AskForBPUsage()
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine($"Seleccione cuantos BP utilizar");
    }

    public TravelerSkillInfo SelectFromAvailableSkills()
    {
        _printer.ShowAvailableSkills();
        int selectedIndex = ReadPlayerIntInput() - 1;
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