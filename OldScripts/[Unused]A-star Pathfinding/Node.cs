using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeOld
{
    //  positions within the node array
    public int gridX;
    public int gridY;

    //  is the node being obstructed?
    public bool isObstacle;

    //  position of the singular node
    public Vector3 position;

    //  previous node
    public Node Parent;

    //  cost of going to the next square
    public int gCost;
    //  distance from the target (player)
    public int hCost;

    //  h+g Cost
    public int FCost { get { return gCost + hCost; } }

    public NodeOld(bool a_isObstacle, Vector3 a_Position, int a_gridX, int a_gridY) {

        isObstacle = a_isObstacle;
        position = a_Position;
        gridX = a_gridX;
        gridY = a_gridY;

    }
}
