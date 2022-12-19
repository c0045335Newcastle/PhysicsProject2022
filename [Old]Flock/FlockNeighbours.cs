using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockNeighbours : MonoBehaviour
{
    [SerializeField]
    GameObject flockEnemy;

    // The size of the flock
    [SerializeField]
    int flockSize;

    // Range to check for neighbours
    [SerializeField]
    float neighbourRange;

    [SerializeField]
    LayerMask flockMask;

    public GameObject[] allFlock;
    public List<FlockMember> flockNeighbours;

    public List<Vector3> flockDirections;

    FlockMember currentMember;

    private void Start()
    {
        allFlock = GameObject.FindGameObjectsWithTag("FlockEnemy");
        currentMember = gameObject.GetComponent<FlockMember>();
       

    }

    private void Update()
    {
        removeNoneNeighbours();
        findFlockNeighbours();
        gatherNeighbourDirection();
        moveWithNeighbours();
    }

    private void gatherNeighbourDirection()
    {
        foreach (FlockMember member in flockNeighbours)
        {
            flockDirections.Add(member.moveDirection);
        }
    }

    private void moveWithNeighbours()
    {
        Vector3 directionToTake = new Vector3(0,0,0);

        for (int i = 0; i < flockDirections.Count; i++)
        {
            directionToTake += flockDirections[i];
        }
        
        //foreach (Vector3 neighbourDir in flockDirections)
        //{
        //    directionToTake += neighbourDir;
        //}

        //Debug.Log($"1:{currentMember.moveDirection}");
        //Debug.Log($"2:{directionToTake}");
        //Debug.Log($"3:{flockDirections.Count}");

        currentMember.moveDirection = directionToTake / flockDirections.Count;
    }

    void findFlockNeighbours() {

        foreach (GameObject unit in allFlock)
        {
            if (unit != gameObject)
            {
                if (!memberInNeighbours())
                {
                    if (Vector3.Distance(transform.position, unit.transform.position) <= neighbourRange)
                    {
                        flockNeighbours.Add(unit.GetComponent<FlockMember>());
                    }
                }
            }
            else{ Debug.Log("nice try"); }
            
        }
    }

    void removeNoneNeighbours()
    {
        //  if no neighbours, nothing to remove
        if (flockNeighbours.Count == 0)
        {
            return;
        }

        //  if "neighbour" is out of range, remove
        for (int i = 0; i < flockNeighbours.Count; i++)
        {
            if (Vector3.Distance(transform.position, flockNeighbours[i].transform.position) > neighbourRange)
            {
                flockNeighbours.Remove(flockNeighbours[i]);
            }
        }
        //foreach (FlockMember unit in flockNeighbours)
        //{
        //    if (Vector3.Distance(transform.position, unit.transform.position) > neighbourRange)
        //    {
        //        flockNeighbours.Remove(unit);
        //    }
        //}
    }

    public void createFlock() {
        for (int i = 0; i < flockSize; i++)
        {
            Vector3 randomLocation = new Vector3((UnityEngine.Random.Range(1, 5)), (UnityEngine.Random.Range(1, 5)), (UnityEngine.Random.Range(1, 5)));

            Instantiate(flockEnemy, transform.position + randomLocation, Quaternion.identity);
        }
    }

    bool memberInNeighbours() {
        foreach (FlockMember m in flockNeighbours)
        {
            if (m.name == this.name)
            {
                return true;
            }
        }
        return false;
    
    }

    public Vector3 directionToNearestMember() {
        float closestDist = 100f;
        Vector3 closestPosition = new Vector3();
        for (int i = 0; i < allFlock.Length; i++)
        {
            if (closestDist > Vector3.Distance(transform.position, allFlock[i].transform.position))
            {
                closestDist = Vector3.Distance(transform.position, allFlock[i].transform.position);
                closestPosition = allFlock[i].transform.position;
            }
        }
        return (transform.position - closestPosition).normalized;
    }

}
