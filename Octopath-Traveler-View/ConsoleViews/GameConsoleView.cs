using Octopath_Traveler_Model;

namespace Octopath_Traveler_View;

public class GameConsoleView : BaseConsoleView
{
    private string _teamsFolder;

    public GameConsoleView(View view, GameState gameState, string teamsFolder)
    : base(view, gameState)
    {
        _teamsFolder = teamsFolder;
    }

    public string GetTeamsFilePath()
    {
        ShowPossibleTeamsFiles();
        string teamChosenInput = _view.ReadLine();
        return GetTeamsFilePath(teamChosenInput);
    }

    private void ShowPossibleTeamsFiles()
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        string[] files = Directory.GetFiles(_teamsFolder);

        int index = 0;
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            _view.WriteLine($"{index}: {fileName}");
            index++;
        }
    }

    private string GetTeamsFilePath(string teamChosenInput)
    {
        string[] files = Directory.GetFiles(_teamsFolder);
        string chosenFilePath = "";

        int index = 0;
        foreach (string file in files)
        {
            if (FileIsTheChosenOne(index, teamChosenInput))
            {
                chosenFilePath = file;
            }

            index++;
        }

        return chosenFilePath;
    }

    private bool FileIsTheChosenOne(int fileIndex, string teamChosenInput) =>
        fileIndex.ToString() == teamChosenInput;

    public void ShowInvalidTeamMessage()
    {
        _view.WriteLine("Archivo de equipos no válido");
    }

    public void ShowRoundHeader()
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

    public void ShowTravelerActions()
    {
        PrintHorizontalRule();
        _view.WriteLine($"Turno de {_gameState.CurrentUnit.Name}");
        string travelerActionOptions = "1: Ataque básico\n2: Usar habilidad\n3: Defender\n4: Huir";
        _view.WriteLine(travelerActionOptions);
    }

    public string AskForPlayerInput()
    {
        return _view.ReadLine();
    }

    public void ShowAvailableWeapons()
    {
        PrintHorizontalRule();
        _view.WriteLine("Seleccione un arma");
        int label = 1;
        foreach (string weapon in _gameState.CurrentTraveler.Weapons)
        {
            _view.WriteLine($"{label}: {weapon}");
            label++;
        }

        _view.WriteLine($"{label}: Cancelar");
    }

    public Beast SelectEnemyBeastTarget()
    {
        ShowAvailableEnemyBeastTargets();
        int selectedIndex = ReadPlayerInput() - 1;
        return _gameState.BeastTeam.AliveUnits[selectedIndex];
    }
    
    public void ShowAvailableEnemyBeastTargets()
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

    public int ReadPlayerInput()
    {
        string input = AskForPlayerInput();
        return Convert.ToInt32(input);
    }
    
    public Traveler SelectTravelerAllyTarget()
    {
        ShowAvailableAllyTravelerTargets();
        int selectedIndex = ReadPlayerInput() - 1;
        return _gameState.TravelerTeam.AliveUnits[selectedIndex];
    }
    
    public void ShowAvailableAllyTravelerTargets()
    {
        PrintHorizontalRule();
        _view.WriteLine($"Seleccione un objetivo para {_gameState.CurrentUnit.Name}");
        int label = 1;
        List<Traveler> aliveTravelers = _gameState.TravelerTeam.AliveUnits;
        foreach (Traveler traveler in aliveTravelers)
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
        return ReadPlayerInput();
    }

    
    public void AskForBPUsage()
    {
        PrintHorizontalRule();
        _view.WriteLine($"Seleccione cuantos BP utilizar");
    }
    
    public void ShowAvailableSkills()
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