using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MenuController : MonoBehaviour
{
    public GameObject CameraCutscene;
    [SerializeField] GameObject menu;

    public GameObject[] MenuPanels;

    public static MenuController menuController;
    public bool skippable = false;

    public GameObject Player;

    private void Start()
    {
        menuController = this;
    }

    //----MAIN MENU----------------------------------------------------------------------------------------------------------------------------
    public void _StartButton()
    {
        CameraCutscene.GetComponent<PlayableDirector>().Play();
        CutSceneController.controller.skipLine.SetActive(true);
        skippable = true;
        menu.SetActive(false);
        GameController.instance.hidemouse();
    }

    public void _SettingsButton()
    {
        MenuPanels[0].SetActive(true);
    }

    public void _ExitButton()
    {

    }

    //----SETTINGS PANEL----------------------------------------------------------------------------------------------------------------------------

    public void _CloseButton()
    {
        foreach (GameObject panel in MenuPanels)
        {
            panel.SetActive(false);
            if (menu.activeInHierarchy == false)
            {
                GameController.instance.state = GameState.play;
                GameController.instance.hidemouse();
            }
        }
    }
}
