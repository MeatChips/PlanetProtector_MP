using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(2 * Time.deltaTime, 2 * Time.deltaTime, 2 * Time.deltaTime);
    }
}
