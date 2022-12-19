using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class BlueEnemyAi : MonoBehaviour
{
    //  This enemy will patrol and shoot at the player when close


    [SerializeField]
    public Player player;

    public float agentSpeedDefault = 6f;

    private NavMeshAgent agent;
    public LayerMask playerMask;

    float agentSpeed;

    public float attackCooldown;
    bool hasAttacked;

    public float attackRange = 0f;
    public bool inAttachRange;

    public GameObject projectile;

    private void Awake()
    {
        //  Can't attack player right as the game begins!
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

        inAttachRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        //ebug.Log($"Attack Range: {attackRange}\n inAttackRange: {inAttachRange}");

        if (inAttachRange)
        {
            attackPlayer();
            //Debug.Log("In Attack Range -> Attacking!");
        }
        else {
            walkToPlayer();
            //Debug.Log("Not in attack range -> Going to Player!");
        }
        
    }

    void attackPlayer() {
        //  Stand still to attack player
        agent.SetDestination(transform.position);

        //  Fixate on Player
        transform.LookAt(player.transform);
        if (!hasAttacked && player.playerIsAlive)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            //  Enemy tag damages Player
            rb.gameObject.tag = "Enemy";

            rb.AddForce(transform.forward * 50f, ForceMode.Impulse);
            rb.AddForce(transform.forward * 5f, ForceMode.Impulse);


            hasAttacked = true;
            Invoke(nameof(rechargeAttack), attackCooldown);
        }
    }

    void rechargeAttack() {
        hasAttacked = false;
    }

    void walkToPlayer()
    {
        agent.SetDestination(player.transform.position);
       
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


    // https://answers.unity.com/questions/1301622/how-can-i-make-an-ai-that-avoids-obstacles-without.html






}
