using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using PinePie.SimpleJoystick;

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

    public Button RetryBtn2, HomeBtn1;

    public Button ContinueBtn;




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

    public GameObject BossRushWinpanel;

    public GameObject BossComingAlert;

    public GameObject ControlPanel;
    public GameObject HighestwavePanel;




    [Header("GameOver UI")]
    public TextMeshProUGUI EnemiesKilledTxt;

    public TextMeshProUGUI AccuracyTxt;

    public TextMeshProUGUI SurvivalTimeTxt;

    public TextMeshProUGUI scoreTxt;



    [Header("Challenge UI")]
    public TextMeshProUGUI challengeTimertxt;

    public TextMeshProUGUI challengeKillTxt;



    public static bool isRetry = false;

    bool openedFromGame = false;



    [Header("Stats")]
    public int enemiesKilled = 0;

    public int bulletsShot = 0;

    public int bulletsHit = 0;

    public float survivalTime = 0f;

    [Header("HighestKills")]
    int highestEnemiesKilled;
    public TextMeshProUGUI HighestKillTxt;
    public GameObject HighestKillsFrame;



    [Header("Challenge Settings")]
    public float challengeTime = 150f;

    public int challengeTargetKills = 10;

    bool challengeCompleted = false;



    [Header("References")]
    public Audiomanager audiomanager;

    public EnemyRespawn enemyRespawn;

    public PlayerHealth playerHealth ;
    public GameObject noInternetText;

    public GameObject HighestWaveFrames;

    public TextMeshProUGUI[] HighestWaves;

    [Header("Tuturial ")]
    public GameObject moveTutorial;
    public GameObject aimTutorial;
    public GameObject[] tutorials;
    public GameObject ControlTutorialPanel ;
    public JoystickController moveJoystick;
    public JoystickController aimJoystick;

    public PlayerController playerController;




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

        RetryBtn2.onClick.AddListener(Retrygame);

        HomeBtn1.onClick.AddListener(QuitGame);

        ContinueBtn.onClick.AddListener(closeHighestWaveapnel);


        
        settingsPanel.SetActive(false);

        gameoverPanel.SetActive(false);

        selectModePanel.SetActive(false);

        victoryPanel.SetActive(false);

        challengeHUD.SetActive(false);

        ChallengeAlertPanel.SetActive(false);

        bossAlertPanel.SetActive(false);

        ControlPanel.SetActive(false);

        HighestWaveFrames.SetActive(false);

        HighestKillsFrame.SetActive(false);




        ControlTutorialPanel.SetActive(false);
        moveTutorial.SetActive(false);
        aimTutorial.SetActive(false);

        
        if (isRetry)
        {
            HomelPannel.SetActive(false);


            isRetry = false;
            if(GamemodeManager.currentMode == "LASTSTAND")
            {
                HighestWaves[0].text = GameStats.GetHighestWave().ToString();
                HighestWaveFrames.SetActive(true);
                HighestKillTxt.text = getHighestKills().ToString();
                HighestKillsFrame.SetActive(true);
            }

            if(GamemodeManager.currentMode == "CHALLENGE")
            {
                StartCoroutine(StartChallengeMission());
            }

            else if(GamemodeManager.currentMode == "BOSSRUSH")
            {
                StartCoroutine(StartBossMission());
            }  
            else
            {
                hudPanel.SetActive(true);

                ControlPanel.SetActive(true);


                Time.timeScale = 1f;

                StartCoroutine(enemyRespawn.ShowWaveText());

                audiomanager.BackGroundmusicSource.Play();
            }  


        }
        else
        {

            AdManager.Instance.HideBanner();

            hudPanel.SetActive(false);

            ControlPanel.SetActive(false);

            Time.timeScale = 0f;
            
            StartControlsTutorial();
        }
    }

    public  void  HighestKills()
    {
        if (enemiesKilled > getHighestKills())
        {
            PlayerPrefs.SetInt("HighestKilles",enemiesKilled);
            PlayerPrefs.Save();
        }
    }
    public int getHighestKills()
    {
        return PlayerPrefs.GetInt("HighestKilles",0);
        
    }



    void Update()
    {
        if (Time.timeScale == 0f)
            return;



        
        survivalTime += Time.deltaTime;





        if(!(GamemodeManager.currentMode == "CHALLENGE") )
        {
            challengeKillTxt.text =
            "Kills : "
            + enemiesKilled.ToString();
        }
        
        
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
        AdManager.Instance.HideBanner();

        selectModePanel.SetActive(true);
    }



    
    public void Playgame()
    {
        AdManager.Instance.HideBanner();

        StartCoroutine(enemyRespawn.ShowWaveText());

        audiomanager.BackGroundmusicSource.Play();

        selectModePanel.SetActive(false);

        hudPanel.SetActive(true);

        ControlPanel.SetActive(true);



        Time.timeScale = 1f;

        
    }
    void StartControlsTutorial()
    {
        if (PlayerPrefs.GetInt("ControlsTutorialCompleted", 0) == 0)
           StartCoroutine(ControlTutorialRoutine());
        else
            HomelPannel.SetActive(true);

        ;
    }

    IEnumerator ControlTutorialRoutine()
    {
        Time.timeScale = 1f;
        playerController.tutorialMode = true;
        ControlTutorialPanel.SetActive(true);
        playerController.enabled = true;
        FindAnyObjectByType<PlayerShoot>().enabled = true;

        moveTutorial.SetActive(true);
        aimTutorial.SetActive(false);

        while (moveJoystick.InputDirection.magnitude < 1f)
        {
            yield return null;
        }

        tutorials[0].SetActive(false);

        while (moveJoystick.InputDirection.magnitude > 0.1f)
        {
            yield return null;
        }

        moveTutorial.SetActive(false);
        playerController.StopMovement();

        
        yield return new WaitForSeconds(0.5f);


        aimTutorial.SetActive(true);

        while (aimJoystick.InputDirection.magnitude <1f)
        {
            yield return null;
        }

        tutorials[1].SetActive(false);

        
        while (aimJoystick.InputDirection.magnitude >0.1f)
        {
            yield return null;
        }
        
        aimTutorial.SetActive(false);

        yield return new WaitForSeconds(0.5f);



        playerController.tutorialMode = false;

        PlayerPrefs.SetInt("ControlsTutorialCompleted", 1);
        PlayerPrefs.Save();

        FindAnyObjectByType<PlayerController>().enabled = false;
        FindAnyObjectByType<PlayerShoot>().enabled = false;
        Time.timeScale = 0f;
        ControlTutorialPanel.SetActive(false);

        newgame();
    }



    
    IEnumerator StartChallengeMission()
    {
        AdManager.Instance.HideBanner();

        selectModePanel.SetActive(false);

        ChallengeAlertPanel.SetActive(true);

        challengeHUD.SetActive(false);

        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(1.5f);

        ChallengeAlertPanel.SetActive(false);
        
        challengeHUD.SetActive(true);

        ControlPanel.SetActive(true);



        Playgame();
    }



    
    public void QuitGame()
    {
        isRetry = false;

        GamemodeManager.currentMode = "";
        
        AdManager.Instance.HideBanner();


        SceneManager.LoadScene(0);


    }



    
    public void openSettingspanelfromHome()
    {
        openedFromGame = false;

        HomelPannel.SetActive(false);

        settingsPanel.SetActive(true);

        hudPanel.SetActive(false);

        ControlPanel.SetActive(false);


    }



    
    public void opensettingsFromGame()
    {
        audiomanager.BackGroundmusicSource.Stop();

        openedFromGame = true;

        settingsPanel.SetActive(true);

        enemyRespawn.waveText.gameObject.SetActive(false);

        challengeHUD.SetActive(false);

        hudPanel.SetActive(false);

        ControlPanel.SetActive(false);

        AdManager.Instance.ShowBanner();



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

            ControlPanel.SetActive(true);


            AdManager.Instance.HideBanner();


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
        AdManager.Instance.HideBanner();

        selectModePanel.SetActive(false);

        bossAlertPanel.SetActive(true);

        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(1.5f);

        bossAlertPanel.SetActive(false);

        ControlPanel.SetActive(true);

        Playgame();
    }

    public IEnumerator ShowBossApproaching()
    {
        BossComingAlert.SetActive(true);

        audiomanager.playBosswarnSound();

        ControlPanel.SetActive(false);

        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(1.5f);

        BossComingAlert.SetActive(false);

        ControlPanel.SetActive(true);

        Time.timeScale = 1f;
    }



    
    void ChallengeVictory()
    {
        challengeCompleted = true;

        victoryPanel.SetActive(true);

        audiomanager.playVictorySound();

        hudPanel.SetActive(false);

        challengeHUD.SetActive(false);

        ControlPanel.SetActive(false);


        AdManager.Instance.ShowBanner();

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

        ControlPanel.SetActive(false);

        AdManager.Instance.ShowBanner();
        


        Time.timeScale = 0f;

        audiomanager.audioSource.PlayOneShot(
            audiomanager.DeathSound
        );

        audiomanager.BackGroundmusicSource.Stop();
    }

    public void showBosswinPanel()
    {
        BossRushWinpanel.SetActive(true);

        audiomanager.playVictorySound();

        hudPanel.SetActive(false);

        Time.timeScale = 0f;

        audiomanager.BackGroundmusicSource.Stop();

        ControlPanel.SetActive(false);

        AdManager.Instance.ShowBanner();



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

        BossRushWinpanel.SetActive(false);

        AdManager.Instance.HideBanner();



        audiomanager.BackGroundmusicSource.Play();

        isRetry = true;

        AdManager.Instance.ShowRetryAd();
    }



    
    public void XBtn()
    {
        HomelPannel.SetActive(true);

        selectModePanel.SetActive(false);
    }



    
    public void StartLastStand()
    {
        GamemodeManager.currentMode = "LASTSTAND";

        challengeCompleted = false;

        HighestWaves[0].text = GameStats.GetHighestWave().ToString();
        HighestWaveFrames.SetActive(true);
        HighestKillTxt.text = getHighestKills().ToString();
        HighestKillsFrame.SetActive(true);


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
        StartCoroutine(StartBossMission());
    }

    public void ContinueGame()
    {
        AdManager.Instance.ShowRewardedAd(playerHealth.RevivePlayer);
    }

    public void ShowNoInternetMessage()
    {
    StartCoroutine(NoInternetRoutine());
    }

    IEnumerator NoInternetRoutine()
    {
    noInternetText.SetActive(true);

    yield return new WaitForSecondsRealtime(2f);

    noInternetText.SetActive(false);
    }
    public void ShowHighestWavePanel()
    {
        HighestWaves[1].text = GameStats.GetHighestWave().ToString();
        HighestwavePanel.SetActive(true);
    }
    public void closeHighestWaveapnel()
    {
        HighestwavePanel.SetActive(false);
    }
}