using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;
using System;

public class OrangeEnemy : MonoBehaviour
{
    //  MinMax NavMesh AI
    //  Will chase player using NavMesh!

    [SerializeField]
    public Player player;

    public float agentSpeedDefault = 12f;

    private NavMeshAgent agent = null;

    float agentSpeed;

    public List<OrangeAction> enemyActions = new List<OrangeAction>();
    public OrangeAction bestAction;


    private void Start()
    {
        agentSpeed = agentSpeedDefault;
        agent = GetComponent<NavMeshAgent>();
        //availableActions.Add(new OrangeAction("test", false, 43));
        createActions();


    }

    private void FixedUpdate()
    {
        GameState currentState = GameStateManager.currentGameState;

        updateAvailableActions();

        if (player.playerIsAlive)
        {
            agent.speed = agentSpeed;
        }
        else
        {
            agent.speed = 0;
        }
        //agentMinimax();
        bestAction = enemyActions[UnityEngine.Random.Range(0, enemyActions.Count)];
        var returnMinMax = minmax(currentState, 1, true, enemyActions, ref bestAction);

        if (bestAction != null)
        {
            switch (bestAction.actionName)
            {
                case "attackPlayer":
                    //Debug.Log("chasing player to attack!");
                    chasePlayer();
                    break;

                case "stealHealth":
                    //Debug.Log("chasing health to steal!");
                    stealHealth();
                    break;
            }
        }

        
    }

    public int evaluateSteal() {
        switch (player.currentPlayerState)
        {
            case Player.playerState.healthy:
                return 40;
            case Player.playerState.hurt:
                return 25;
            case Player.playerState.weak:
                return 50;
            case Player.playerState.critical:
                return 40;
        }

        return 0;
    }

    public int evaluateAttack()
    {
        if (HealthKitAvailable())
        {
            switch (player.currentPlayerState)
            {
                case Player.playerState.healthy:
                    return 25;
                case Player.playerState.hurt:
                    return 40;
                case Player.playerState.weak:
                    return 70;
                case Player.playerState.critical:
                    return 100;
            }
            return 0;
        }
        else
        {
            return int.MinValue;
        }
    }

    public bool minimaxNodeIsTerminal() {
        //  25 health and below is "critical"
        //  attacking the player means a big chance of killing
        //  thus winning
        return (player.currentPlayerState == Player.playerState.dead);
    
    }

    //public void agentMinimax(int depth, int alpha, int beta, bool maxiPlayer)
    //{
    //    if (depth == 0 || minimaxNodeIsTerminal())
    //    {
    //        if (minimaxNodeIsTerminal())
    //        {

    //        }
    //    }


    //}





    public int minmax(GameState state, int depth, bool isMaximizingPlayer, List<OrangeAction> actions, 
                                                                                ref OrangeAction bestAction)
    {
        //bestAction = actions[UnityEngine.Random.Range(0, actions.Count-1)];
        //bestAction = actions[1];
        // If the game is over or the maximum depth has been reached, return the value of the current state
        if (state.isGameOver() || depth == 0)
        {
            if (minimaxNodeIsTerminal())
            {
                //  player dead, game over
                //  goal for ai
                return int.MaxValue;
            }
            else
            {
                return bestAction.actionValue;
            }
        }
        // If it's the maximizing player's turn, find the maximum value
        if (isMaximizingPlayer)
        {
            int bestValue = int.MinValue;
            foreach (var action in actions)
            {
                int value = minmax(state.applyAction(action), depth - 1, false, actions, ref bestAction);
                if (value > bestValue)
                {
                    bestValue = value;
                    bestAction = action;

                }
            }
            return bestValue;
        }
        // If it's the minimizing player's turn, find the minimum value
        else
        {
            int bestValue = int.MaxValue;

            foreach (var action in actions)
            {
                int value = minmax(state.applyAction(action), depth - 1, true, actions, ref bestAction);
                if (value < bestValue)
                {
                    bestValue = value;
                    bestAction = action;
                }
            }
            return bestValue;
        }
    }
    //public int minmax(int depth, bool isMaximizingPlayer, List<OrangeAction> actions)
    //{
    //    // If the game is over or the maximum depth has been reached, return the value of the current state
    //    if (minimaxNodeIsTerminal() || depth == 0)
    //    {
    //        if (minimaxNodeIsTerminal())
    //        {
    //            return int.MaxValue;

    //        }
    //        else // Depth == 0
    //        {
    //            evaluateAttack();
    //        }
    //    }

    //    // If it's the maximizing player's turn, find the maximum value
    //    if (isMaximizingPlayer)
    //    {
    //        int bestValue = int.MinValue;
    //        foreach (var action in actions)
    //        {
    //            int value = minmax(depth - 1, false, actions);
    //            bestValue = Mathf.Max(bestValue, value);
    //        }
    //        return bestValue;
    //    }
    //    // If it's the minimizing player's turn, find the minimum value
    //    else
    //    {
    //        int bestValue = int.MaxValue;
    //        foreach (var action in actions)
    //        {
    //            int value = minmax(depth - 1, true, actions);
    //            bestValue = Mathf.Min(bestValue, value);
    //        }
    //        return bestValue;
    //    }

//}

    //public int minmax(GameState state, int depth, bool isMaximizingPlayer, List<OrangeAction> actions)
    //{
    //    // If the game is over or the maximum depth has been reached, return the value of the current state
    //    if (state.isGameOver() || depth == 0)
    //    {
    //        return state.getValue();
    //    }

    //    // If it's the maximizing player's turn, find the maximum value
    //    if (isMaximizingPlayer)
    //    {
    //        int bestValue = int.MinValue;
    //        foreach (var action in actions)
    //        {
    //            int value = minmax(state.applyAction(action), depth - 1, false, actions);
    //            bestValue = Mathf.Max(bestValue, value);
    //        }
    //        return bestValue;
    //    }
    //    // If it's the minimizing player's turn, find the minimum value
    //    else
    //    {
    //        int bestValue = int.MaxValue;
    //        foreach (var action in actions)
    //        {
    //            int value = minmax(state.applyAction(action), depth - 1, true, actions);
    //            bestValue = Mathf.Min(bestValue, value);
    //        }
    //        return bestValue;
    //    }

    //}




    void chasePlayer()
    {
        agent.SetDestination(player.transform.position);
       
    }

    void stealHealth()
    {
        GameObject healthKit = GameObject.FindGameObjectWithTag("HealthPack");
        if (healthKit != null)
        {
            agent.SetDestination(healthKit.transform.position);
        }

    }

    bool HealthKitAvailable() {
        GameObject HealthPack = GameObject.FindGameObjectWithTag("HealthPack");
        if (HealthPack != null)
        {
            return true;
        }
        return false;
    }

    void updateAvailableActions() {
            foreach (OrangeAction act in enemyActions)
            {
                if (act.actionName == "stealHealth")
                {
                    if (HealthKitAvailable())
                    {
                        act.canDoAction = true;
                    }
                    else
                    {
                        //  if there's no HP, it cannot be stolen
                        act.canDoAction = false;
                    }
                    act.actionValue = evaluateSteal();
                }
                else if (act.actionName == "attackPlayer")
                {
                    if (player.playerIsAlive)
                    {
                        act.canDoAction = true;

                    }
                    else
                    {
                        act.canDoAction = false;

                    }
                    act.actionValue = evaluateAttack();
            }
        }
    }

    void createActions() {
        enemyActions.Add(new OrangeAction("attackPlayer", true, evaluateAttack()));
        enemyActions.Add(new OrangeAction("stealHealth", true, evaluateSteal()));
    }



    // https://answers.unity.com/questions/1301622/how-can-i-make-an-ai-that-avoids-obstacles-without.html










}

