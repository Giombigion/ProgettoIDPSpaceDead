using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    public void HideMe()
    {
        gameObject.SetActive(false);
    }

    public void ShowMe()
    {
        gameObject.SetActive(true);
    }
}
