using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Daycycle : MonoBehaviour
{
    public float minutesPerSecond = 5f;

    Quaternion startRotation;

    // Use this for initialization
    void Start()
    {
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        float angleThisFrame = Time.deltaTime * minutesPerSecond;
        transform.RotateAround(transform.position, Vector3.forward, angleThisFrame);

        if (DayNightController.currentTime >= 8 && DayNightController.currentTime <= 17)
        {
            Debug.Log("Die");
            SceneManager.LoadScene("GameOver");
        }
    }
}
