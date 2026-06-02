using Octopath_Traveler_Model;

namespace Octopath_Traveler_View;

public class RoundConsolePrinter : BaseConsoleView
{
    public RoundConsolePrinter(View view, GameState gameState)
        : base(view, gameState){}

    public void ShowAllUnitInformation()
    {
        HorizontalRulePrinter.Print(_view);
        ShowTravelerTeamInformation();
        ShowBeastTeamInformation();
    }

    public void ShowTravelerTeamInformation()
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

    public void ShowBeastTeamInformation()
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

    public void ShowTurnQueues()
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine("Turnos de la ronda");
        ShowTurnQueue(_gameState.CurrentTurnQueue);

        HorizontalRulePrinter.Print(_view);
        _view.WriteLine("Turnos de la siguiente ronda");
        ShowTurnQueue(_gameState.NextTurnQueue);
    }

    public void ShowTurnQueue(TurnQueue turnQueue)
    {
        int label = 1;
        foreach (CombatUnit unit in turnQueue)
        {
            _view.WriteLine($"{label}.{unit.Name}");
            label++;
        }
    }

    public void ShowTravelerActions()
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine($"Turno de {_gameState.CurrentUnit.Name}");
        string travelerActionOptions = "1: Ataque básico\n2: Usar habilidad\n3: Defender\n4: Huir";
        _view.WriteLine(travelerActionOptions);
    }

    public void ShowWeapons(IEnumerable<string> weapons)
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine("Seleccione un arma");
        ShowListedItems(weapons);
    }

    public void ShowListedItems(IEnumerable<string> items)
    {
        int label = 1;
        foreach (string item in items)
        {
            _view.WriteLine($"{label}: {item}");
            label++;
        }
        _view.WriteLine($"{label}: Cancelar");
    }

    public void ShowAvailableEnemyBeastTargets()
    {
        HorizontalRulePrinter.Print(_view);
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

    public void ShowAvailableAllyTravelerTargets(List<Traveler> travelers)
    {
        HorizontalRulePrinter.Print(_view);
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
    
    public void AskForBPUsage()
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine("Seleccione cuantos BP utilizar");
    }

    public void ShowInsufficientBPMessage(int bpToUse)
    {
        HorizontalRulePrinter.Print(_view);
        _view.WriteLine($"{_gameState.CurrentUnit.Name} no tiene {bpToUse} BP para utilizar");
    }

    public void ShowAvailableSkills()
    {
        HorizontalRulePrinter.Print(_view);
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
}