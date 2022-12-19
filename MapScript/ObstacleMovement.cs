using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleMovement : MonoBehaviour
{
    public Vector3 positionA = new Vector3(-6.5f, 4.8f, -6.5f);
    public Vector3 positionB = new Vector3(-13f, 4.8f, -14.5f);
    public float speed = 5f;
    float movement;
    public bool movingRight = false;
    Vector3 direction;



    void Start()
    {
        
        transform.position = positionB;
        direction = (positionB - positionA).normalized;
    }

    private void Update()
    {
        movement = speed * Time.deltaTime;
        if (transform.position.x > -6.5)
        {
            //Debug.Log($"222:  {transform.position.x} < {positionB.x} = {transform.position.x < positionB.x}");
            movingRight = true;
        }
            
        if(transform.position.x < -13)
        {
            //Debug.Log($"111:  {transform.position.x} > {positionA.x} = {transform.position.x > positionA.x}");
            movingRight = false;
        }
           

        switch (movingRight)
        {
            case true:
                
                transform.position += direction * movement;
                //Debug.Log("case true");
                break;
            case false:
                //Debug.Log("case false");
                transform.position -= direction * movement;
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
       
            //  When the player is hit, a random force is applied
            Vector3 rndForce = new Vector3();
            rndForce.Set((Random.Range(-1000f, 1000f)), (Random.Range(-1000f, 1000f)), (Random.Range(-1000f, 1000f)));

            //Debug.Log($"HIT FORCE: ({rndForce})");
            //Debug.DrawLine(transform.position, rndForce, Color.cyan);
            rb.AddForce(rndForce);
        
    }










    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    Debug.Log($"Position {transform.position}");

    //    if (transform.position == positionB)
    //    {
    //        Debug.Log("==B");
    //        moveToA();
    //    }
    //    else if (transform.position == positionA)
    //    {
    //        Debug.Log("==A");
    //        moveToB();
    //    }
    //}





    //void moveToA()
    //{
    //    while (transform.position != positionA)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, positionA, movement);
    //    }
    //}



    //void moveToB()
    //{
    //    while (transform.position != positionB)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, positionB, movement);
    //    }
    //}
}