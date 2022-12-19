using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class enemyGOAP : MonoBehaviour
{
    // The player object
    public Player player;

    // The agent's attack radius
    public float attackRadius = 4.6f;

    // The agent's movement speed
    public float moveSpeed = 7;

    NavMeshAgent currentAgent;

    // The two block zone objects
    [SerializeField]
    public GameObject blockZone1;
    [SerializeField]
    public GameObject blockZone2;

    // The agent's current action
    private string currentAction;

    // A dictionary that maps state variables to their values
    private Dictionary<string, bool> state;

    // A list of available actions
    private List<GOAP_action> actions;

    // A list of available goals
    private List<GOAP_goal> goals;

    void Start()
    {
        // Initialize the state dictionary
        state = new Dictionary<string, bool>();

        // Initialize the list of actions
        actions = new List<GOAP_action>();

        currentAgent = GetComponent<NavMeshAgent>();

        // Initialize the list of goals
        goals = new List<GOAP_goal>
        {
            new GOAP_defend_goal(),
            new GOAP_attack_goal()
        };
    }

    void Update()
    {
        LoadActions();
        // Update the state of the world
        UpdateState();


        // Calculate the utility of each goal
        foreach (GOAP_goal goal in goals)
        {
            goal.CalculateUtility(state["withinRange"]);
        }

        // Sort the goals by utility
        goals.Sort((g1, g2) => g2.utility.CompareTo(g1.utility));

        // Select the highest utility goal
        GOAP_goal goalToAchieve = goals[0];

        // Select the best action to take based on the selected goal
        GOAP_action actionToTake = goalToAchieve.SelectAction(actions, state);

        Debug.Log($"actionToTake: {actionToTake}");


        // If an action was selected, execute it
        if (actionToTake != null)
        {
            // Set the agent's current action
            currentAction = actionToTake.name;

            // Execute the action
            if (currentAction == "attack")
            {
                AttackPlayer();
            }
            else if (currentAction == "block")
            {
                BlockPlayer();
            }
        }
        else
        {
            // If no action was selected, do nothing
            currentAction = "idle";
        }
    }

    private void LoadActions()
    {
        GOAP_action[] enemyActions = gameObject.GetComponents<GOAP_action>();
        foreach (GOAP_action act in enemyActions)
        {
            actions.Add(act);
        }
    }

    void UpdateState()
    {
        // Update the state variables

        //  check to see whether the player is within the enemy attack range
        if (withinAttackRange())
        {
            state["withinRange"] = true;
        }
        else
        {
            state["withinRange"] = false;
        }

        //  check to see whether the player is on low health
        if (player.health < 40)
        {
            state["lowPlayerHealth"] = true;
        }
        else
        {
            state["lowPlayerHealth"] = false;
        }

        //  check to see whether the enemy is near a blockzone
        if (Vector3.Distance(transform.position, blockZone1.transform.position) < 5f ||
            Vector3.Distance(transform.position, blockZone2.transform.position) < 5f)
        {
            state["blocking"] = true;
        }
        else
        {
            state["blocking"] = false;
        }
    }

    void AttackPlayer()
    {
        // Set the agent's destination to the player's position
        currentAgent.destination = player.transform.position;

        // Damage the player if the agent is within the attack radius
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRadius)
        {
            player.hitRandomForce(2000);
            player.reduceRndHealth(10, 40);
        }
    }

    void BlockPlayer()
    {
        // Set the agent's destination to the nearest block zone
        currentAgent.destination = Vector3.Distance(blockZone1.transform.position, player.transform.position) < Vector3.Distance(blockZone2.transform.position, player.transform.position) ? blockZone1.transform.position : blockZone2.transform.position;

        // Move the agent towards the block zone
        
    }

    public bool withinAttackRange()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < attackRadius)
        {
            return true;
        }
        return false;
    }

}