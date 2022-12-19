using UnityEngine;
using System.Collections.Generic;

public class GOAP_block : GOAP_action
{
    public GOAP_block()
    {
        name = "block";
        cost = 2;
        preconditions = new Dictionary<string, bool>
        {
            { "withinRange", true }
        };
        effects = new Dictionary<string, bool>
        {
            { "blocking", true}
        };
    }

    public override int CalculateCost(Dictionary<string, bool> state)
    {
        // If the player is already blocked, there is no need to block again
        if (state["blocking"] == true)
        {
            return int.MaxValue;
        }

        // Otherwise, return the base cost of the action
        return cost;
    }
}
