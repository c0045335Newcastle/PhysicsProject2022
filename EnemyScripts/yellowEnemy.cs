using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class yellowEnemy : EnemyBase
{
	[SerializeField]
	Material chargedMatForcefield;
	[SerializeField]
	Material unchargedMatForcefield;

	NavMeshAgent navMeshAgent;

	//GameObject forcefield;



	// Use this for initialization
	void Start()
	{
		defaultSpeed = 7;
		patrolSpeed = 3;
		attackStamina = 5;
		regenRate = 2f;

		navMeshAgent = GetComponent<NavMeshAgent>();
		//strength = 10; //unused as of now

		//forcefield = GameObject.FindGameObjectWithTag("Forcefield");

	}



    private void LateUpdate()
    {
		preventStuck();
	}

    public override void passiveRegen()
	{
		attackStamina += regenRate;
	}

	private void FixedUpdate()
	{
		if (attackStamina < 5)
		{
			//forcefield.GetComponent<MeshRenderer>().material = unchargedMatForcefield;
			attackStamina = Mathf.Max(0.0f, attackStamina + Time.deltaTime);
		}
		else
		{ 
			//forcefield.GetComponent<MeshRenderer>().material = chargedMatForcefield; 
		}
	}
	
	public override HashSet<KeyValuePair<string, object>> createGoalState()
	{
		HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
		goal.Add(new KeyValuePair<string, object>("damagePlayer", true));
		goal.Add(new KeyValuePair<string, object>("blockPlayer", true));

		//goal.Add(new KeyValuePair<string, object>("blockPlayer", true));
		return goal;
	}

	void preventStuck() {
		GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");

        foreach (GameObject wall in walls)
        {
			if (Vector3.Distance(wall.transform.position, transform.position) < 1.5f)
			{
				Debug.Log("prevent stuck!");
				
				Rigidbody rb = GetComponent<Rigidbody>();
				Vector3 force = new Vector3(200f, 50f, 200f);
				rb.AddForce(force);
				return;
			}
		}

		
	}

}
