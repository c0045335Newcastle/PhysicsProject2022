using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravity : MonoBehaviour
{
    [SerializeField]
    Material workingBox;
    [SerializeField]
    Material coolingBox;

    bool IsWorkingBox = true;

    [SerializeField]
    float modifiedMass;

    void Start()
    {
        activateGravityBox();
    }
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsWorkingBox)
        {
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
            if (other.tag == "Player")
            {
                //  reset mass to default
                other.gameObject.GetComponent<Rigidbody>().mass = 0.7f;
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsWorkingBox)
        {
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
            if (other.tag == "Player")
            {
                //  reset mass to default
                other.gameObject.GetComponent<Rigidbody>().mass = 1;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsWorkingBox)
        {
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        if (other.tag == "Player")
        {

            //  reset mass to default
            other.gameObject.GetComponent<Rigidbody>().mass = 1;
        }
    }

    void activateGravityBox() {
        IsWorkingBox = true;
        gameObject.GetComponent<Renderer>().material = workingBox;

        //  once activated, can be used for 10 seconds
        Invoke(nameof(deactivateGravityBox), 10f);
    }

    void deactivateGravityBox() {
        gameObject.GetComponent<Renderer>().material = coolingBox;
        IsWorkingBox = false;

        //  once deactivated, after 20 seconds, reactivated
        Invoke(nameof(activateGravityBox), 20f);
    }
}

