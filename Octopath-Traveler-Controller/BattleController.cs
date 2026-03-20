using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler;

public class BattleController
{
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
        _gameState.ResetTurnQueues();
        _view.ShowRoundHeader();
        while (_gameState.TurnQueue.Count > 0)
        {
            RunTurn();
        }
        _gameState.TravelerTeam.IncreaseBPs();
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
        _gameState.UpdateTurnQueue();
    }
    
    private void RunTravelerTurn()
    {
        _view.ShowTravelerActions();
        string playerInput = _view.AskForPlayerInput();
        Act(playerInput);
    }

    private void Act(string playerInput)
    {
        switch (playerInput)
        {
            case "1":
                RunAttackAction();
                break;
            // case "2":
            //     RunUseSkillAction();
            //     break;
            // case "3":
            //     RunDefendAction();
            //     break;
            // case "4":
            //     RunFleeAction();
            //     throw new GameThrownException($"Player {_activePlayer.GetInt()} has thrown the game.");
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
        int damageValueResult = (int)damageValue; 
        return new Damage(damageValueResult, weapon);
    }

    private void AttackTarget(CombatUnit target, Damage damage)
    {
        target.CurrentHP -= damage.Value;
    }
    
    private void RunBeastTurn()
    {
        Traveler attackTarget = _gameState.TravelerTeam.HealthiestUnit;
        string damageType = "Physical";
        MakeBasicAttack(attackTarget, damageType);
    }

}