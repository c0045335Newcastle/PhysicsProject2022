using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    [SerializeField]
    FlockNeighbours flockAI;


    void Start()
    {
        flockAI.createFlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
