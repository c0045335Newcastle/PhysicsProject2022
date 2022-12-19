using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOld : MonoBehaviour
{
    // Start is called before the first frame update
    //public Transform startPosition;
    public LayerMask obstacleMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float distance;

    public Node[,] grid;
    public List<Node> FinalPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeX; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool Obstacle = true;

                if (Physics.CheckSphere(worldPoint, nodeRadius, obstacleMask))
                {
                    Obstacle = false;
                }

                grid[x, y] = new Node(Obstacle, worldPoint, x, y);
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            foreach (Node node in grid)
            {
                
                Debug.Log($"node: {node}");
                Debug.Log($"nodeObs?: {node.isObstacle}");


                if (node.isObstacle)
                {
                    Gizmos.color = Color.yellow;
                }
                else
                {
                    Gizmos.color = Color.white;
                }

                if (FinalPath != null)
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawCube(node.position, Vector3.one * (nodeDiameter - distance));
            }
        }
    }


}
