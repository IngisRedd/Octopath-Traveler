using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;
using Octopath_Traveler.Exceptions;

namespace Octopath_Traveler;

public class BattleController
{
    public bool IsGameStillGoing = true;
    private SetupConsoleView _setupView;
    private RoundConsoleView _roundConsoleView;
    private CombatActionConsoleView _combatActionView;
    private GameState _gameState;

    public BattleController(GameState gameState, SetupConsoleView setupView, RoundConsoleView roundConsoleView, CombatActionConsoleView combatActionView)
    {
        _gameState = gameState;
        _setupView = setupView;
        _roundConsoleView = roundConsoleView;
        _combatActionView = combatActionView;
    }
    
    public void ExecuteBattleRound()
    {
        GameStateUpdater.PerformStartOfRoundUpdates(_gameState);
        _roundConsoleView.ShowRoundHeader();
        try
        {
            while (_gameState.CurrentTurnQueue.Count > 0)
            {
                ExecuteTurn();
            }
        }
        catch (GameOverException exception)
        {
            IsGameStillGoing = false;
        }
        GameStateUpdater.PerformEndOfRoundUpdates(_gameState);
    }
    
    private void ExecuteTurn()
    {
        GameStateUpdater.UpdateCurrentUnit(_gameState);
        _roundConsoleView.ShowTurnInfo();
        if (_gameState.CurrentUnit is Traveler)
        {
            ExecuteTravelerTurn();
        }
        else
        {
            ExecuteBeastTurn();
        }
        GameStateUpdater.EndOfTurnUpdate(_gameState);

        EndOfGameValidator.CheckIfGameIsOver(_gameState, _roundConsoleView);
    }

    private void ExecuteTravelerTurn()
    {
        bool isValidActionSelected = false;
        while (!isValidActionSelected)
        {
            try
            {
                _roundConsoleView.ShowTravelerActions();
                int playerInput = _roundConsoleView.ReadPlayerInput();
                ExecuteTravelerAction(playerInput);
                isValidActionSelected = true;
            }
            catch (ArgumentOutOfRangeException exception){}
        }
    }

    private void ExecuteTravelerAction(int playerInput)
    {
        switch (playerInput)
        {
            case 1:
                ExecuteAttack();
                break;
            case 2:
                ExecuteUseSkill();
                break;
            case 3:
                ExecuteDefend();
                break;
            case 4:
                ExecuteFlee();
                break;
        }
    }

    private void ExecuteAttack()
    {
        AttackAction attackAction = new AttackAction(_gameState, _roundConsoleView);
        attackAction.Execute();
        _combatActionView.ShowCombatActionResults();

    }
    
    private void ExecuteUseSkill()
    {
        UseSkillAction useSkillAction = new UseSkillAction(_gameState, _roundConsoleView);
        useSkillAction.Execute();
        _combatActionView.ShowCombatActionResults();
    }
    
    private void ExecuteDefend()
    {
        DefendAction defendAction = new DefendAction(_gameState, _roundConsoleView);
        defendAction.Execute();
    }

    private void ExecuteFlee()
    {
        FleeAction fleeAction = new FleeAction(_gameState, _roundConsoleView);
        fleeAction.Execute();
    }

    private void ExecuteBeastTurn()
    {
        BeastTurnController beastTurnController = new BeastTurnController(_gameState, _roundConsoleView);
        beastTurnController.Execute();
        _combatActionView.ShowCombatActionResults();
    }
}