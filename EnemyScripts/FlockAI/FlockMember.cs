using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockMember : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float speed;
    [SerializeField]
    float moveForce;


    public enum FlockMemberState
    {
        seePlayer,
        flocking,
        lost
    }

    public FlockMemberState stateThing;

    

    Rigidbody rb;
    FlockNeighbours currentNeighbours;

    public Vector3 moveDirection;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        currentNeighbours = gameObject.GetComponent<FlockNeighbours>();

    }

    void FixedUpdate()
    {
        //  Move to Player
        //  0 as the y prevents levitation
        Vector3 targetDest = new Vector3(target.position.x, 0f, target.position.z);
        if (canSeeTarget("Player"))
        {
            stateThing = FlockMemberState.seePlayer;

            moveDirection = (targetDest - gameObject.transform.position).normalized;
            Debug.Log($"target pos: {target.position}");

            //transform.Translate(moveDirection * speed * Time.deltaTime);
        }
        else {
            //  goto nearest neighbour neighbour
            if (canSeeTarget("FlockEnemy"))
            {
                stateThing = FlockMemberState.flocking;
                moveDirection = currentNeighbours.directionToNearestMember();
                //  assuming they're straggling
                moveDirection = moveDirection * 3f;
            }
            else
            {
                stateThing = FlockMemberState.lost;
            }
        }

        rb.AddForce(moveDirection * moveForce);


    }

    bool canSeeTarget(string tag)
    {
        Vector3 rayPosition = transform.position;
        Vector3 rayDirection = (target.transform.position - rayPosition).normalized;

        RaycastHit info;
        if (Physics.Raycast(rayPosition, rayDirection, out info))
        {
            if (info.transform.CompareTag(tag))
            {
                // the enemy can see the player!
                Debug.Log("=== see player ====");
                return true;
            }
        }
        Debug.Log("[][][] don't see player [][][]");
        return false;
    }

}
