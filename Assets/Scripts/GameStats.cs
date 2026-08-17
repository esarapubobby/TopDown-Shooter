using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStats 
{
    public static int GetHighestWave()
    {
        return PlayerPrefs.GetInt("HighestWave", 0);
    }

    public static void SaveHighestWave(int wave)
    {
        int currentHighest = GetHighestWave();

        if (wave > currentHighest)
        {
            PlayerPrefs.SetInt("HighestWave", wave-1);
            PlayerPrefs.Save();
        }
    }
}
