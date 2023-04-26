using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehObstaclesController : MonoBehaviour
{
    private float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        // We move the vehicle forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
