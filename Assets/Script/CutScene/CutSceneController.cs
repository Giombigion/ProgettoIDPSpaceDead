using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneController : MonoBehaviour
{
    public static CutSceneController controller;

    [SerializeField] public GameObject[] cutscenes;

    public KeyCode key;

    [SerializeField] private PlayableDirector introCutscene = null;
    [SerializeField] public double skiptime = 50f;

    [SerializeField] public GameObject skipLine;

    public GameObject testiIntro;

    // Start is called before the first frame update
    void Start()
    {
        controller = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) && MenuController.menuController.skippable == true)
        {
            introCutscene.time = skiptime;
            Destroy(testiIntro);
            Destroy(skipLine);
            GameController.instance.state = GameState.play;
        }
    }
}
