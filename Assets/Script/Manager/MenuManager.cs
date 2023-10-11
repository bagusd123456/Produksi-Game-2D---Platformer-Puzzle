using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private SwitchCharacterHandler switchChar;
    [Header("Menu Parameter")]
    public GameObject levelSelection;
    public GameObject optionPanel;
    public GameObject credits;
    public bool play;

    [Header("Menu In Game Parameter")]
    //public GameObject inGameCanvas;
    public GameObject player;
    public GameObject pausedPanel;
    public GameObject optionPanelInGame;
    //public bool inGame;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        switchChar = GetComponent<SwitchCharacterHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        //player = GameObject.Find("Player").GetComponent<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        if(player != null)
        {
            OnPause();
        }
    }

    void OnPause()
    {
        if(isPaused)
        {
            //player.GetComponent<PlayerController>().enabled = false;
            for (int i = 0; i < switchChar.possibleCharacters.Count; i++)
            {
                if (switchChar.possibleCharacters[i] == switchChar.characterActive)
                {
                    switchChar.possibleCharacters[i].GetComponent<PlayerController>().enabled = false;
                }
            }
            Time.timeScale = 0;
        }

        else if (!isPaused)
        {
            //player.GetComponent<PlayerController>().enabled = true;
            for (int i = 0; i < switchChar.possibleCharacters.Count; i++)
            {
                if (switchChar.possibleCharacters[i] == switchChar.characterActive)
                {
                    switchChar.possibleCharacters[i].GetComponent<PlayerController>().enabled = true;
                }
            }
            Time.timeScale = 1;
        }
    }

    #region MainMenu
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LevelSelect()
    {
        if(levelSelection.activeSelf == false)
        {
            levelSelection.SetActive(true);
        }
        else
        {
            levelSelection.SetActive(false);
        }
    }

    #region ChooseLevel
    public void ChooseLevelTutorial()
    {
        SceneManager.LoadScene("Level 1 Tutorial");
    }
    public void ChooseLevel1()
    {
        SceneManager.LoadScene("Level 1 new");
    }

    public void ChooseLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void ChooseLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }
    #endregion

    public void Option()
    {
        if (optionPanel.activeSelf == false)
        {
            optionPanel.SetActive(true);
        }
        else
        {
            optionPanel.SetActive(false);
        }
    }

    public void Credits()
    {
        Time.timeScale = 1;
        if (credits.activeSelf == false)
        {
            credits.SetActive(true);
        }
        else
        {
            credits.SetActive(false);
        }
    }
    #endregion

    #region inGameMenu
    public void Pause()
    {
        if(pausedPanel.activeSelf == false)
        {
            isPaused = true;
            pausedPanel.SetActive(true);
        }
        else
        {
            isPaused = false;
            pausedPanel.SetActive(false);
        }
    }

    public void OptionInGame()
    {
        if (optionPanelInGame.activeSelf == false)
        {
            Pause();
            optionPanelInGame.SetActive(true);
            isPaused = true;
        }
        else
        {
            Pause();
            optionPanelInGame.SetActive(false);
        }
    }

    public void MainMenuInGame()
    {
        
        SceneManager.LoadScene(0);

        Debug.Log("Back To Menu");
    }

    #endregion


    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
    private void OnGUI()
    {

    }
}
