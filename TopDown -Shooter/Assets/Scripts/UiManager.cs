using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button PlayBtn, SettingsBtn, ExitBtn;

    public Button newGameBtn, quitGameBtn, Xbtn;

    public Button RetryBtn, QuitBtn;

    public Button LaststandBtn,
                  challengeBtn,
                  BossRushbtn,
                  Xbtn1;

    public Button RetryBtn1, HomeBtn;



    [Header("Panels")]
    public GameObject HomelPannel;

    public GameObject settingsPanel;

    public GameObject gameoverPanel;

    public GameObject hudPanel;

    public GameObject selectModePanel;

    public GameObject victoryPanel;

    public GameObject challengeHUD;

    public GameObject ChallengeAlertPanel;

    public GameObject bossAlertPanel;



    [Header("GameOver UI")]
    public TextMeshProUGUI EnemiesKilledTxt;

    public TextMeshProUGUI AccuracyTxt;

    public TextMeshProUGUI SurvivalTimeTxt;

    public TextMeshProUGUI scoreTxt;



    [Header("Challenge UI")]
    public TextMeshProUGUI challengeTimertxt;

    public TextMeshProUGUI challengeKillTxt;



    [Header("Victory Panel UI")]
    public TextMeshProUGUI victoryKillsTxt;

    public TextMeshProUGUI victoryTimeTxt;



    public static bool isRetry = false;

    bool openedFromGame = false;



    [Header("Stats")]
    public int enemiesKilled = 0;

    public int bulletsShot = 0;

    public int bulletsHit = 0;

    public float survivalTime = 0f;



    [Header("Challenge Settings")]
    public float challengeTime = 150f;

    public int challengeTargetKills = 10;

    bool challengeCompleted = false;



    [Header("References")]
    public Audiomanager audiomanager;

    public EnemyRespawn enemyRespawn;

    public PlayerHealth playerHealth ;



    void Start()
    {
        
        PlayBtn.onClick.AddListener(playBtn);

        SettingsBtn.onClick.AddListener(openSettingspanelfromHome);

        ExitBtn.onClick.AddListener(Exit);

        newGameBtn.onClick.AddListener(newgame);

        quitGameBtn.onClick.AddListener(QuitGame);

        Xbtn.onClick.AddListener(closeSettingsPanel);

        RetryBtn.onClick.AddListener(Retrygame);

        QuitBtn.onClick.AddListener(QuitGame);

        LaststandBtn.onClick.AddListener(StartLastStand);

        challengeBtn.onClick.AddListener(StartChallenge);

        BossRushbtn.onClick.AddListener(StartBossRush);

        Xbtn1.onClick.AddListener(XBtn);

        RetryBtn1.onClick.AddListener(Retrygame);

        HomeBtn.onClick.AddListener(QuitGame);



        
        settingsPanel.SetActive(false);

        gameoverPanel.SetActive(false);

        selectModePanel.SetActive(false);

        victoryPanel.SetActive(false);

        challengeHUD.SetActive(false);

        ChallengeAlertPanel.SetActive(false);

        bossAlertPanel.SetActive(false);



        
        if (isRetry)
        {
            HomelPannel.SetActive(false);

            hudPanel.SetActive(true);

            Time.timeScale = 1f;

            StartCoroutine(enemyRespawn.ShowWaveText());

            audiomanager.BackGroundmusicSource.Play();

            if(GamemodeManager.currentMode == "CHALLENGE")
            {
                challengeHUD.SetActive(true);
            }

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
        if (Time.timeScale == 0f)
            return;



        
        survivalTime += Time.deltaTime;



        
        if (GamemodeManager.currentMode == "CHALLENGE"
            && !challengeCompleted)
        {
            challengeTime -= Time.deltaTime;

            challengeTime =
            Mathf.Clamp(challengeTime, 0, 150f);



            int minutes =
            Mathf.FloorToInt(challengeTime / 60);

            int seconds =
            Mathf.FloorToInt(challengeTime % 60);



            challengeTimertxt.text =
            minutes.ToString("00")
            + ":"
            + seconds.ToString("00");



            challengeKillTxt.text =
            "Kills : "
            + enemiesKilled.ToString()
            + " / "
            + challengeTargetKills.ToString();



            
            if(enemiesKilled >= challengeTargetKills)
            {
                ChallengeVictory();
            }



            
            if(challengeTime <= 0 &&
               enemiesKilled < challengeTargetKills)
            {
                ChallengeFailed();
            }
        }
    }



    
    public void playBtn()
    {
        HomelPannel.SetActive(false);

        selectModePanel.SetActive(true);
    }



    
    public void Playgame()
    {
        StartCoroutine(enemyRespawn.ShowWaveText());

        audiomanager.BackGroundmusicSource.Play();

        selectModePanel.SetActive(false);

        hudPanel.SetActive(true);

        Time.timeScale = 1f;
    }



    
    IEnumerator StartChallengeMission()
    {
        selectModePanel.SetActive(false);

        ChallengeAlertPanel.SetActive(true);

        challengeHUD.SetActive(false);

        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(3f);

        ChallengeAlertPanel.SetActive(false);
        
        challengeHUD.SetActive(true);


        Playgame();
    }



    
    public void QuitGame()
    {
        isRetry = false;

        GamemodeManager.currentMode = "";

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

        challengeHUD.SetActive(false);

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

            if(GamemodeManager.currentMode == "CHALLENGE")
            {
                challengeHUD.SetActive(true);
            }
            else
            {
                challengeHUD.SetActive(false);
            }
        }
        else
        {
            HomelPannel.SetActive(true);
        }
    }

    IEnumerator StartBossMission()
    {
        selectModePanel.SetActive(false);

        bossAlertPanel.SetActive(true);

        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(3f);

        bossAlertPanel.SetActive(false);

        Playgame();
    }



    
    public void ChallengeVictory()
    {
        challengeCompleted = true;

        victoryKillsTxt.text =
        enemiesKilled.ToString();



        int minutes =
        Mathf.FloorToInt(survivalTime / 60);

        int seconds =
        Mathf.FloorToInt(survivalTime % 60);



        victoryTimeTxt.text =
        minutes.ToString("00")
        + ":"
        + seconds.ToString("00");



        victoryPanel.SetActive(true);

        audiomanager.playVictorySound();

        hudPanel.SetActive(false);

        challengeHUD.SetActive(false);

        Time.timeScale = 0f;

        audiomanager.BackGroundmusicSource.Stop();
    }



    
    void ChallengeFailed()
    {
        challengeCompleted = true;

        gameoverPanel.SetActive(true);

         
        playerHealth.gameOverdetails();

        challengeHUD.SetActive(false);

        hudPanel.SetActive(false);

        Time.timeScale = 0f;

        audiomanager.audioSource.PlayOneShot(
            audiomanager.DeathSound
        );

        audiomanager.BackGroundmusicSource.Stop();
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



    
    public void Retrygame()
    {
        challengeCompleted = false;

        challengeTime = 150f;

        enemiesKilled = 0;

        survivalTime = 0f;

        gameoverPanel.SetActive(false);

        victoryPanel.SetActive(false);

        audiomanager.BackGroundmusicSource.Play();

        isRetry = true;

        SceneManager.LoadScene(0);
    }



    
    public void XBtn()
    {
        HomelPannel.SetActive(true);

        selectModePanel.SetActive(false);
    }



    
    public void StartLastStand()
    {
        GamemodeManager.currentMode = "LASTSTAND";

        challengeHUD.SetActive(false);

        challengeCompleted = false;

        Playgame();
    }



    
    public void StartChallenge()
    {
        GamemodeManager.currentMode = "CHALLENGE";

        challengeHUD.SetActive(true);

        challengeCompleted = false;

        challengeTime = 150f;

        enemiesKilled = 0;

        survivalTime = 0f;

        StartCoroutine(StartChallengeMission());
    }



    
    public void StartBossRush()
    {
        GamemodeManager.currentMode = "BOSSRUSH";

        challengeHUD.SetActive(false);

        StartCoroutine(StartBossMission());
    }
}