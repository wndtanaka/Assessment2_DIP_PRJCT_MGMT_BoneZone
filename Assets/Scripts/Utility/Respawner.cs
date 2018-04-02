using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    // use this function if you want to respawn the object
    public void Respawn(GameObject go, float inSeconds)
    {
        go.SetActive(false);
        // after inSeconds, gameObject SetActive set to true true to spawn back the gameObject using delegate method   
        GameManager.Instance.Timer.Add(() => { go.SetActive(true); }, inSeconds);
    }
    // use this function if you want to despawn the object
    public void Despawn(GameObject go, float inSeconds = 0)
    {
        Destroy(go,inSeconds);
    }
}
