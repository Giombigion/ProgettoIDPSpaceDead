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

    [SerializeField] GameObject skipLine;

    // Start is called before the first frame update
    void Start()
    {
        controller = this;
        cutscenes[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!cutscenes[0].activeInHierarchy)
        {
            GameController.instance.panels[2].SetActive(true);
        }

        if (Input.GetKeyDown(key))
        {
            introCutscene.time = skiptime;
            Destroy(skipLine);
        }
    }
}
