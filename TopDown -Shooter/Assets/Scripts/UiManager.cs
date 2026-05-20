using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
   public Button PlayBtn,SettingsBtn,ExitBtn,newGameBtn,quitGameBtn,Xbtn;

   public GameObject HomelPannel,settingsPanel;

    bool openedFromGame = false;
    void Start()
    {
        PlayBtn.onClick.AddListener(Playgame);
        SettingsBtn.onClick.AddListener(openSettingspanelfromHome);
        ExitBtn.onClick.AddListener(Exit);
        newGameBtn.onClick.AddListener(newgame);
        quitGameBtn.onClick.AddListener(QuitGame);
        Xbtn.onClick.AddListener(closeSettingsPanel);
        
        HomelPannel.SetActive(true);
        settingsPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Playgame()
    {
        HomelPannel.SetActive(false);
        Time.timeScale = 1f;

    }
    public void QuitGame()
    {
        settingsPanel.SetActive(false);
        HomelPannel.SetActive(true);
    }
    public void openSettingspanelfromHome()
    {
        openedFromGame = false;
        HomelPannel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void opensettingsFromGame()
    {
        openedFromGame = true;
        settingsPanel.SetActive(true);
        Time.timeScale = 0f;
    }


    public void closeSettingsPanel()
    {
        settingsPanel.SetActive(false);

        if (openedFromGame)
        {
            Time.timeScale = 1f;
        }
        else
        {
            HomelPannel.SetActive(true);
        }
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void newgame()
    {
        SceneManager.LoadScene(0);
    }
}
