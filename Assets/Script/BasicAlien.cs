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
    //[SerializeField] Transform[] WayPoints;
    [SerializeField] Transform[] Paths;
    [SerializeField] int IDPaths;
    Animator animazione;


    float distanzaWP;
    int contatorewaypoints;
    [SerializeField] Transform raypoint;
    [SerializeField] LayerMask layer;
    [SerializeField] float lengthray;
    [SerializeField] bool isHuman;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animazione = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.state == GameState.play)
        {
            animazione.SetBool("isWalking", true);

            //Spheracast per il controllo della distanza tra il nemico e i vari oggetti
            RaycastHit hit;
            if (Physics.SphereCast(raypoint.position, 1, transform.forward, out hit, 5))
            {
                if (hit.transform.gameObject.layer == 6)
                {
                    isHuman = true;
                }
                else
                {
                    isHuman = false;
                }
                
                print(hit.transform.gameObject.layer);
            }


            //Debug.DrawRay(raypoint.position, raypoint.forward * lengthray, Color.red);

            //
            Vector3 directionToTarget = target.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            //Angolo di vista 
            if (Mathf.Abs(angle) < 110 && isHuman == true)
            {
                print("ti vedo");
                Attack();
            }
            else
            {
                print("non ti vedo");
                Patrol();
            }

        }
    }
    void OnDrawGizmos()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(raypoint.position, 5);
    }

    void Patrol()
    {
        animazione.SetBool("isAttacking", false);

        Transform EnemyPaths = Paths[IDPaths].transform.GetChild(contatorewaypoints);
        distanzaWP = Vector3.Distance(transform.position, EnemyPaths.position);
        //print(distanzaWP);

        if (distanzaWP < 2)
        {
            contatorewaypoints += 1;
            if (contatorewaypoints >= Paths[IDPaths].childCount)
            {
                contatorewaypoints = 0;
            }
        }
        agent.SetDestination(EnemyPaths.position);

    }

    void Attack()
    {
        agent.SetDestination(target.position);
        animazione.SetBool("isAttacking", true);
    }
}
