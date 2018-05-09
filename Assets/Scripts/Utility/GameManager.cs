using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void OnLocalPlayerJoined(Player player); // event system when player spawned / joined
    public event OnLocalPlayerJoined onLocalPlayerJoined;

    public static GameManager Instance;
    //{
    //    get
    //    {
    //        // creating GameManager when the game started
    //        if (m_Instance == null)
    //        {
    //            m_Instance = new GameManager();
    //            m_Instance.gameObject = new GameObject("GameManager"); // create GameManager gameObject
    //            m_Instance.gameObject.AddComponent<InputController>(); // adding InputController script to the GameManager
    //            m_Instance.gameObject.AddComponent<Timer>(); // adding Timer script to the GameManager
    //            m_Instance.gameObject.AddComponent<Respawner>(); // adding Respawner script to the GameManager
    //        }
    //        return m_Instance;
    //    }
    //}

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GameManager in scene.");
        }
        else
        {
            Instance = this;
        }
    }

    private InputController m_InputController;
    public InputController InputController
    {
        // getting InputController script component 
        get
        {
            if (m_InputController == null)
            {
                m_InputController = gameObject.GetComponent<InputController>();
            }
            return m_InputController;
        }
    }

    private Timer m_Timer;
    public Timer Timer
    {
        // getting Timer script component
        get
        {
            if (m_Timer == null)
            {
                m_Timer = gameObject.GetComponent<Timer>();
            }
            return m_Timer;
        }
    }

    private Respawner m_Respawner;
    public Respawner Respawner
    {
        // getting Respawner script component
        get
        {
            if (m_Respawner == null)
            {
                m_Respawner = gameObject.GetComponent<Respawner>();
            }
            return m_Respawner;
        }
    }

    // setting LocalPlayer upon start using delegate
    private Player m_LocalPlayer;
    public Player LocalPlayer
    {
        get
        {
            return m_LocalPlayer;
        }
        set
        {
            m_LocalPlayer = value;
            if (onLocalPlayerJoined != null)
            {
                onLocalPlayerJoined(m_LocalPlayer);
            }
        }
    }
}
