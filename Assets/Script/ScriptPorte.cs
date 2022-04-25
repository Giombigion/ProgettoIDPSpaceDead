using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPorte : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //parent non è giusto. Deve prendere l'animator dalla porta
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void porta_aperta()
    {
        anim.Play("PortaAperta", -1, 0);
    }
}
