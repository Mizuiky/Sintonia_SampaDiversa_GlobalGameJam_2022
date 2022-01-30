using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Image backgroundImage;

    
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void ExitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void CreditsOn()
    {
        
        Color tmp = backgroundImage.color;
        tmp.a = 0.3f;
        backgroundImage.color = tmp;

    }
    public void CreditsOff()
    {
        
        Color tmp = backgroundImage.color;
        tmp.a = 1f;
        backgroundImage.color = tmp;

    }
}
