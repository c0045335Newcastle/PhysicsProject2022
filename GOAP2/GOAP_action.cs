using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GOAP_action : MonoBehaviour
{
    // The name of the action
    public string name;

    // The cost of the action
    public int cost;

    // A dictionary that maps state variables to their desired values
    public Dictionary<string, bool> preconditions;

    // A dictionary that maps state variables to their changed values
    public Dictionary<string, bool> effects;

    // Calculates the cost of the action based on the current state of the world
    public abstract int CalculateCost(Dictionary<string, bool> state);
}
