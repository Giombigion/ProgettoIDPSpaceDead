using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Animator anim;
    float asseZ;
    float asseX;

    //Variabili per la gestione dello sparo
    [SerializeField] Transform weaponRayPoint;
    [SerializeField] int length = 5;
    bool isFired = false;

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

        float animSpeedZ = Vector3.Dot(movements, PlayerController.playercon.transform.forward);
        float animSpeedX = Vector3.Dot(movements, PlayerController.playercon.transform.right);

        //animator con BLEND TREE
        anim.SetFloat("xArma", animSpeedX, 0.2f, Time.deltaTime);
        anim.SetFloat("zArma", animSpeedZ, 0.2f, Time.deltaTime);
    }

    //Codice per sparare
    public void Shoot()
    {
        //SEGUE IL MOUSE
        Debug.DrawRay(weaponRayPoint.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.blue);

        if (Input.GetButtonDown("Fire1") && PlayerController.playercon.weaponEquipped == true && isFired == false
            && GameController.instance.currentStamina > 5 && GameController.instance.gauntlet2.activeInHierarchy)
        {
            AudioController.instance.PlaySound(AudioController.instance.sourceSFX, "ColpoArma");
            anim.Play("SparoGuanto", -1, 0);
            print("bullet fired");
            isFired = true;
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

        GameController.instance.currentStamina -= 5;
        GameController.instance.StaminaData.text = GameController.instance.currentStamina.ToString();

        isFired = false;
    }
}
