using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource hoverSFX, clickSFX;

    private Animator anim;
    private bool isPaused = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        HideCursor();
    }

    void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            OpenPause();
            ShowCursor();
        }

        if (Input.GetKeyDown(KeyCode.R) && isPaused)
            Resume();

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Debug.Log("Quit");
            Quit();
        }
    }

    #region Pause Menu

    IEnumerator PauseCountdown()
    {
        yield return new WaitForSeconds(0.15f);
        Time.timeScale = 0;
    }

    public void OpenPause()
    {
        playClickSound();
        anim.Play("PauseIn");
        ShowCursor();
        StartCoroutine("PauseCountdown");
        isPaused = true;
        Debug.Log("Is Paused");
    }
    
    public void Resume()
    {
        Time.timeScale = 1;
        playClickSound();
        anim.Play("PauseOut");
        HideCursor();
        isPaused = false;
        Debug.Log("Is not Paused");
    }

    public void Restart()
    {

    }

    public void Quit()
    {
        Debug.Log("Quit");
        Time.timeScale = 1;
        playClickSound();
        SceneManager.LoadScene("MainMenu");
    }
    #endregion

    #region Sounds
    public void playHoverClip()
    {
        hoverSFX.Play();
    }

    public void playClickSound()
    {
        clickSFX.Play();
    }

    #endregion
}
