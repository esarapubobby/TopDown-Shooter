using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour
{
   public Button PlayBtn,SettingsBtn,ExitBtn;
   public Button newGameBtn,quitGameBtn,Xbtn;

   public Button RetryBtn,QuitBtn;

   public GameObject HomelPannel,settingsPanel,gameoverPanel,hudPanel;

    public TextMeshProUGUI EnemiesKilledTxt,AccuracyTxt,SurvivalTimeTxt,scoreTxt;
   public static bool isRetry = false;
    bool openedFromGame = false;

    public int enemiesKilled = 0;

    public int bulletsShot = 0;
    public int bulletsHit = 0;
    public float survivalTime = 0f;

    public Audiomanager audiomanager;
    public EnemyRespawn enemyRespawn;
    void Start()
    {
        PlayBtn.onClick.AddListener(Playgame);
        SettingsBtn.onClick.AddListener(openSettingspanelfromHome);
        ExitBtn.onClick.AddListener(Exit);
        newGameBtn.onClick.AddListener(newgame);
        quitGameBtn.onClick.AddListener(QuitGame);
        Xbtn.onClick.AddListener(closeSettingsPanel);
        RetryBtn.onClick.AddListener(Retrygame);
        QuitBtn.onClick.AddListener(QuitGame);
        
        settingsPanel.SetActive(false);
        gameoverPanel.SetActive(false);

        if (isRetry)
        {
            HomelPannel.SetActive(false);

            hudPanel.SetActive(true);

            Time.timeScale = 1f;
    
            StartCoroutine(enemyRespawn.ShowWaveText());
            
            audiomanager.BackGroundmusicSource.Play();



            isRetry = false;
        }
        else
        {
            HomelPannel.SetActive(true);

            hudPanel.SetActive(false);

            Time.timeScale = 0f;
        }
    }
    void Update()
    {
        survivalTime += Time.deltaTime;
    }

    public void Playgame()
    {
        StartCoroutine(enemyRespawn.ShowWaveText());
        audiomanager.BackGroundmusicSource.Play();
        HomelPannel.SetActive(false);
        hudPanel.SetActive(true);
        Time.timeScale = 1f;

    }
    public void QuitGame()
    {
        isRetry = false;
        SceneManager.LoadScene(0);
        Time.timeScale = 0f;
    }
    public void openSettingspanelfromHome()
    {
        openedFromGame = false;
        HomelPannel.SetActive(false);
        settingsPanel.SetActive(true);
        hudPanel.SetActive(false);
    }
    public void opensettingsFromGame()
    {
        audiomanager.BackGroundmusicSource.Stop();
        openedFromGame = true;
        settingsPanel.SetActive(true);
        hudPanel.SetActive(false);
        Time.timeScale = 0f;
    }


    public void closeSettingsPanel()
    {
        settingsPanel.SetActive(false);

        if (openedFromGame)
        {
            audiomanager.BackGroundmusicSource.Play();
            Time.timeScale = 1f;
            hudPanel.SetActive(true);
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

    public void  Retrygame()
    {
        audiomanager.BackGroundmusicSource.Play();
        isRetry =true;
        SceneManager.LoadScene(0);

    }
}
