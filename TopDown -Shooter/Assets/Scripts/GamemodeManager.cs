using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamemodeManager : MonoBehaviour
{
 public static string currentMode = "";

    public void SetLastStandMode()
    {
        currentMode = "LASTSTAND";
    }

    public void SetChallengeMode()
    {
        currentMode = "CHALLENGE";
    }

    public void SetBossRushMode()
    {
        currentMode = "BOSSRUSH";
    }
}
