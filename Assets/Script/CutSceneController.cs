using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] GameObject[] cutscenes;


    void Start()
    {
        cutscenes[0].SetActive(true);
        cutscenes[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!cutscenes[0].activeInHierarchy)
        {
            GameController.instance.panels[2].SetActive(true);
        }
    }
}
