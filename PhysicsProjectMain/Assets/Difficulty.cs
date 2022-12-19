using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Difficulty : MonoBehaviour
{
    public static Difficulty globalDifficulty = null;
    
    public Player.playerDifficulty difficulty = Player.playerDifficulty.beginner;
    GameObject[] enemies;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (globalDifficulty == null)
        {
            globalDifficulty = this;
        }
    }

    public void reduceEnemySpeed()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<NavMeshAgent>() != null)
            {
                enemy.GetComponent<NavMeshAgent>().speed = enemy.GetComponent<NavMeshAgent>().speed * 0.7f;
            }
        }
    }

    public void increaseEnemySpeed()
    {
        int score = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().score;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<NavMeshAgent>() != null)
            {

                float multiplier = (Mathf.FloorToInt(score / 5000)) * 0.2f;

                multiplier += 1;

                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().testMultiplier = multiplier;

                enemy.GetComponent<NavMeshAgent>().speed = enemy.GetComponent<NavMeshAgent>().speed * multiplier;
            }
        }
    }




    //public class YourClass : MonoBehaviour
    //{

    //    public static YourClass singleton = null;

    //    void Awake()
    //    {
    //        DontDestroyOnLoad(this.gameObject);
    //        if (singleton == null)
    //        {
    //            singleton = this;
    //        }
    //    }
    //}
}
