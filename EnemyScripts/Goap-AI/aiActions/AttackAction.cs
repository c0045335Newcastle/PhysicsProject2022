using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : GoapAction
{
	private bool attacked = false;
	public LayerMask attackTarget;

	public AttackAction()
	{
		addEffect("damagePlayer", true);
		cost = 100f;
	}

	public override void reset()
	{
		attacked = false;
		target = null;
	}

	public override bool isDone()
	{
		return attacked;
	}

	public override bool requiresInRange()
	{
		return true;
	}

	public override bool checkProceduralPrecondition(GameObject agent)
	{
		target = GameObject.Find("Player");
		//Debug.Log("checkProcduralPre - found player!");

		yellowEnemy currentEnemy = agent.GetComponent<yellowEnemy>();

		if (currentEnemy.player.playerIsAlive == false)
        {
			//	Won't attack player corpse
			return false;
        }

		if (currentEnemy.attackStamina > 4)
        {
            if (target != null)
            {
                if (currentEnemy.canSeePlayer())
                {
					return true;
				}
			}

		}
		return false;
	}

	public override bool perform(GameObject agent)
	{
		Debug.Log($"Action Attack Perform!");
		yellowEnemy currentEnemy = agent.GetComponent<yellowEnemy>();

		//int damage = currentEnemy.strength;

		// forcefield attack player
		currentEnemy.player.hitRandomForce(2000);
		currentEnemy.player.reduceRndHealth(10, 40);
		currentEnemy.attackStamina = 0;

		attacked = true;
		return true;



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

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 4.5f);
	}



}
