using UnityEngine;
using System.Collections.Generic;

public class GOAP_attack_goal : GOAP_goal
{
    public GOAP_attack_goal()
    {
        name = "attack";
        conditions = new Dictionary<string, bool>
        {
            { "withinRange", true}
        };
    }

    public override void CalculateUtility(bool withinRange)
    {
        // If the player is far, the utility of the attack goal is high
        if (withinRange)
        {
            utility = 10;
        }
        else
        {
            utility = 0;
        }
    }

    public override GOAP_action SelectAction(List<GOAP_action> actions, Dictionary<string, bool> state)
    {
        // Sort the actions by cost
        actions.Sort((a1, a2) => a1.CalculateCost(state).CompareTo(a2.CalculateCost(state)));

        // Select the cheapest action that satisfies the preconditions of the attack goal
        foreach (GOAP_action action in actions)
        {
            bool satisfiesPreconditions = true;
            foreach (KeyValuePair<string, bool> precondition in conditions)
            {
                if (action.preconditions[precondition.Key] != precondition.Value)
                {
                    satisfiesPreconditions = false;
                    break;
                }
            }

            if (satisfiesPreconditions)
            {
                return action;
            }
        }

        // If no action was found, return null
        return null;
    }
}
