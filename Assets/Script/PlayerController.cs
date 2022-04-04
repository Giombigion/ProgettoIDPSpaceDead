using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Animator anim; //Assegno un nome al componente Animator.
    CharacterController controller; //Assegno un nome al componente CharacterController.

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
    public bool take = false;


    //Varibili per il raycast.
    public Transform raypoint;
    bool Jump; //Variabile bool per il salto = variabile vera o falsa.
    [SerializeField] bool isGround;

    public LayerMask layer;
    Vector3 velocity;

    public static PlayerController playercon;

    [SerializeField] public int idlevel; //Variabile per l'assegnazione di un valore ad ogni livello

    public void Awake()
    {
        playercon = this;
    }

    private void Start()
    {
        //anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Se il componente GameMaster è settato sullo stato di Play, allora esegui il contenuto.
        if (GameController.instance.state == GameState.play || GameController.instance.state == GameState.take)
        {
            isGround = Physics.CheckSphere(raypoint.position, groundDistance, layer);
            Jump = Input.GetButtonDown("Jump");

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

            float animSpeedX = Vector3.Dot(movements, controller.transform.right);
            float animSpeedY = Vector3.Dot(movements, controller.transform.forward);

            //animator con BLEND TREE
            //anim.SetFloat("ypose", animSpeedY, 0.2f, Time.deltaTime);
            //anim.SetFloat("xpose", animSpeedX, 0.2f, Time.deltaTime);

            Vector3 moveplayer = Vector3.forward * movements.z * speed + Vector3.right * movements.x * speedStrafe;
            moveplayer = transform.TransformDirection(moveplayer);
            controller.Move(moveplayer*Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, rotX * rotSpeed , 0);

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision hit)
    {

        print(hit.gameObject.tag);

        if (hit.gameObject.tag == "Gauntlet" && take == false)
        {
            GameController.instance.panels[0].SetActive(true);
            GameController.instance.state = GameState.take;
            take = true;
        }

    }
    private void OnTriggerEnter(Collider hit)
    {

        if (hit.gameObject.tag == "Medikit")
        {
            Destroy(hit.gameObject);
            print("Your Health was maxed out");
            GameController.instance.heal();
        }
        if (hit.gameObject.tag == "Ammo")
        {
            Destroy(hit.gameObject);
            print("You gained 1 ammo");
            GameController.instance.ammoUp();
        }

    }

}