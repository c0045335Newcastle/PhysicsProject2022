using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public Player player;
    
    public static GameState currentGameState;
    public GameObject[] enemies;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currentGameState = new GameState(player.transform.position, getEnemyLocations(), getHealthKitLocation(), player.health);

        if (Difficulty.globalDifficulty.difficulty == Player.playerDifficulty.beginner)
        {
            Difficulty.globalDifficulty.reduceEnemySpeed();
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateGameState();

        if (Difficulty.globalDifficulty.difficulty == Player.playerDifficulty.beginner && player.score < 10000)
        {
            //  if beginner and score is less than 10,000, don't increase the player's speed
        }
        else
        {
            Difficulty.globalDifficulty.increaseEnemySpeed();
        }
    }

    List<Vector3> getEnemyLocations() {
        List<Vector3> enemyLocations = new List<Vector3>();
        foreach (GameObject enemy in enemies)
        {
            enemyLocations.Add(enemy.transform.position);
        }
        return enemyLocations;
    }

    Vector3 getHealthKitLocation()
    {
        GameObject HK = GameObject.FindGameObjectWithTag("HealthPack");
        if (HK != null)
        {
            return HK.transform.position;
        }
        return Vector3.zero;
    }

    void updateGameState() { 
        currentGameState.playerPosition = player.transform.position;
        currentGameState.enemyPositions = getEnemyLocations();
        currentGameState.healthKitPosition = getHealthKitLocation();
        currentGameState.playerHealth = player.health;
    }
}
