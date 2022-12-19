using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public float speed = 5f;
    float rotationSpeed = 10f;

    Vector3 averageHeading;
    Vector3 averagePosition;

    float neighbourDistance = 5.5f;
    bool turnAround = false;


    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(5f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldTurnAround())
        {
            turnAround = true;
        }
        else
        {
            turnAround = false;
        }

        if (turnAround)
        {
            Vector3 direction = Vector3.zero - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                    Quaternion.LookRotation(direction),
                                    rotationSpeed * Time.deltaTime);

            speed = Random.Range(3f, 5.25f);
        }
        else
        {
            if (Random.Range(0, 5) < 1)
            {
                applyFlockRules();
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }


    bool shouldTurnAround() {
        //  return true if near the edge of the box
        //  turn around!
        if (this.transform.position.x - 2 < GlobalFlock.lowerBoundX ||
            this.transform.position.x + 2 > GlobalFlock.upperBoundX ||
            this.transform.position.z - 2 < GlobalFlock.lowerBoundZ ||
            this.transform.position.z + 2 > GlobalFlock.upperBoundZ)
        {
            return true;
        }
        return false;
    }

    void applyFlockRules() {
        //  gather all other flock members
        GameObject[] flockMembers;
        flockMembers = GlobalFlock.allFlockMembers;

        //  centre of flock
        Vector3 fCentre = Vector3.zero;
        //  avoid neighbours
        Vector3 fAvoid = Vector3.zero;

        Vector3 memberGoalPosition = GlobalFlock.goalPosition;


        float distance;
        //  who is within the neighbourDistance
        int groupSize = 0;

        foreach (GameObject member in flockMembers)
        {
            if (member != this.gameObject)
            {
                distance = Vector3.Distance(member.transform.position, this.transform.position);
                if (distance <= neighbourDistance)
                {
                    fCentre += member.transform.position;
                    groupSize++;

                    //  near collision
                    if (distance < 0.5f)
                    {
                        //  move off in other direction
                        fAvoid = fAvoid + (this.transform.position - member.transform.position);
                    }

                    //  average speed
                    //Flock anotherFlock = member.GetComponent<Flock>();
                    //fSpeed = fSpeed + anotherFlock.speed;
                }
            }
        }
        //  if in a group
        if (groupSize > 0)
        {
            //  find average values
            fCentre = fCentre / groupSize + (memberGoalPosition - this.transform.position);
            //speed = fSpeed / groupSize;

            //  new direction
            Vector3 direction = (fCentre + fAvoid) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      rotationSpeed * Time.deltaTime);
            }
        }
    }
}
