using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator otherAnimator; //Assegno un nome al componente Animator del guanto.
    CharacterController controller; //Assegno un nome al componente CharacterController.

    ChipScript chipscript;

   [SerializeField] GameObject otherObject; //Inserisco il guanto.

    //Variabili per il movimento
    public float speed = 12f; //Imposto la forza con cui il Players si muove orizontalmente.
    public float speedStrafe = 12;
    public float gravity = -9.81f; //Imposto una forza che da una spinta verso il basso al Player, simulando la gravità.
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

    [SerializeField] int checkcounter; //Variabile per il controllo dei chekpoints

    //Variabili per la gestione dello sparo
    float timer;
    public bool weaponEquipped;
    float height;
    float runSpeed;

    [SerializeField] GunScript _gunScript;

    //Variabili per la gestione dei chip
    public int chipCounter = 0;

    public void Awake()
    {
        playercon = this;
        otherAnimator = otherObject.GetComponent<Animator>();
    }

    private void Start()
    {
        //anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        chipscript = new ChipScript();

    }

    // Update is called once per frame
    void Update()
    {
        //Se il componente GameMaster è settato sullo stato di Play, allora esegui il contenuto.
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
            else
            {
                height = 2;
            }
            height = Mathf.Lerp(height, controller.height, Time.deltaTime * 2);
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
            controller.Move(moveplayer * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, rotX * rotSpeed , 0);
            _gunScript.Shoot();
            CharacterMove();

        }
        else {

            CharacterMove();
        }
    }


    void CharacterMove() {

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
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

    private void OnCollisionExit(Collision hit)
    {
        print(hit.gameObject.tag);

        //Codice per l'ottenimento del guanto
        if (hit.gameObject.tag == "Gauntlet" && take == true)
        {
            GameController.instance.panels[0].SetActive(false);
            GameController.instance.state = GameState.play;
            take = false;
        }
    }

    public void OnCollisionStay(Collision hit)
    {
        if (hit.gameObject.tag == "Alien")
        {
            this.transform.position = GameController.instance.checkPoints[GameController.instance.idlevel].position;
        }
    }

    public void Key(int key)
    {
        print(chipCounter);
        GameController.instance.addChip(chipCounter);
        chipCounter += 1;

        Debug.Log("Sto raccogliendo chip");

        GameController.instance.Keys[key] = true;
        Debug.Log("Chip");

    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Collezzionabili")
        {
            hit.gameObject.GetComponent<BoxCollider>().enabled = false;

            var Obj = hit.gameObject.GetComponent<CollectableScript>();

            if (Obj.ID == 0)
            {
                Destroy(hit.gameObject);
                GameController.instance.heal(Obj.maxhealth);
            }
            if (Obj.ID == 1)
            {
                Destroy(hit.gameObject);
                GameController.instance.ammoUp(Obj.maxammo);
            }
        }

        if (hit.gameObject.tag == "PulsantePorta")
        {
            if (GameController.instance.Keys[hit.gameObject.GetComponent<ScriptPulsantePorta>().ID])
            {
                hit.transform.gameObject.GetComponent<ScriptPulsantePorta>().animPort.Play("PortaAperta", -1, 0);
            }
        }

        /*
        //Codice per la raccolta dei medikit
        if (hit.gameObject.tag == "Medikit")
        {
            Destroy(hit.gameObject);
            print("Your Health was maxed out");
            //GameController.instance.heal();
        }

        //Codice per la raccolta delle munizioni
        if (hit.gameObject.tag == "Ammo")
        {
            Destroy(hit.gameObject);
            print("You gained 1 ammo");
            //GameController.instance.ammoUp();
        }
        */

        //Codice per la raccolta dei chip
        if (hit.gameObject.tag == "Chip")
        {
            Key(hit.gameObject.GetComponent<ChipScript>().keyID);

            Destroy(hit.gameObject);
        }

        //Codice per il teletrasporto a fine livello
        if (hit.gameObject.tag == "EndLevel")
        {
            GetComponent<CharacterController>().enabled = false;
            GameController.instance.idlevel += 1;
            GameController.instance.initLevel(GameController.instance.idlevel);
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

}