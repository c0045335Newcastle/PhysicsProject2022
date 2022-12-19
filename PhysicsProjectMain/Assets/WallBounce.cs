using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        wallBounce(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        wallBounce(collision);
    }

    void hitRandomForce(Rigidbody rb)
    {
        //  When the player is hit, a random force is applied
        Vector3 rndForce = new Vector3();
        rndForce.Set((Random.Range(-200f, 200f)), (Random.Range(0, 0)), (Random.Range(-200f, 200f)));

        //Debug.Log($"HIT FORCE: ({rndForce})");
        //Debug.DrawLine(transform.position, rndForce, Color.cyan);
        rb.AddForce(rndForce);
    }


    void wallBounce(Collision collision) {
        if (collision.gameObject.tag == "Enemy")
        {
            hitRandomForce(collision.gameObject.GetComponent<Rigidbody>());
        }
    }
}
