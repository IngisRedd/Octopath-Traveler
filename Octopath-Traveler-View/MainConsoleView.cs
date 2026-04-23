using Octopath_Traveler_Model;

namespace Octopath_Traveler_View;

public class MainConsoleView
{
    private View _view;
    private GameState _gameState;
    private string _teamsFolder;

    public MainConsoleView(View view, GameState gameState, string teamsFolder)
    {
        _view = view;
        _gameState = gameState;
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

    public void PrintHorizontalRule()
    {
        _view.WriteLine("----------------------------------------");
    }


    public void ShowTurnInfo()
    {
        ShowAllUnitInformation();
        ShowTurnQueues();
    }

    public void ShowAllUnitInformation()
    {
        PrintHorizontalRule();
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

    public void ShowAvailableTargets()
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

    public void AskForBPUsage()
    {
        PrintHorizontalRule();
        _view.WriteLine($"Seleccione cuantos BP utilizar");
    }

    public void ShowBasicAttack()
    {
        PrintHorizontalRule();
        _view.WriteLine($"{_gameState.CurrentUnit.Name} ataca");
    }

    public void ShowDamageResults(DamageActionResultInfo damageInfo)
    {
        for (int i = 0; i < damageInfo.Targets.Count; i++)
        {
            if (damageInfo.IsTravelerDefending[i])
            {
                ShowDefense(damageInfo.Targets[i]);
            }
            
            if (damageInfo.Targets[i] is Beast)
            {
                Beast beast = (Beast)damageInfo.Targets[i];
                if (beast.IsWeakToDamageType(damageInfo.Damages[i].Type))
                {
                    ShowSuperEffectiveDamageReceived(beast, damageInfo.Damages[i]);
                    if (damageInfo.IsBreakingPointAchieved[i])
                    {
                        ShowBreakingPointAchieved(beast);
                    }

                    continue;
                }
            }

            ShowDamageReceived(damageInfo.Targets[i], damageInfo.Damages[i]);
        }

        foreach (CombatUnit target in damageInfo.Targets)
        {
            ShowFinalHP(target);
        }
    }

    private void ShowDefense(CombatUnit target)
    {
        _view.WriteLine($"{target.Name} se defiende");
    }
    
    private void ShowDamageReceived(CombatUnit target, Damage damage)
    {
        if (damage.Type is DamageType.None) 
        {
            _view.WriteLine($"{target.Name} recibe {damage.Value} de daño");
        }
        else if (damage.Type is DamageType.Phys)
        {
            _view.WriteLine($"{target.Name} recibe {damage.Value} de daño físico");
        }
        else if (damage.Type is DamageType.Elem)
        {
            _view.WriteLine($"{target.Name} recibe {damage.Value} de daño elemental");
        }
        else
        {
            _view.WriteLine($"{target.Name} recibe {damage.Value} de daño de tipo {damage.Type}");
        }
    }
    
    private void ShowSuperEffectiveDamageReceived(CombatUnit attackTarget, Damage damage)
    {
        _view.WriteLine($"{attackTarget.Name} recibe {damage.Value} de daño de tipo {damage.Type} con debilidad");
    }

    private void ShowBreakingPointAchieved(Beast attackTarget)
    {
        _view.WriteLine($"{attackTarget.Name} entra en Breaking Point");
    }
    
    private void ShowFinalHP(CombatUnit attackTarget)
    {
        _view.WriteLine($"{attackTarget.Name} termina con HP:{attackTarget.CurrentHP}");
    }
    
    public void ShowSkillUsage(string skillName)
    {
        PrintHorizontalRule();
        _view.WriteLine($"{_gameState.CurrentUnit.Name} usa {skillName}");
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