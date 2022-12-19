using UnityEngine.AI;
using UnityEngine;

public class RedEnemyAI : MonoBehaviour
{
    //  Basic NavMesh AI
    //  Will chase player using NavMesh!

    [SerializeField]
    public Player player;

    public float agentSpeedDefault = 6f;

    private NavMeshAgent agent = null;

    float agentSpeed;


    private void Start()
    {
        agentSpeed = agentSpeedDefault;
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (player.playerIsAlive)
        {
            agent.speed = agentSpeed;
        }
        else
        {
            agent.speed = 0;
        }
        agentMovement();

    }

    void agentMovement()
    {
        agent.SetDestination(player.transform.position);
       
    }



    // https://answers.unity.com/questions/1301622/how-can-i-make-an-ai-that-avoids-obstacles-without.html









}
