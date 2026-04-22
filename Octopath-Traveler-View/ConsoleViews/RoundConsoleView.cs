using Octopath_Traveler_Model;
using Octopath_Traveler_View.Interfaces;

namespace Octopath_Traveler_View.ConsoleViews;

public class RoundConsoleView : BaseConsoleView, IRoundView
{
    public RoundConsoleView(View view, GameState gameState)
        : base(view, gameState)
    {
        _view = view;
        _gameState = gameState;
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

    private void ShowTurnQueue(List<CombatUnit> turnQueue)
    {
        int label = 1;
        foreach (CombatUnit unit in turnQueue)
        {
            _view.WriteLine($"{label}.{unit.Name}");
            label++;
        }
    }
}