using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsStars : MonoBehaviour
{
    public float level;
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneIndex = currentScene.buildIndex;
        if (sceneIndex>=1)
        {
            level = sceneIndex;
        }
    }

    
    
}
