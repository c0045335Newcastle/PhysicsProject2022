using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakspot : MonoBehaviour
{

    void Start()
    {
        //Debug.Log("weakalive");
    }

    // Update is called once per frame
    void Update()
    {

    
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Critical Hit!");
            damagePlayer();
        }
    }

    void damagePlayer() { 
        Player player = this.transform.parent.GetComponent<Player>();

        //  critically damage the player
        player.reduceRndHealth(45, 75);

        //  excess spaces push the text to the side
        Vector3 textHeight = new Vector3(0, 2, 0);
        player.spawnText(player.gameObject, textHeight, "Critical Hit!\n\n", Color.magenta);

    }
}
