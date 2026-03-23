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
    public void RunBattleRound()
    {
        _gameState.RoundCounter++;
        _gameState.StartOfRoundQueueUpdate();
        _view.ShowRoundHeader();
        try
        {
            while (_gameState.CurrentTurnQueue.Count > 0)
            {
                RunTurn();
            }
            _gameState.TravelerTeam.IncreaseBPs();
        }
        catch (GameOverException exception)
        {
            IsGameStillGoing = false;
        }
    }

    private void RunTurn()
    {
        _gameState.UpdateCurrentUnit();
        _view.ShowAllUnitInformation();
        _view.ShowTurnQueues();
        if (_gameState.CurrentUnit is Traveler)
        {
            RunTravelerTurn();
        }
        else
        {
            RunBeastTurn();
        }
        _gameState.UpdateTurnQueues();

        CheckIfGameIsOver();
    }

    private void RunTravelerTurn()
    {
        bool isValidActionSelected = false;
        while (!isValidActionSelected)
        {
            try
            {
                _view.ShowTravelerActions();
                string playerInput = _view.AskForPlayerInput();
                Act(playerInput);
                isValidActionSelected = true;
            }
            catch (ArgumentOutOfRangeException exception){}
        }
    }

    private void Act(string playerInput)
    {
        switch (playerInput)
        {
            case "1":
                RunAttackAction();
                break;
            case "2":
                RunUseSkillAction();
                break;
            case "3":
                RunDefendAction();
                break;
            case "4":
                RunFleeAction();
                break;
        }
    }

    private void RunAttackAction()
    {
        _view.ShowAvailableWeapons();
        string playerInput = _view.AskForPlayerInput();
        
        int inputToInt = Convert.ToInt32(playerInput);
        Traveler currentTraveler = (Traveler)_gameState.CurrentUnit;
        string selectedWeapon = currentTraveler.Weapons[inputToInt - 1];
        
        _view.ShowAvailableTargets();
        playerInput = _view.AskForPlayerInput();
        inputToInt = Convert.ToInt32(playerInput);
        Beast attackTarget = _gameState.BeastTeam.AliveUnits[inputToInt - 1];
        
        int BPToUse = 0;
        if (AreThereAnyBPLeft())
        {
            _view.AskForBPUsage();
            playerInput = _view.AskForPlayerInput();
            BPToUse = Convert.ToInt32(playerInput);
        }

        MakeBasicAttack(attackTarget, selectedWeapon);
    }

    private void MakeBasicAttack(CombatUnit target, string weapon)
    {
        double basicAttackModifier = 1.3;
        Damage damage = CreateDamage(basicAttackModifier, target, weapon);
        AttackTarget(target, damage);
        _view.ShowAttackResults(target, damage);
    }
    
    private bool AreThereAnyBPLeft()
        => ((Traveler)_gameState.CurrentUnit).BP > 0;

    private Damage CreateDamage(double modifier, CombatUnit target, string weapon)
    {
        double damageValue = _gameState.CurrentUnit.PhysAtk * modifier - target.PhysDef;
        if (target.StatusEffects[StatusType.Defend].IsActive)
        {
            damageValue = damageValue * 0.5;
        }
        int damageValueResult = (int)damageValue;
        damageValueResult = Math.Max(0, damageValueResult);
        
        return new Damage(damageValueResult, weapon);
    }

    private void AttackTarget(CombatUnit target, Damage damage)
    {
        target.CurrentHP -= damage.Value;
    }
    
    private void RunUseSkillAction()
    {
        _view.ShowAvailableSkills();
        string playerInput = _view.AskForPlayerInput();

        int inputToInt = Convert.ToInt32(playerInput);
        Traveler currentTraveler = (Traveler)_gameState.CurrentUnit;
        Skill selectedSkill = currentTraveler.AvailableSkills[inputToInt - 1];
    }
    
    private void RunDefendAction()
    {
        _gameState.CurrentUnit.StatusEffects[StatusType.Defend].Duration = 1;
        
        _gameState.NextTurnQueue.Remove(_gameState.CurrentUnit);
        _gameState.NextTurnQueue.Insert(0, _gameState.CurrentUnit);
    }

    private void RunFleeAction()
    {
         _view.ShowFleeMessage();
         throw new GameOverException("Player team surrendered");
    }

    private void RunBeastTurn()
    {
        Traveler attackTarget = _gameState.TravelerTeam.HealthiestUnit;
        string damageType = "Physical";
        MakeBasicAttack(attackTarget, damageType);
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