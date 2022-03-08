using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float slow = 25;
    public float scrollSpeed = 10;
    public int minHeight = 10;
    public int maxHeight = 30;

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

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            transform.position -= Vector3.up * Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minHeight, maxHeight), transform.position.z);
        }
    }
}
