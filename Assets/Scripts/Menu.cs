using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickPlay()
    {
        SceneManager.LoadScene(10);
    }
    public void OnClickOption()
    {
        SceneManager.LoadScene(12);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }

    
}
