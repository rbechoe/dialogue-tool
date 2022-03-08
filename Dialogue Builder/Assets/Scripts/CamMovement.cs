using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float slow = 25;

    public Vector3 startPos;
    public Vector3 endPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            startPos = new Vector3(Input.mousePosition.x, transform.position.y, Input.mousePosition.y);
        }

        if (Input.GetMouseButton(1))
        {
            endPos = new Vector3(Input.mousePosition.x, transform.position.y, Input.mousePosition.y);

            transform.position += (startPos - endPos) / slow * Time.deltaTime;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y > 10)
        {
            transform.position -= Vector3.up;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.y < 30)
        {
            transform.position += Vector3.up;
        }
    }
}
