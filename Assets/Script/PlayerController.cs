using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator otherAnimator; //Assegno un nome al componente Animator del guanto.
    CharacterController controller; //Assegno un nome al componente CharacterController.

   [SerializeField] GameObject otherObject; //Inserisco il guanto.

    //Variabili per il movimento
    public float speed = 12f; //Imposto la forza con cui il Players si muove orizontalmente.
    public float speedStrafe = 12;
    public float gravity = -9.81f; //Imposto una forza che da una spinta verso il basso al Player, simulando la gravit�.
    public float jumpHeight = 3f; //Imposto la forza che da una spinta verso l'alto al Player.
    public float rotSpeed; //imposto la forza con cui il Player ruota sul suo asse.
    public float groundDistance = 0.4f;
    float asseZ;
    float asseX;
    float rotX = 90;

    //Variabile di controllo per l'interazione col guanto
    public bool take = false;

    //Varibili per il raycast.
    public Transform raypoint;
    bool Jump; //Variabile bool per il salto = variabile vera o falsa.
    [SerializeField] bool isGround;

    public LayerMask layer;
    Vector3 velocity;

    public static PlayerController playercon;

    [SerializeField] public int idlevel; //Variabile per l'assegnazione di un valore ad ogni livello
    [SerializeField] int checkcounter; //Variabile per il controllo dei chekpoints

    //Variabili per la gestione dei chip
    public int chipCounter = 0;

    //Variabili per la gestione dello sparo
    [SerializeField] public float maxTime = 0.5f;
    [SerializeField] Transform weaponRayPoint;
    [SerializeField] int length = 5;
    public bool weaponEquipped;
    bool isFired = false;
    float timer;
    float height;
    float runSpeed;

    public void Awake()
    {
        playercon = this;
        otherAnimator = otherObject.GetComponent<Animator>();
    }

    private void Start()
    {
        //anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        //Se il componente GameMaster � settato sullo stato di Play, allora esegui il contenuto.
        if (GameController.instance.state == GameState.play || GameController.instance.state == GameState.take)
        {
            isGround = Physics.CheckSphere(raypoint.position, groundDistance, layer);
            Jump = Input.GetButtonDown("Jump");


            var run = Input.GetKey(KeyCode.LeftShift);
            var cruch = Input.GetKey(KeyCode.LeftControl);

            //RUN
            if (run)
            {
                runSpeed = 2;
            }
            else
            {
                runSpeed = 1;
            }
            //end RUN

            //Cruch
            if (cruch)
            {
                height = 0.5f;
                runSpeed = 0.5f;
            }
            else {
                height = 2;
            }
            height = Mathf.Lerp(height, controller.height,Time.deltaTime*2);
            controller.height = height;
            //end Cruch


            //Imposta l'animazione del salto
            if (Jump && isGround)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                //anim.SetBool("isjump", true);
            }

            //Imposta le animazioni di camminata/corsa
            if (isGround && velocity.y < 0)
            {
                //anim.SetBool("isjump", false);
                velocity.y = -2f;
            }

            asseX = Input.GetAxis("Horizontal");
            //rotX += asseX;
            asseZ = Input.GetAxis("Vertical");

            Vector3 movements = new Vector3(asseX, 0, asseZ);

            //float animSpeedX = Vector3.Dot(movements, controller.transform.right);
            //float animSpeedY = Vector3.Dot(movements, controller.transform.forward);

            //animator con BLEND TREE
            otherAnimator.SetFloat("yArma", asseZ, 0.2f, Time.deltaTime);
            //otherAnimator.SetFloat("xArma", animSpeedX, 0.2f, Time.deltaTime);

            Vector3 moveplayer = Vector3.forward * movements.z * speed * runSpeed + Vector3.right * movements.x * speedStrafe;
            moveplayer = transform.TransformDirection(moveplayer);
            controller.Move(moveplayer*Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, rotX * rotSpeed , 0);

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            
            Shoot();

        }
    }

    private void OnCollisionEnter(Collision hit)
    {

        print(hit.gameObject.tag);

        //Codice per l'ottenimento del guanto
        if (hit.gameObject.tag == "Gauntlet" && take == false)
        {
            GameController.instance.panels[0].SetActive(true);
            GameController.instance.state = GameState.take;
            take = true;
        }

    }
    private void OnTriggerEnter(Collider hit)
    {
        //Codice per la raccolta dei medikit
        if (hit.gameObject.tag == "Medikit")
        {
            Destroy(hit.gameObject);
            print("Your Health was maxed out");
            GameController.instance.heal();
        }

        //Codice per la raccolta delle munizioni
        if (hit.gameObject.tag == "Ammo")
        {
            Destroy(hit.gameObject);
            print("You gained 1 ammo");
            GameController.instance.ammoUp();
        }

        //Codice per la raccolta dei chip
        if (hit.gameObject.tag == "Chip")
        {
            print(chipCounter);
            GameController.instance.addChip(chipCounter);
            chipCounter += 1;
            Destroy(hit.gameObject);
        }

        //Codice per il teletrasporto a fine livello
        if (hit.gameObject.tag == "EndLevel")
        {
            GetComponent<CharacterController>().enabled = false;
            GameController.instance.initLevel(idlevel + 1);
            GetComponent<CharacterController>().enabled = true;
        }

        //Codice per i check points
        if (hit.gameObject.tag == "CheckPoint")
        {
            checkcounter += 1;
            print(checkcounter);
            GameController.instance.startspawnlevels[0].position = GameController.instance.checkPoints[checkcounter].position;
            hit.gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(hit.gameObject);
        }
    }

    //Codice per sparare
    private void Shoot()
    {
        //Debug.DrawRay(weaponRayPoint.position, weaponRayPoint.forward * length, Color.red);

        //SEGUE IL MOUSE
        Debug.DrawRay(weaponRayPoint.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.blue);
        //

        if (Input.GetButtonDown("Fire1") && weaponEquipped == true && isFired == false 
            && GameController.instance.currentAmmo > 0 && GameController.instance.gauntlet2.activeInHierarchy)
        {
            print("bullet fired");
            isFired = true;
        }
        else if(GameController.instance.currentAmmo == 0 && GameController.instance.gauntlet2.activeInHierarchy)
        {
            print("No ammo left");
        }

        if (isFired)
        {
            FiringRay();
            //firing();
        }

    }

    void FiringRay() {
        RaycastHit hit;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, length))
        {
            print(hit.transform.name);

            if (hit.transform.tag == "Alien")
            {
                Destroy(hit.transform.gameObject);
            }
            else if (hit.transform.tag == "PulsantePorta")
            {
                hit.transform.gameObject.GetComponent<ScriptPulsantePorta>().animPort.Play("PortaAperta", -1, 0);
            }

        }

        GameController.instance.currentAmmo -= 1;
        GameController.instance.AmmoData.text = GameController.instance.currentAmmo.ToString();

        isFired = false;
    }


    private void firing()
    {
        timer += Time.deltaTime;

        if (timer > maxTime)
        {

            timer = 0;

            RaycastHit hit;
            if (Physics.Raycast(weaponRayPoint.position, weaponRayPoint.forward, out hit, length))
            {
                print("Target hit");
                print(hit.transform.name);

                if (hit.transform.tag == "Alien")
                {
                    Destroy(hit.transform.gameObject);
                }

                if (hit.transform.tag == "PulsantePorta")
                {
                    hit.transform.gameObject.GetComponent<ScriptPulsantePorta>().animPort.Play("PortaAperta", -1, 0);
                }
               // Debug.DrawRay(weaponRayPoint.position, weaponRayPoint.transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            }
            else
            {
                print("Target not hit");
            }

            GameController.instance.currentAmmo -= 1;
            GameController.instance.AmmoData.text = GameController.instance.currentAmmo.ToString();

            isFired = false;

        }
    }
}