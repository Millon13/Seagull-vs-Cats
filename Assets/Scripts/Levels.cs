using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Levels : Sound
{
    private void Start()
    {
        PlaySound(0);
    }
    public void OnClickFirst()
    {
        SceneManager.LoadScene(11);
    }
    public void OnClickSecond()
    {
        SceneManager.LoadScene(2);
    }
    public void OnClickThriert()
    {
        SceneManager.LoadScene(3);
    }
    public void OnClickForth()
    {
        SceneManager.LoadScene(4);
    }
    public void OnClickFifth()
    {
        SceneManager.LoadScene(5);
    }
    public void OnClickSixth()
    {
        SceneManager.LoadScene(6);
    }
    public void OnClickSeventh()
    {
        SceneManager.LoadScene(7);
    }
    public void OnClickEigth()
    {
        SceneManager.LoadScene(8);
    }
    public void OnClickNineth()
    {
        SceneManager.LoadScene(9);
    }

}
