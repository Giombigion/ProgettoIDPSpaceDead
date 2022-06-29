using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class PlayerController : MonoBehaviour
{
    Animator otherAnimator; //Assegno un nome al componente Animator del guanto.
    CharacterController controller; //Assegno un nome al componente CharacterController.
    public static PlayerController playercon;

    public AudioController audioController;

   [SerializeField] GameObject otherObject; //Inserisco il guanto.

    public GameObject Teletrasporto;
    public GameObject CameraPlayer;

    //Variabili per il movimento
    public float speed = 12f; //Imposto la forza con cui il Players si muove orizontalmente.
    public float speedStrafe = 12;
    public float gravity = -9.81f; //Imposto una forza che da una spinta verso il basso al Player, simulando la gravità.
    public float jumpHeight = 3f; //Imposto la forza che da una spinta verso l'alto al Player.
    public float rotSpeed; //imposto la forza con cui il Player ruota sul suo asse.
    public float groundDistance = 0.55f;
    float asseZ;
    float asseX;

    //Danno dei laser al giocatore.
    public float TakeDemage = 2f;

    //Variabile di controllo per l'interazione col guanto
    public bool take = false;

    //Varibili per il raycast per il controllo del terrenno sotto il gicatore.
    public Transform raypoint;
    public LayerMask layer;
    Vector3 velocity;

    bool Jump; //Variabile bool per il salto = variabile vera o falsa.
    [SerializeField] bool isGround;

    [SerializeField] int checkcounter; //Variabile per il controllo dei chekpoints

    //Variabili per la gestione dello sparo
    public bool weaponEquipped;
    float height;
    float runSpeed;
    public bool playSound;
    float t;
    bool CheckTp;
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
        controller = GetComponent<CharacterController>();
        audioController.audioSources[1].Play();
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
            }

            //Imposta le animazioni di camminata/corsa
            if (isGround && velocity.y < 0)
            { 
                velocity.y = -2f;
            }

            asseX = Input.GetAxis("Horizontal");
            asseZ = Input.GetAxis("Vertical");

            Vector3 movements = new Vector3(asseX, 0, asseZ);
            float sZarma = Vector3.Dot(movements, controller.transform.forward);

            //animator con BLEND TREE
            otherAnimator.SetFloat("zArma", sZarma, 0.2f, Time.deltaTime);

            Vector3 moveplayer = Vector3.forward * movements.z * speed * runSpeed + Vector3.right * movements.x * speedStrafe;
            moveplayer = transform.TransformDirection(moveplayer);
            controller.Move(moveplayer * Time.deltaTime);
            _gunScript.Shoot();
            CharacterMove();

            // GESTIONE PASSI
            if (Mathf.Abs(asseZ) > 0 && isGround)
            {
                if (PassoAlternato(moveplayer) > 0)
                {
                    audioController.AudioTimer(Random.Range(0.4f, 0.6f), "PassoSX");
                }
                else
                {
                    audioController.AudioTimer(Random.Range(0.35f, 0.55f), "PassoDX");
                }
            }
        }
        else {

            CharacterMove();
        }

        if(CheckTp == true)
        {
            TpForSpaceship(4);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="vettore"></param>
    /// <returns></returns>
    int PassoAlternato(Vector3 vettore) {
        return (int)(1 + Mathf.Abs(vettore.z)) % 2;
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
            GameController.instance.PannelMessage(0, 0, true);
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

    public void OnTriggerStay(Collider hit)
    {
        if (hit.gameObject.tag == "Crystal")
        {
            print(hit.gameObject.tag);
            Crystal.cr.crHeal();
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
        if (hit.gameObject.tag == "Laser")
        {
            GameController.instance.TakeDemage(2);
        }

        if (hit.gameObject.tag == "PulsantePorta")
        {
            if (GameController.instance.Keys[hit.gameObject.GetComponent<ScriptPulsantePorta>().ID])
            {
                audioController.Play("PortaAperta");
                hit.transform.gameObject.GetComponent<ScriptPulsantePorta>().animPort.Play("PortaAperta", -1, 0);
                GameController.instance.Keys[hit.gameObject.GetComponent<ScriptPulsantePorta>().ID] = false;
                Destroy(hit.gameObject.GetComponent<Collider>());
            }
            else
            {
                audioController.Play("PortaChiusa");
            }
        }

        if (hit.gameObject.tag == "TriggerNaveSorvolo")
        {
            hit.transform.gameObject.GetComponent<TriggerNaveSorvolo>().animShip01.Play("AstronaveSorvolo", -1, 0);
            hit.transform.gameObject.GetComponent<TriggerNaveSorvolo>().animShip02.Play("AstronaveSorvolo", -1, 0);
            audioController.Play("SpaceshipFlyby");

            Destroy(hit.gameObject);
        }

        if (hit.gameObject.tag == "TriggerMotherSpaceshipSound")
        {
            audioController.audioSources[0].Play();

            Destroy(hit.gameObject);
        }

        //Codice per la raccolta dei chip
        if (hit.gameObject.tag == "Chip")
        {
            Key(hit.gameObject.GetComponent<ChipScript>().keyID);

            Destroy(hit.gameObject);
        }

        //Codice per il teletrasporto a fine livello
        if (hit.gameObject.tag == "EndLevel")
        {
            if (weaponEquipped == true)
            {
                GameController.instance.state = GameState.idle;
                Teletrasporto.SetActive(true);
                audioController.Play("Teletrasporto");
                audioController.Stop("Rain_Terra");
                //Player viene bloccato

                CheckTp = true;
            }
            else{
                print("Ti serve il guanto per accedere all'area successiva");
            }
        }

        //Codice per i check points
        if (hit.gameObject.tag == "CheckPoint")
        {
            GameController.instance.state = GameState.play;
            checkcounter += 1;
            print(checkcounter);
            GameController.instance.startspawnlevels[0].position = GameController.instance.checkPoints[checkcounter].position;
            hit.gameObject.GetComponent<BoxCollider>().enabled = false;
            //Destroy(hit.gameObject);
        }
    }

    public void TpForSpaceship(float waitingTime)
    {   
        t += Time.deltaTime;
        if (t > waitingTime) //Aspetta il tempo di esecuzione della clip audio
        {
            GetComponent<CharacterController>().enabled = false;
            GameController.instance.idlevel += 1;
            GameController.instance.initLevel(GameController.instance.idlevel);
            GetComponent<CharacterController>().enabled = true;

            CheckTp = false;
        }
    }
}