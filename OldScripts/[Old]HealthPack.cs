using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackOld : MonoBehaviour
{
    [SerializeField]
    GameObject healthPackItem;

    //  set area that health pack spawns
    public float spawnX1;
    public float spawnX2;
    public float spawnZ1;
    public float spawnZ2;

    Rigidbody pack;

    //  value to countdown from
    public float countdownTimerDefault = 20f;
    //  value holding timer
    //  > set to TRUE
    public bool respawnPack = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (respawnPack == true)
        {
            spawnHealth();
        }
        
    }

    void spawnHealth() {
        // set destroy timer
        respawnPack = false;


        pack = Instantiate(healthPackItem, getSpawnPos(), Quaternion.identity).GetComponent<Rigidbody>();

        pack.gameObject.tag = "HealthPack";
        Invoke(nameof(readyToRespawn), countdownTimerDefault);


    }

    void readyToRespawn()
    {
        respawnPack = true;
    }

    //  function for readability
    Vector3 getSpawnPos() {
        Vector3 newPos = new Vector3(Random.Range(spawnX1, spawnX2), 3,Random.Range(spawnZ1, spawnZ2));
        return newPos;
    }
}
