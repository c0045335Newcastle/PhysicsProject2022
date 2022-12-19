using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;

public class AStarEnemy : MonoBehaviour
{
    //  Basic NavMesh AI
    //  Will chase player using NavMesh!

    [SerializeField]
    public Player player;

    public float agentSpeedDefault = 6f;
    public float attackCooldown;


    private NavMeshAgent agent = null;

    float agentSpeed;
    public GameObject projectile;


    bool hasAttacked;


    [SerializeField]
    Pathfinding pathFinder;
    // get path from game manager

    List<Node> path;



    private void Start()
    {
        agentSpeed = agentSpeedDefault;
        agent = GetComponent<NavMeshAgent>();

        
    }

    private void FixedUpdate()
    {
        path = pathFinder.retrieveAgentPath();

        if (player.playerIsAlive)
        {
            agent.speed = agentSpeed;
        }
        else
        {
            agent.speed = 0;
        }

        sendAgentAlongPath();
        attackPlayer();


    }

    void rechargeAttack()
    {
        hasAttacked = false;
    }

    void attackPlayer()
    {
        //  Stand still to attack player
        agent.SetDestination(transform.position);

        //  Fixate on Player
        transform.LookAt(player.transform);
        if (!hasAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            //  Enemy tag damages Player
            rb.gameObject.tag = "Enemy";

            rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
            rb.AddForce(transform.forward * 5f, ForceMode.Impulse);


            hasAttacked = true;
            Invoke(nameof(rechargeAttack), attackCooldown);
        }
    }


    public void sendAgentAlongPath()
    {
        //Debug.Log($"path: {path}");
        for (int i = 0; i < path.Count; i++)
        {
            agent.destination = path[i].position;
            Debug.DrawLine(agent.transform.position, path[i].position, Color.blue);
        }
    }





    // https://answers.unity.com/questions/1301622/how-can-i-make-an-ai-that-avoids-obstacles-without.html









}
