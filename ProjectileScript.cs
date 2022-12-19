using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float textDestroyTime = 3.5f;

    private void Start()
    {
        Destroy(gameObject, textDestroyTime);
    }
}
