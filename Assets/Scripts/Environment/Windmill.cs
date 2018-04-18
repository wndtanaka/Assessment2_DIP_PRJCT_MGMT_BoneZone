using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour
{
    [SerializeField]
    private float xRotationPerMinute;
    [SerializeField]
    private float yRotationPerMinute;
    [SerializeField]
    private float zRotationPerMinute;

    private void Update()
    {
        float xDegreesPerFrame = Time.deltaTime / 60 * 360 * xRotationPerMinute;
        transform.RotateAround(transform.position, transform.right, xDegreesPerFrame);

        float yDegreesPerFrame = Time.deltaTime / 60 * 360 * yRotationPerMinute;
        transform.RotateAround(transform.position, transform.right, yDegreesPerFrame);

        float zDegreesPerFrame = Time.deltaTime / 60 * 360 * zRotationPerMinute;
        transform.RotateAround(transform.position, transform.right, zDegreesPerFrame);

    }
}
