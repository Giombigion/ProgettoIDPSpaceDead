using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MenuController : MonoBehaviour
{
    public GameObject CameraCutscene;
    [SerializeField] GameObject menu;

    public void _StartButton()
    {
        CameraCutscene.GetComponent<PlayableDirector>().Play();
        CutSceneController.controller.skipLine.SetActive(true);
        menu.SetActive(false);
    }

    public void _SettingsButton()
    {

    }

    public void _ExitButton()
    {

    }
}
