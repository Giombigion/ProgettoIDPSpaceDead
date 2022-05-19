using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAlien : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float force;
    Rigidbody rb;
    public NavMeshAgent agent;
    [SerializeField] Transform[] Paths;
    [SerializeField] int IDPaths;
    Animator animazione;


    float distanzaWP;
    int contatorewaypoints;
    [SerializeField] Transform raypointfront;
    [SerializeField] Transform raypointback;
    [SerializeField] float lengthsphere;
    [SerializeField] bool isHuman;
    [SerializeField] float distanza;

    float timer;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animazione = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (GameController.instance.state == GameState.play)
        {
            //animazione.SetBool("isWalking", true);

 
            //Spheracast per il controllo della distanza tra il nemico e i vari oggetti
            RaycastHit hit;
            if (Physics.SphereCast(raypointfront.position, 1, transform.forward, out hit, lengthsphere))
            {
                if (hit.transform.gameObject.layer ==  6)
                {
                    isHuman = true;
                }
                else
                {
                    isHuman = false;
                }
                
                print(hit.transform.gameObject.layer);
            }


            //Angolo di vista e azioni

            distanza = Vector3.Distance(transform.position, target.transform.position);

            if (isFrontOff (270) && isHuman == true)
            {
                print("ti vedo");
                Attack();
            }
            else
            {
                if (distanza < 3)
                {
                    print("pensavi di farla franca!!");
                    Attack();
                }
                else
                {
                    print("non ti vedo");
                    Patrol();
                }
                
            }
                       

            var changeAnim = Vector3.Dot(agent.velocity, agent.transform.forward);
            animazione.SetFloat("Blend", changeAnim,0.2f,Time.deltaTime);

        }
    }
    void OnDrawGizmos()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(raypointfront.position, lengthsphere);
    }


    //Codice per la ronda dei Nemici
    void Patrol()
    {
        Transform EnemyPaths = Paths[IDPaths].transform.GetChild(contatorewaypoints);
        distanzaWP = Vector3.Distance(transform.position, EnemyPaths.position);
        

        if (distanzaWP < 2)
        {
            timer += Time.deltaTime;
            if (timer > EnemyPaths.GetComponent<WaypointScript>().waitState)
            {
                timer = 0;
                agent.speed = EnemyPaths.GetComponent<WaypointScript>().speedNode;

                contatorewaypoints += 1;
                if (contatorewaypoints >= Paths[IDPaths].childCount)
                {
                    contatorewaypoints = 0;
                }
            }
        }
        agent.SetDestination(EnemyPaths.position);

    }
    

    //Codice per l'attacco dei Nemici
    void Attack()
    {
        animazione.SetFloat("Blend", 1f);
        agent.speed = 3;
        agent.SetDestination(target.position);
        
    }

    bool isFrontOff(float visuale)
    {
        var direzioneTarget = (transform.position - target.transform.position).normalized;
        var angle = Mathf.Acos(Vector3.Dot(transform.forward.normalized, direzioneTarget)) * 100;
        //print("DEBUG ANGLE:" + angle);
        if (angle < visuale)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
