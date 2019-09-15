using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool hunter = false; // default is princes option
    
    // maybe change method name to StartGame()
    public void PlayGame()
    {
        SceneManager.LoadScene("Menu");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("Games has Exited");
        Application.Quit();
    }

    public void ChoosePrincess()
    {
        hunter = false;
    }

    public void ChooseHero()
    {
        hunter = true;
    }
}
