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
public class TestiDialoghi
{ 
    public string narratore;

    [TextArea(5, 10)]
    public string dialogo;
}

public class Testi : MonoBehaviour
{

    public List<TestiData> data = new List<TestiData>();
    public List<TestiDialoghi> dialoghi = new List<TestiDialoghi>();

}
