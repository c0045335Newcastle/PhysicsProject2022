using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextScript : MonoBehaviour
{
    public float textDestroyTime = 2.5f;
    new GameObject camera;

    Vector3 textFall = new Vector3(0f, 0.1f, 0f);
    private void Start()
    {
        Destroy(gameObject, textDestroyTime);
        camera = (GameObject.FindGameObjectsWithTag("MainCamera"))[0];
    }
    private void Update()
    {
        //  Text faces camera
        transform.LookAt(camera.transform);
        transform.rotation = Quaternion.LookRotation(camera.transform.forward);
    }
    private void FixedUpdate()
    {
        //  Text gradually falls
        transform.position = transform.position - textFall;
    }
}
