using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private Animator anim;
    private int scrW, scrH;
    private bool isFullScreen;
    
    public string newGameSceneName;
    public AudioSource hoverSFX, clickSFX;
    public GameObject mainMenuPanel;
    public GameObject mainOptionsPanel;
    public GameObject controlsPanel;
    public GameObject characterPanel;
    public Text character1, character2;
    public Text fullScreenText;
    public Light directLight;
    public Slider brightnessSlider;
    public Slider soundSlider, musicSlider;
    public AudioSource music;

    void Start ()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        anim = GetComponent<Animator>();

        if (Screen.fullScreen)
        {
            isFullScreen = true;
            fullScreenText.text = "ON"; 
        }
        else
        {
            isFullScreen = false;
            fullScreenText.text = "OFF";
        }

        brightnessSlider.value = directLight.intensity; 
        musicSlider.value = music.volume;
        soundSlider.value = AudioListener.volume; 
    }

    #region Open Different panels

    public void OpenOptions()
    {
        //mainOptionsPanel.SetActive(true);
        anim.Play("OptionsIn");
        //mainMenuPanel.SetActive(false);
        playClickSound();
        
    }
    
    public void OpenControls()
    {
        //controlsPanel.SetActive(true);
        anim.Play("ControlsIn");
        //mainMenuPanel.SetActive(false);
        playClickSound();

    }

    public void OpenCharacters()
    {
        anim.Play("CharactersIn");
        playClickSound();
    }

    public void Versus()
    {

    }

    public void NewGame()
    {
        if (Application.CanStreamedLevelBeLoaded("Level_Artist") && character1.text == "Runner")
            SceneManager.LoadScene("Level_Artist");

        if (Application.CanStreamedLevelBeLoaded("Level_Programmer") && character2.text == "Runner")
            SceneManager.LoadScene("Level_Programmer");
    }

    public void CharacterSelect1()
    {
        character1.text = "Runner";
        character2.text = "Chaser";
    }

    public void CharacterSelect2()
    {
        character2.text = "Runner";
        character1.text = "Chaser";
    }

    #endregion

    #region Back Buttons

    public void Back_Options()
    {
        //mainMenuPanel.SetActive(true);
        anim.Play("OptionsOut");
        //mainOptionsPanel.SetActive(false);
        playClickSound();
    }

    public void Back_Controls()
    {
        anim.Play("ControlsOut");
        //controlsPanel.SetActive(false);
        //mainMenuPanel.SetActive(true);
        playClickSound();
    }

    public void Back_Characters()
    {
        anim.Play("CharactersOut");
        playClickSound();
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region Options Buttons

    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;

        if (Screen.fullScreen)
        {
            fullScreenText.text = "ON";
            isFullScreen = true;
        }
        else
        {
            fullScreenText.text = "OFF";
            isFullScreen = false;
        }
    }

    public void Resolution1280()
    {
        scrW = 1280;
        scrH = 720;
        Screen.SetResolution(scrW, scrH, isFullScreen);
    }

    public void Resolution1366()
    {
        scrW = 1366;
        scrH = 768;
        Screen.SetResolution(scrW, scrH, isFullScreen);
    }

    public void Resolution1600()
    {
        scrW = 1600;
        scrH = 900;
        Screen.SetResolution(scrW, scrH, isFullScreen);
    }

    public void Resolution1920()
    {
        scrW = 1920;
        scrH = 1080;
        Screen.SetResolution(scrW, scrH, isFullScreen);
    }

    public void Brightness()
    {
        if (directLight != null && brightnessSlider != null)
        {
            directLight.intensity = brightnessSlider.value;
        }

    }

    public void Difficulties()
    {
        
    }

    public void MusicVolume()
    {
        if (music != null)
            music.volume = musicSlider.value; 
    }

    public void SoundVolume()
    {
        AudioListener.volume = soundSlider.value;
    }


    #endregion

    #region Sounds
    public void playHoverClip()
    {
        hoverSFX.Play(); 
    }

    void playClickSound()
    {
        clickSFX.Play(); 
    }

    #endregion
    
}
