using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;
using Octopath_Traveler.Exceptions;

namespace Octopath_Traveler;

public class BattleController
{
    public bool IsGameStillGoing = true;
    private MainConsoleView _view;
    private GameState _gameState;

    public BattleController(MainConsoleView view, GameState gameState)
    {
        _view = view;
        _gameState = gameState;
    }
    public void ExecuteBattleRound()
    {
        PerformStartOfRoundUpdates();
        _view.ShowRoundHeader();
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
        PerformEndOfRoundUpdates();
    }

    private void PerformStartOfRoundUpdates()
    {
        _gameState.RoundCounter++;
        GameStateUpdater.StartOfRoundQueueUpdate(_gameState);
    }
    
    private void ExecuteTurn()
    {
        GameStateUpdater.UpdateCurrentUnit(_gameState);
        _view.ShowAllUnitInformation();
        _view.ShowTurnQueues();
        if (_gameState.CurrentUnit is Traveler)
        {
            ExecuteTravelerTurn();
        }
        else
        {
            ExecuteBeastTurn();
        }
        GameStateUpdater.EndOfTurnUpdateTurnQueues(_gameState);

        EndOfGameValidator.CheckIfGameIsOver(_gameState, _view);
    }

    private void ExecuteTravelerTurn()
    {
        bool isValidActionSelected = false;
        while (!isValidActionSelected)
        {
            try
            {
                _view.ShowTravelerActions();
                string playerInput = _view.AskForPlayerInput();
                ExecuteTravelerAction(playerInput);
                isValidActionSelected = true;
            }
            catch (ArgumentOutOfRangeException exception){}
        }
    }

    private void ExecuteTravelerAction(string playerInput)
    {
        switch (playerInput)
        {
            case "1":
                ExecuteAttack();
                break;
            case "2":
                ExecuteUseSkill();
                break;
            case "3":
                ExecuteDefend();
                break;
            case "4":
                ExecuteFlee();
                break;
        }
    }

    private void ExecuteAttack()
    {
        AttackAction attackAction = new AttackAction(_gameState, _view);
        attackAction.Execute();
    }
    
    private void ExecuteUseSkill()
    {
        _view.ShowAvailableSkills();

        int selectedIndex = Utils.ReadPlayerInput(_view) - 1;
        SkillInfo selectedSkillInfo = _gameState.CurrentTraveler.AvailableSkills[selectedIndex];
    }
    
    private void ExecuteDefend()
    {
        DefendAction defendAction = new DefendAction(_gameState, _view);
        defendAction.Execute();
    }

    private void ExecuteFlee()
    {
         _view.ShowFleeMessage();
         throw new GameOverException("Player team surrendered");
    }

    private void ExecuteBeastTurn()
    {
        BeastTurnController beastTurnController = new BeastTurnController(_gameState, _view);
        beastTurnController.Execute();
    }
    
    private void PerformEndOfRoundUpdates()
    {
        _gameState.TravelerTeam.IncreaseBPs();
        GameStateUpdater.UpdateStatusEffectDuration(_gameState);
    }




}