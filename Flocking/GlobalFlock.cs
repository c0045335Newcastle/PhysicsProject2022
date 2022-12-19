using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour
{
    public GameObject flockPrefab;
    public static int numberInFlock = 8;

    //  static to allow access elsewhere
    public static GameObject[] allFlockMembers = new GameObject[numberInFlock];

    //  co-ord limits of flock spawn
    public static float lowerBoundX = -18.2f;
    public static float upperBoundX = 14.7f;
    public static float lowerBoundZ = -17.6f;
    public static float upperBoundZ = 28.1f;

    public static Vector3 goalPosition = Vector3.zero;

    [SerializeField]
    Transform player;

    void Start()
    {
        flockInstantiation();
    }

    void Update()
    {
        resetGoalPosition();
    }

    void flockInstantiation() {
        //  generate a random position within the map co-ords
        //  instantiate a flock member at that pos
        for (int i = 0; i < numberInFlock; i++)
        {
            Vector3 pos = new Vector3(Random.Range(lowerBoundX, upperBoundX), 1,
                                      Random.Range(lowerBoundZ, upperBoundZ));
            allFlockMembers[i] = (GameObject)Instantiate(flockPrefab, pos, Quaternion.identity);
        }
    }

    void resetGoalPosition() {
        //  every 50/10000 times
        //  generate a random position within the map co-ords
        //  set that as the goal position
        int chance = Random.Range(0, 10000);

        //  move to random direction
        if (chance < 100)
        {
            goalPosition = new Vector3(Random.Range(lowerBoundX, upperBoundX), 1,
                                      Random.Range(lowerBoundZ, upperBoundZ));
        }
        else if (chance > 9950)
        {
            goalPosition = player.position;
        }
    }


}
