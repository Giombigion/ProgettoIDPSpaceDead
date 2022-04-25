using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsantePorta : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.GetComponent<Animator>(); //parent non è giusto. Deve prendere l'animator dalla porta
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
