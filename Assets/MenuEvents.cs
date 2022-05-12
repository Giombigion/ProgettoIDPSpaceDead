using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEvents : MonoBehaviour
{

    
    public void _onPlay(string scenename) {

        print("inizio gioco");
        SceneManager.LoadScene(scenename);
    }

    public void _onOptions()
    {
        print("opzioni");

    }

    public void _onCredits()
    {
        print("credits");

    }

    public void _onExit()
    {
        Application.Quit();

    }

    public void Back(string scenename) {
        SceneManager.LoadScene(scenename);
    }
}
