using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public abstract class EnemyBase : MonoBehaviour, IGoap
{

	//public Rigidbody rigidBody;
	public Player player;
	public NavMeshAgent agent;

	public int health;
	//public int strength;
	public int defaultSpeed;
	public int patrolSpeed;
	public float regenRate;

	public float attackStamina;

	// Use this for initialization
	void Start()
	{

	}


	public HashSet<KeyValuePair<string, object>> getWorldState()
	{
		HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
		worldData.Add(new KeyValuePair<string, object>("damagePlayer", false)); //to-do: change player's state for world data here
		worldData.Add(new KeyValuePair<string, object>("evadePlayer", false));
		return worldData;
	}

	public abstract HashSet<KeyValuePair<string, object>> createGoalState();

	public void planFailed(HashSet<KeyValuePair<string, object>> failedGoal)
	{

	}

	public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> action)
	{

	}

	public void actionsFinished()
	{

	}

	public void planAborted(GoapAction aborter)
	{

	}

	public abstract void passiveRegen();

	public bool canSeePlayer()
	{
		Vector3 rayPosition = transform.position;
		Vector3 rayDirection = (player.transform.position - rayPosition).normalized;

		RaycastHit info;
		if (Physics.Raycast(rayPosition, rayDirection, out info))
		{
			if (info.transform.CompareTag("Player"))
			{
				
				//Debug.Log("=== see player ====");
				return true;
			}
		}
		//Debug.Log("@@@ don't see player @@@");
		return false;
	}

	public bool withinAttackRange()
	{
		float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < 4.75)
        {
			return true;
        }
		return false;
	}



	public virtual bool moveAgent(GoapAction nextAction)
	{
		//float dist = Vector3.Distance(transform.position, nextAction.target.transform.position);

		if (!canSeePlayer())
		{
			
			//Vector3 moveDirection = player.transform.position - transform.position;
			
			//agent.destination = 
			
			agent.speed = patrolSpeed;

			//	move to the middle of the map and lurk
			agent.destination = Vector3.zero;

			return false;

		}
		else {

			agent.speed = defaultSpeed;
			agent.destination = player.transform.position;

            if (withinAttackRange())
            {
				nextAction.setInRange(true);
				return true;

			}
            else
            {
				return false;
            }
		}
	}
}
