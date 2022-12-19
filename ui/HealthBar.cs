using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;


    private void Awake()
    {

        slider.value = 100;
        //Debug.Log($"health bar awake");
    }

    public void setHealth(int health)
    {
        //Debug.Log($"OLD BAR: {slider.value}");
        slider.value = health;
        //Debug.Log($"NEW BAR: {slider.value}");
    }


}
