using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    Animator anim; //Assegno un nome al componente Animator.
    float asseZ;
    float asseX;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        asseX = Input.GetAxis("Horizontal");
        asseZ = Input.GetAxis("Vertical");

        Vector3 movements = new Vector3(asseX, 0, asseZ);

        float animSpeedX = Vector3.Dot(movements, PlayerController.playercon.transform.right);
        float animSpeedY = Vector3.Dot(movements, PlayerController.playercon.transform.forward);

        //animator con BLEND TREE
        anim.SetFloat("yArma", animSpeedY, 0.2f, Time.deltaTime);
        anim.SetFloat("xArma", animSpeedX, 0.2f, Time.deltaTime);
    }
}
