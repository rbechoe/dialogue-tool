using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float speed = 5;

    void Update()
    {
        float xVal = Input.GetAxis("Horizontal");
        float zVal = Input.GetAxis("Vertical");

        transform.position += new Vector3(xVal, 0, zVal) * speed * Time.deltaTime;
    }
}
