using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float _cameraSpeed = 1f;
    public float _board = 5f;
    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.y >= Screen.height - _board)
        {
            transform.Translate(Vector3.right * _cameraSpeed * Time.deltaTime, Space.World);
        }

        if (Input.mousePosition.y <= _board)
        {
            transform.Translate(Vector3.left * _cameraSpeed * Time.deltaTime, Space.World);
        }

        if (Input.mousePosition.x >= Screen.width - _board)
        {
            transform.Translate(Vector3.back * _cameraSpeed * Time.deltaTime, Space.World);
        }

        if (Input.mousePosition.x <= _board)
        {
            transform.Translate(Vector3.forward * _cameraSpeed * Time.deltaTime, Space.World);
        }
    }
}
