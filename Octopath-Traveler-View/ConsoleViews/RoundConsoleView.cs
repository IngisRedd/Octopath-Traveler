using Octopath_Traveler_Model;

namespace Octopath_Traveler_View;

public class RoundConsoleView : BaseConsoleView
{
    public RoundConsoleView(View view, GameState gameState)
        : base(view, gameState){}
            
    public void ShowRoundStart()
    {
        PrintHorizontalRule();
        _view.WriteLine($"INICIA RONDA {_gameState.RoundCounter}");
    }
    
    public void ShowTurnInfo()
    {
        ShowAllUnitInformation();
        ShowTurnQueues();
    }

    private void ShowAllUnitInformation()
    {
        PrintHorizontalRule();
        ShowTravelerTeamInformation();
        ShowBeastTeamInformation();
    }

    private void ShowTravelerTeamInformation()
    {
        _view.WriteLine("Equipo del jugador");
        char labelLetter = 'A';
        foreach (Traveler traveler in _gameState.TravelerTeam.Units)
        {
            _view.WriteLine(
                $"{labelLetter}-{traveler.Name} - " +
                $"HP:{traveler.CurrentHP}/{traveler.MaxHP} " +
                $"SP:{traveler.CurrentSP}/{traveler.MaxSP} " +
                $"BP:{traveler.BP}"
            );
            labelLetter++;
        }
    }

    private void ShowBeastTeamInformation()
    {
        _view.WriteLine("Equipo del enemigo");
        char labelLetter = 'A';
        foreach (Beast beast in _gameState.BeastTeam.Units)
        {
            _view.WriteLine(
                $"{labelLetter}-{beast.Name} - " +
                $"HP:{beast.CurrentHP}/{beast.MaxHP} " +
                $"Shields:{beast.CurrentShields}"
            );
            labelLetter++;
        }
    }

    private void ShowTurnQueues()
    {
        PrintHorizontalRule();
        _view.WriteLine("Turnos de la ronda");
        ShowTurnQueue(_gameState.CurrentTurnQueue);

        PrintHorizontalRule();
        _view.WriteLine("Turnos de la siguiente ronda");
        ShowTurnQueue(_gameState.NextTurnQueue);
    }

    private void ShowTurnQueue(TurnQueue turnQueue)
    {
        int label = 1;
        foreach (CombatUnit unit in turnQueue)
        {
            _view.WriteLine($"{label}.{unit.Name}");
            label++;
        }
    }

    public CombatActionType SelectTravelerCombatAction()
    {
        ShowTravelerActions();
        int playerInput = ReadPlayerIntInput();
        return (CombatActionType)playerInput;
    }
    
    private void ShowTravelerActions()
    {
        PrintHorizontalRule();
        _view.WriteLine($"Turno de {_gameState.CurrentUnit.Name}");
        string travelerActionOptions = "1: Ataque básico\n2: Usar habilidad\n3: Defender\n4: Huir";
        _view.WriteLine(travelerActionOptions);
    }
    
    public DamageType SelectWeapon(List<DamageType> weapons)
    {
        IEnumerable<string> deParsedWeapons = weapons.Select(weapon => weapon.ToString());
        ShowWeapons(deParsedWeapons);
        int selectedIndex = ReadPlayerIntInput() - 1;
        return weapons[selectedIndex];
    }

    private void ShowWeapons(IEnumerable<string> weapons)
    {
        PrintHorizontalRule();
        _view.WriteLine("Seleccione un arma");
        ShowListedItems(weapons);
    }

    private void ShowListedItems(IEnumerable<string> items)
    {
        int label = 1;
        foreach (string item in items)
        {
            _view.WriteLine($"{label}: {item}");
            label++;
        }
        _view.WriteLine($"{label}: Cancelar");
    }

    public Beast SelectEnemyBeastTarget()
    {
        ShowAvailableEnemyBeastTargets();
        int selectedIndex = ReadPlayerIntInput() - 1;
        return _gameState.BeastTeam.AliveUnits[selectedIndex];
    }
    
    private void ShowAvailableEnemyBeastTargets()
    {
        PrintHorizontalRule();
        _view.WriteLine($"Seleccione un objetivo para {_gameState.CurrentUnit.Name}");
        int label = 1;
        List<Beast> aliveBeasts = _gameState.BeastTeam.AliveUnits;
        foreach (Beast beast in aliveBeasts)
        {
            _view.WriteLine(
                $"{label}: {beast.Name} - " +
                $"HP:{beast.CurrentHP}/{beast.MaxHP} " +
                $"Shields:{beast.CurrentShields}"
            );
            label++;
        }

        _view.WriteLine($"{label}: Cancelar");
    }

    private int ReadPlayerIntInput()
    {
        string input = AskForPlayerInput();
        return Convert.ToInt32(input);
    }

    private string AskForPlayerInput()
    {
        return _view.ReadLine();
    }

    public Traveler SelectTravelerAllyTarget(List<Traveler> allies)
    {
        ShowAvailableAllyTravelerTargets(allies);
        int selectedIndex = ReadPlayerIntInput() - 1;
        return allies[selectedIndex];
    }
    
    private void ShowAvailableAllyTravelerTargets(List<Traveler> travelers)
    {
        PrintHorizontalRule();
        _view.WriteLine($"Seleccione un objetivo para {_gameState.CurrentUnit.Name}");
        int label = 1;
        foreach (Traveler traveler in travelers)
        {
            _view.WriteLine(
                $"{label}: {traveler.Name} - " +
                $"HP:{traveler.CurrentHP}/{traveler.MaxHP} " +
                $"SP:{traveler.CurrentSP}/{traveler.MaxSP} " +
                $"BP:{traveler.BP}"
            );
            label++;
        }

        _view.WriteLine($"{label}: Cancelar");
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
        PrintHorizontalRule();
        _view.WriteLine($"Seleccione cuantos BP utilizar");
    }

    public TravelerSkillInfo SelectFromAvailableSkills()
    {
        ShowAvailableSkills();
        int selectedIndex = ReadPlayerIntInput() - 1;
        return _gameState.CurrentTraveler.AvailableSkills[selectedIndex];
    }

    private void ShowAvailableSkills()
    {
        PrintHorizontalRule();
        Traveler currentTraveler = _gameState.CurrentTraveler;
        _view.WriteLine($"Seleccione una habilidad para {currentTraveler.Name}");
        int label = 1;
        foreach (SkillInfo skill in currentTraveler.AvailableSkills)
        {
            _view.WriteLine($"{label}: {skill.Name}");
            label++;
        }
        _view.WriteLine($"{label}: Cancelar");
    }
    
    public void ShowFleeMessage()
    {
        PrintHorizontalRule();
        _view.WriteLine("El equipo de viajeros ha huido!");
        ShowLostGameMessage();
    }
    
    public void ShowVictoryMessage()
    {
        PrintHorizontalRule();
        _view.WriteLine("Gana equipo del jugador");
    }

    public void ShowLostGameMessage()
    {
        PrintHorizontalRule();
        _view.WriteLine("Gana equipo del enemigo");
    }
}