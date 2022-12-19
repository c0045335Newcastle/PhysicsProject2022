using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement2 : MonoBehaviour
{
    public Vector3 positionA = new Vector3(-6.5f, 4.8f, -6.5f);
    public Vector3 positionB = new Vector3(-13f, 4.8f, -14.5f);
    public float speed = 100f;
    float movement;


    void Start()
    {
        movement = speed * Time.deltaTime;
        transform.position = positionA;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Position {transform.position}");

        Vector3 dir = (positionA - positionB).normalized;



    }
}



