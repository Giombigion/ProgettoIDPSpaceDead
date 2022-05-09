using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAlien : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float force;
    Rigidbody rb;
    NavMeshAgent agent;
    [SerializeField] Transform[] Paths;
    [SerializeField] int IDPaths;
    Animator animazione;


    float distanzaWP;
    int contatorewaypoints;
    [SerializeField] Transform raypointfront;
    [SerializeField] Transform raypointback;
    [SerializeField] float lengthray;
    [SerializeField] bool isHuman;
    [SerializeField] LayerMask layer;

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
            if (Physics.SphereCast(raypointfront.position, 1, transform.forward, out hit, 5))
            {
                if (/*hit.transform.gameObject.layer == layer*/ layer == 6)
                {
                    isHuman = true;
                }
                else
                {
                    isHuman = false;
                }
                
                print(hit.transform.gameObject.layer);
            }

            //Raycast che permette di capire al nemico che il player si trova alle sue spelle
            RaycastHit hit1;
            if (Physics.Raycast(raypointback.position, -raypointback.forward, out hit1, lengthray))
            {
                if(hit1.transform.gameObject.layer == layer)
                {
                    Attack();
                }
                else
                {
                    Patrol();
                }

            }
            Debug.DrawRay(raypointback.position, -raypointback.forward* lengthray, Color.blue);

            //
            Vector3 directionToTarget = target.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            //Angolo di vista 
            if (Mathf.Abs(angle) <= 120 && isHuman == true)
            {
                print("ti vedo");
                Attack();
            }
            else
            {
                print("non ti vedo");
                Patrol();
            }

            var changeAnim = Vector3.Dot(agent.velocity, agent.transform.forward);
            animazione.SetFloat("Blend", changeAnim,0.2f,Time.deltaTime);

        }
    }
    void OnDrawGizmos()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(raypointfront.position, 5);
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
}
