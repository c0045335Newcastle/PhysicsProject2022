using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    
    public void Replay()
    {
        SceneManager.LoadScene(1);

    }

    public void Menu() {
        SceneManager.LoadScene(0);
    }
}
