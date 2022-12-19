using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    // The position of the player
    public Vector3 playerPosition;
    // The positions of the enemy agents
    public List<Vector3> enemyPositions;
    // The position of the health kits
    public Vector3 healthKitPosition;
    // The player's health
    public int playerHealth;

    // Constructor that initializes the state with the given values
    public GameState(Vector3 playerPosition, List<Vector3> enemyPositions, Vector3 healthPosition, int playerHealth)
    {
        this.playerPosition = playerPosition;
        this.enemyPositions = enemyPositions;
        this.healthKitPosition = healthPosition;
        this.playerHealth = playerHealth;
    }

    // Check if the game is over (e.g. the player has been caught or has run out of health)
    public bool isGameOver()
    {
        // Check if the player has run out of health
        if (playerHealth <= 0)
        {
            return true;
        }
        // Otherwise, the game is not over
        return false;
    }

    // Get the value of the current state (e.g. how far ahead the player is from the enemy agents and the amount of health remaining)
    public int getValue()
    {
        int value = 0;

        // Calculate the distance between the player and the enemy agents
        foreach (var enemyPosition in enemyPositions)
        {
            value = Mathf.RoundToInt(Vector3.Distance(playerPosition, enemyPosition));
        }
        // Add the amount of health remaining to the value
        value -= playerHealth;

        return value;
    }

    // Apply the given action to the current state and return the resulting state
    public GameState applyAction(OrangeAction action)
    {
        // Create a new state object with the same values as the current state
        GameState newState = new GameState(playerPosition, enemyPositions, healthKitPosition, playerHealth);

        // Update the new state based on the action that is taken
        if (action.actionName == "attackPlayer")
        {
            //  assuming 25 is the average amount of damage
            //  damage is random
            foreach (var enPos in enemyPositions)
            {
                if (enPos == playerPosition)
                {
                    newState.playerHealth -= 25;

                }
            }
        }
        else if (action.actionName == "stealHealth")
        {
            newState.healthKitPosition = healthKitPosition;
            newState.playerHealth += 20;
            //newState.
        }

        // Return the updated state
        return newState;
    }
}