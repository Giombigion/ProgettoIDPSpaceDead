using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MenuController : MonoBehaviour
{
    public GameObject CameraCutscene;
    [SerializeField] GameObject menu;
    public static MenuController menuController;
    public bool skippable = false;

    private void Start()
    {
        menuController = this;
    }

    public void _StartButton()
    {
        CameraCutscene.GetComponent<PlayableDirector>().Play();
        CutSceneController.controller.skipLine.SetActive(true);
        skippable = true;
        menu.SetActive(false);
    }

    public void _SettingsButton()
    {

    }

    public void _ExitButton()
    {

    }
}
