using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAP_attack : GOAP_action
{
    public GOAP_attack()
    {
        name = "attack";
        cost = 1;

        preconditions = new Dictionary<string, bool>
        {
            { "withinRange", true }
        };

        effects = new Dictionary<string, bool>
        {
            { "lowPlayerHealth", true }
        };
    }

    public override int CalculateCost(Dictionary<string, bool> state)
    {
        // If the player is already at low health, there is no need to attack
        if (state["lowPlayerHealth"] == true)
        {
            return int.MaxValue;
        }

        // Otherwise, return the base cost of the action
        return cost;
    }
}







