using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject healthPackItem;

    Player.playerDifficulty difficulty;

    //  set area that health pack spawns
    public float lowerBoundX;
    public float upperBoundX;
    public float lowerBoundZ;
    public float upperBoundZ;

    //  value to countdown from
    public float countdownTimerDefault = 20f;
    //  value holding timer
    //  > set to TRUE
    public bool respawnPack = true;


    void Start()
    {
        difficulty = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().difficulty;
    }

    void Update()
    {
        if (respawnPack == true)
        {
            spawnHealth();
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            //  player shouts that they see a health pack!
            player.seeHealthPack();
        }

    }

    void spawnHealth()
    {
        // set destroy timer
        respawnPack = false;

        if (difficulty == Player.playerDifficulty.beginner)
        {
            for (int i = 0; i < 2; i++)
            {
                var pack = Instantiate(healthPackItem, getSpawnPos(), Quaternion.identity);
            }
        }
        else if (difficulty == Player.playerDifficulty.regular)
        {
            var pack = Instantiate(healthPackItem, getSpawnPos(), Quaternion.identity);
        }
        else if(difficulty == Player.playerDifficulty.hardened)
        {
            //  Spawn random number of healthpacks between 1-3
            for (int i = 0; i < (Random.Range(1,4)); i++)
            {
                var pack = Instantiate(healthPackItem, getSpawnPos(), Quaternion.identity);
            }
        }
        Invoke(nameof(readyToRespawn), countdownTimerDefault);
    }

    void readyToRespawn()
    {
        respawnPack = true;
    }

    //  function for readability
    Vector3 getSpawnPos()
    {
        Vector3 newPos = new Vector3(Random.Range(lowerBoundX, upperBoundX), 1, Random.Range(lowerBoundZ, upperBoundZ));
        return newPos;
    }
}
