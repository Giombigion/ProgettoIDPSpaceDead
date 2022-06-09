using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestiData{
    public int id;
    public string titolo;

    [TextArea(5, 10)]
    public string testo;
}

public class Testi : MonoBehaviour
{

    public List<TestiData> data = new List<TestiData>();

}
