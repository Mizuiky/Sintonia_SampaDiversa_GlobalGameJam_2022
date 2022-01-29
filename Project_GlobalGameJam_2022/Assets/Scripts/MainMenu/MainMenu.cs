using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void ExitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
