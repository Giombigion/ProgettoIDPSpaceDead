using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    /*Animator anim; //Assegno un nome al componente Animator.
    float asseZ;
    float asseX;*/

    //Variabili per la gestione dello sparo
    [SerializeField] float maxTime = 0.5f;
    [SerializeField] Transform weaponRayPoint;
    [SerializeField] int length = 5;
    bool isFired = false;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        /*asseX = Input.GetAxis("Horizontal");
        asseZ = Input.GetAxis("Vertical");

        Vector3 movements = new Vector3(asseX, 0, asseZ);

        float animSpeedX = Vector3.Dot(movements, PlayerController.playercon.transform.right);
        float animSpeedY = Vector3.Dot(movements, PlayerController.playercon.transform.forward);

        //animator con BLEND TREE
        anim.SetFloat("yArma", animSpeedY, 0.2f, Time.deltaTime);
        anim.SetFloat("xArma", animSpeedX, 0.2f, Time.deltaTime);*/

    }

    //Codice per sparare
    public void Shoot()
    {
        //Debug.DrawRay(weaponRayPoint.position, weaponRayPoint.forward * length, Color.red);

        //SEGUE IL MOUSE
        Debug.DrawRay(weaponRayPoint.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.blue);

        if (Input.GetButtonDown("Fire1") && PlayerController.playercon.weaponEquipped == true && isFired == false
            && GameController.instance.currentAmmo > 0 && GameController.instance.gauntlet2.activeInHierarchy)
        {
            print("bullet fired");
            isFired = true;
        }
        else if (GameController.instance.currentAmmo == 0 && GameController.instance.gauntlet2.activeInHierarchy)
        {
            print("No ammo left");
        }

        if (isFired)
        {
            FiringRay();
        }

    }
    public void FiringRay()
    {
        RaycastHit hit;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, length))
        {
            print(hit.transform.name);

            if (hit.transform.tag == "Alien")
            {
                BlockEnemy.block.isHit = true;
                print("colpito");
            }
        }

        GameController.instance.currentAmmo -= 1;
        GameController.instance.AmmoData.text = GameController.instance.currentAmmo.ToString();

        isFired = false;
    }

}
