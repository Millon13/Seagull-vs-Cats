using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystemPrefs
{
    public static void DeliteAllSavings()
    {
        PlayerPrefs.DeleteAll();
    
    }


    public static void SetCoinsCount(float coinsCount)
    {
        PlayerPrefs.SetFloat("CoinsCount", coinsCount);
        PlayerPrefs.Save();
    }
    public static float GetCoinsCount()
    {
        if (PlayerPrefs.HasKey("CoinsCount"))
        {
            return PlayerPrefs.GetFloat("CoinsCount");
        }
        else
        {
            return PlayerPrefs.GetFloat("CoinsCount", 0);
        }
    }


    public static void SetStarsCont(float starsCount)
    {
        PlayerPrefs.SetFloat("StarsCount", starsCount);
        PlayerPrefs.Save();

    }
    public static float GetStarsCont()
    {
        if (PlayerPrefs.HasKey("StarsCount"))
        {
            return PlayerPrefs.GetFloat("StarsCount");
        }
        else
        {
            return PlayerPrefs.GetFloat("StarsCount", 0);
        }
    }





    public static void SetMusicVolume()
    {

    }
    public static void GetMusicVolume()
    {

    }


    public static void SetSoundVolume()
    {

    }
    public static void GetSoundVolume()
    {

    }


    public static void SetScreenResolution()
    {

    }
    public static void GetScreenResolution()
    {

    }
}
