using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_script : MonoBehaviour
{
    //Remove when done
    /// <summary>
    [SerializeField]
    Text diffTest;
    /// </summary>


    [SerializeField]
    Text scoreTextWhite;
    [SerializeField]
    Text scoreTextBlack;

    [SerializeField]
    Text healthTextWhite;
    [SerializeField]
    Text healthTextBlack;

    [SerializeField]
    Text sensTextWhite;


    [SerializeField]
    Player player;

    string score = "";
    string health = "";
    string sens = "";


    // Update is called once per frame
    void Update()
    {
        diffTest.text = ($"Difficulty: {player.difficulty}");


        score = player.getScore().ToString();
        //Debug.Log($"Score: {score}");

        scoreTextBlack.text = ($"SCORE: {score}");
        scoreTextWhite.text = ($"SCORE: {score}");

        health = player.getHealth().ToString();

        healthTextBlack.text = ($"HEALTH: {health}");
        healthTextWhite.text = ($"HEALTH: {health}");

        sens = player.playerSensitivity.ToString();

        sensTextWhite.text = ($"SENS: {sens}");
    }

}
