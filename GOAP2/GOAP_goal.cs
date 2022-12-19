using UnityEngine;
using System.Collections.Generic;

public abstract class GOAP_goal
{
    // The name of the goal
    public string name;

    // The utility of the goal
    public int utility;

    // A dictionary that maps state variables to their desired values
    public Dictionary<string, bool> conditions;

    // Calculates the utility of the goal based on the current state of the world
    public abstract void CalculateUtility(bool withinRange);

    // Selects the best action to take based on the available actions and the current state of the world
    public abstract GOAP_action SelectAction(List<GOAP_action> actions, Dictionary<string, bool> state);

    public bool withinAttackRange(GameObject enemy, GameObject player)
    {
        float distance = Vector3.Distance(enemy.transform.position, player.transform.position);

        if (distance < 4.75)
        {
            return true;
        }
        return false;
    }
}