using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlockAction : GoapAction
{
	private bool blocked = false;
	public LayerMask attackTarget;
	


	public BlockAction()
	{
		addEffect("blockPlayer", true);
		cost = 60f;
	}

	public override void reset()
	{
		target = null;
	}

	public override bool isDone()
	{
		return blocked;
	}

	public override bool requiresInRange()
	{
		return false;
	}

	public override bool checkProceduralPrecondition(GameObject agent)
	{
		target = GameObject.Find("Player");
		//Debug.Log("BLOCK checkProcduralPre - found player!");
		return target != null;


	}

	public override bool perform(GameObject agent)
	{

		//	Send agent with forcefield to block a part of the map
		//	Attempt to trap the player and have others damage the player
		//	Cannot damage player when blocking
		Debug.Log($"Block Attack Perform!");
		yellowEnemy currentEnemy = agent.GetComponent<yellowEnemy>();

		//	Retrieve the "blockzone" locations
		GameObject[] BlockZones = GameObject.FindGameObjectsWithTag("BlockZone");





		//int damage = currentEnemy.strength;
		// explode player
		//currentEnemy.player.hitRandomForce(2000);
		//currentEnemy.player.reduceRndHealth(10, 40);
		Debug.Log($"Blocked: {blocked}");
		if (blocked)
		{
			Debug.Log($"Return: true");

			return true;

		}
		else {
			
			if (Vector3.Distance(BlockZones[0].transform.position, transform.position) >
				Vector3.Distance(BlockZones[1].transform.position, transform.position))
			{
				currentEnemy.agent.destination = BlockZones[1].transform.position;
			}
			else
			{
				currentEnemy.agent.destination = BlockZones[0].transform.position;
			}
		}

        if (Vector3.Distance(currentEnemy.agent.destination, currentEnemy.transform.position) < 5)
        {
			blocked = true;
			return true;

		}
		Debug.Log($"action performed: false");
		return false;


		//if (currentEnemy.chargeStamina > 1)
		//{


		//	int damage = currentEnemy.strength;

		//	currentEnemy.player.reduceRndHealth(5, 1);
		//	attacked = true;
		//	return true;
		//}
		//else
		//{
		//	return false;
		//}


	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("BlockZone"))
		{
			//Debug.Log($"BLOCKZONE TOUCHED!");

			blocked = true;
			//Debug.Log($"Blocked: {blocked}");

		}
		//Debug.Log($"No collision with blockzone");

	}

	//	Show radius of forcefield when selected in editor
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 4.5f);
	}



}
