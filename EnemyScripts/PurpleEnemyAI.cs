using UnityEngine.AI;
using UnityEngine;

public class PurpleEnemyAI : MonoBehaviour
{
    //  Basic Enemy
    //  Will chase player when spotted
    //  If can't see player, will hang around/go to one of two nodes
    //  Hoping to see the player


    //public NavMeshAgent agent;
    public Player player;
    public Rigidbody aiRigidBody;

    public float moveForceDefault = 700f;
    float moveForce;

    public GameObject[] nodes;


    private void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");
    }

    private void FixedUpdate()
    {
        if (player.playerIsAlive) {
            moveForce = moveForceDefault;
        }
        else
        {
            moveForce = 0;
        }
        aiMovement();

        //Vector3 flatDir = new Vector3(player.transform.forward.x, 0, player.transform.forward.z).normalized;
        //flatDir = flatDir * moveForce * Time.deltaTime;
        //aiRigidBody.AddForce(flatDir);

    }

    void aiMovement()
    {
        Vector3 directionToTravel = new Vector3();
        Vector3 movement = new Vector3();

        if (canSeePlayer())
        {
            Debug.DrawLine(transform.position, player.transform.position, Color.red);
            directionToTravel = (player.transform.position - transform.position).normalized;
            directionToTravel.y = directionToTravel.y * 0.5f;
        }
        else {
            // calculate distance to each node (side of map)
            float distNodeA = (nodes[0].gameObject.transform.position - transform.position).magnitude;
            float distNodeB = (nodes[1].gameObject.transform.position - transform.position).magnitude;

            
            if (distNodeA > distNodeB)
            {
                Debug.DrawLine(transform.position, nodes[0].gameObject.transform.position, Color.green);
                directionToTravel = chooseClosestNode(1);
                //Debug.Log($"{this.name}: Travelling to node1");

            }
            else {
                Debug.DrawLine(transform.position, nodes[0].gameObject.transform.position, Color.blue);
                directionToTravel = chooseClosestNode(0);
                //Debug.Log($"{this.name}: Travelling to node2");

            }

        }
        movement = directionToTravel * moveForce * Time.deltaTime;
        aiRigidBody.AddForce(movement);

    }

    Vector3 chooseClosestNode(int node) {
        Vector3 currentNode;
        currentNode = nodes[node].gameObject.transform.position;
        currentNode.Set(currentNode.x, 0, currentNode.z);
        Vector3 directionToTravel = (currentNode - transform.position).normalized;
        return directionToTravel;
    }

    

    bool canSeePlayer()
    {
        Vector3 rayPosition = transform.position;
        Vector3 rayDirection = (player.transform.position - rayPosition).normalized;

        RaycastHit info;
        if (Physics.Raycast(rayPosition, rayDirection, out info))
        {
            if (info.transform.CompareTag("Player"))
            {
                // the enemy can see the player!
                //Debug.Log("=== see player ====");
                return true;
            }
        }
        //Debug.Log("[][][] don't see player [][][]");
        return false;
    }

    // https://answers.unity.com/questions/1301622/how-can-i-make-an-ai-that-avoids-obstacles-without.html









}
