using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public void Despawn(GameObject go, float inSeconds)
    {
        go.SetActive(false);
        // after inSeconds, gameObject SetActive set to true true to spawn back the gameObject using delegate method   
        GameManager.Instance.Timer.Add(() => { go.SetActive(true); }, inSeconds);
    }
}
