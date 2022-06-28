using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestiData
{
    public string titolo;

    [TextArea(5, 10)]
    public string testo;
}

[System.Serializable]
public class TestiHUD
{
    public string titolo;
}

public class Testi : MonoBehaviour
{

    public List<TestiData> data = new List<TestiData>();
    public List<TestiHUD> testiHUD = new List<TestiHUD>();

}
