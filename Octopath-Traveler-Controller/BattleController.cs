using Octopath_Traveler_Model;
using Octopath_Traveler_View;
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
        _gameState.StartOfRoundQueueUpdate();
    }
    
    private void ExecuteTurn()
    {
        _gameState.UpdateCurrentUnit();
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
        _gameState.EndOfTurnUpdateTurnQueues();

        CheckIfGameIsOver();
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
        string selectedWeapon = SelectWeapon();
        Beast attackTarget = SelectTarget();
        int BPToUse = AskForBPToUseIfAvailable();

        MakeBasicAttack(attackTarget, selectedWeapon);
    }

    private string SelectWeapon()
    {
        _view.ShowAvailableWeapons();
        int selectedIndex = ReadPlayerInput() - 1;
        return _gameState.CurrentTraveler.Weapons[selectedIndex];
    }
    
    private int ReadPlayerInput()
    {
        string input = _view.AskForPlayerInput();
        return Convert.ToInt32(input);
    }
    
    private Beast SelectTarget()
    {
        _view.ShowAvailableTargets();
        int selectedIndex = ReadPlayerInput() - 1;
        return _gameState.BeastTeam.AliveUnits[selectedIndex];
    }

    private int AskForBPToUseIfAvailable()
    {
        if (!AreThereAnyBPLeft())
            return 0;

        _view.AskForBPUsage();
        return ReadPlayerInput();
    }
    private bool AreThereAnyBPLeft()
        => (_gameState.CurrentTraveler.BP > 0);
    
    private void MakeBasicAttack(CombatUnit target, string weapon)
    {
        double basicAttackModifier = 1.3;
        Damage damage = new Damage(basicAttackModifier, _gameState.CurrentUnit, target, weapon);
        AttackTarget(target, damage);
        _view.ShowAttackResults(target, damage);
    }
    
    private void AttackTarget(CombatUnit target, Damage damage)
    {
        target.CurrentHP -= damage.Value;
    }
    
    private void ExecuteUseSkill()
    {
        _view.ShowAvailableSkills();

        int selectedIndex = ReadPlayerInput() - 1;
        Skill selectedSkill = _gameState.CurrentTraveler.AvailableSkills[selectedIndex];
    }
    
    private void ExecuteDefend()
    {
        _gameState.CurrentUnit.StatusEffects[StatusType.Defend].Duration = 1;
        MoveCurrentUnitToFrontOfNextTurnQueue();        
    }

    private void MoveCurrentUnitToFrontOfNextTurnQueue()
    {
        _gameState.NextTurnQueue.Remove(_gameState.CurrentUnit);
        _gameState.NextTurnQueue.Insert(0, _gameState.CurrentUnit);
    }

    private void ExecuteFlee()
    {
         _view.ShowFleeMessage();
         throw new GameOverException("Player team surrendered");
    }

    private void ExecuteBeastTurn()
    {
        Traveler attackTarget = _gameState.TravelerTeam.HealthiestUnit;
        string damageType = "Physical";
        MakeBasicAttack(attackTarget, damageType);
    }
    
    private void PerformEndOfRoundUpdates()
    {
        _gameState.TravelerTeam.IncreaseBPs();
        _gameState.UpdateStatusEffectDuration();
    }


    private void CheckIfGameIsOver()
    {
        if (AreAllBeastsDefeated())
        {
            _view.ShowVictoryMessage();
            throw new GameOverException("All enemies defeated");
        }
        if (AreAllTravelersDefeated())
        {
            _view.ShowLostGameMessage();
            throw new GameOverException("All travelers in team defeated");
        }
    }

    private bool AreAllBeastsDefeated()
        => _gameState.BeastTeam.AliveUnits.Count <= 0;
    private bool AreAllTravelersDefeated()
        => _gameState.TravelerTeam.AliveUnits.Count <= 0;


}