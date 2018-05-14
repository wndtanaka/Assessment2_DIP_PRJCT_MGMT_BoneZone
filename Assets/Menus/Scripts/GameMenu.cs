using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource hoverSFX, clickSFX;
    public GameObject gameoverMenu;
    public GameObject aimPivot;
    public GameObject winningMenu;

    private Animator anim;
    private bool isPaused = false;
    private InputController inputContoller;

    void Start()
    {
        anim = GetComponent<Animator>();
        HideCursor();
        inputContoller = GameManager.Instance.InputController;
    }

    void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        aimPivot.SetActive(true);
    }

    void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        aimPivot.SetActive(false);
    }

    void Update ()
    {
        if (inputContoller.Pause && !isPaused)
        {
            OpenPause();
            ShowCursor();
        }

        if (inputContoller.Resume && isPaused)
            Resume();

        if (inputContoller.QuitGame && isPaused)
        {
            Debug.Log("Quit");
            Quit();
        }

        if (inputContoller.QuitGame && gameoverMenu.activeSelf == true)
            Quit();

        if (inputContoller.QuitGame && winningMenu.activeSelf == true)
            Quit();
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
