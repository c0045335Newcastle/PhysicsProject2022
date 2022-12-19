using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outOfBoundsScript : MonoBehaviour
{
    [SerializeField]
    Material transparentMat;
    [SerializeField]
    Player player;

    //  if an enemy falls out of bound for whatever reason
    //  they will respawn at this object!
    [SerializeField]
    GameObject returnObject;
    
    void Start()
    {
        this.gameObject.GetComponent<Renderer>().material = transparentMat;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Out of Bounds: DEAD");
            player.killPlayer();
        }
        collision.gameObject.transform.position = returnObject.transform.position;
    }
}
