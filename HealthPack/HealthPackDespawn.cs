using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackDespawn : MonoBehaviour
{
    public float textDestroyTime = 20f;

    private void Start()
    {
        Destroy(gameObject, textDestroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" ||
            collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}

