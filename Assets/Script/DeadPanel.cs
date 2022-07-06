using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPanel : MonoBehaviour
{
    public void _Respawn()
    {
        GameController.instance.state = GameState.play;
        GameController.instance.panels[6].SetActive(false);
        GameController.instance.hidemouse();
    }
}
