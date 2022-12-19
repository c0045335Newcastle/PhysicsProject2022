using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetSensitivity : MonoBehaviour
{
    public Player player;
    public int sens = 350;

    public CinemachineFreeLook freeCamera;

    // Start is called before the first frame update
    void Start()
    {
        freeCamera.m_XAxis.m_MaxSpeed = sens;
        sens = 350;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerSensitivity < 0)
            player.playerSensitivity = 1;
        else if(player.playerSensitivity > 2000)
            player.playerSensitivity = 2000;





        sens = player.playerSensitivity;
        freeCamera.m_XAxis.m_MaxSpeed = sens;

        
    }
}
