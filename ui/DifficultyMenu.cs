using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenu : MonoBehaviour
{
    void Start()
    {
        //Scene game = SceneManager.GetActiveScene();

        //if (game.name == "Level1")
        //{
        //    SceneManager.UnloadSceneAsync(1);

        //}

    }

    //  layer 13 = healthpack
    //  layer 8  = enemy
    //  IgnoreLayerCollision(int layer1, int layer2, bool ignore = true);

    public void Beginner()
    {
        stopEnemyHealthPackCollision(true);
        Difficulty.globalDifficulty.difficulty = Player.playerDifficulty.beginner;
        PlayGame();
    }

    public void Regular()
    {
        stopEnemyHealthPackCollision(true);
        Difficulty.globalDifficulty.difficulty = Player.playerDifficulty.regular;
        PlayGame();
    }

    public void Hardened()
    {
        stopEnemyHealthPackCollision(false);
        Difficulty.globalDifficulty.difficulty = Player.playerDifficulty.hardened;
        PlayGame();
    }

    
    void PlayGame() {
        SceneManager.LoadScene(2);
    }

    //  stop regular enemies from stealings healthpacks
    //  only the orange (stealer) enemy can steal them
    void stopEnemyHealthPackCollision(bool stopCollision) {
        Physics.IgnoreLayerCollision(8, 13, stopCollision);
    }

}
