using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAlien : MonoBehaviour
{
    [SerializeField] Transform target;

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

    public int EnemyDamage;
    [SerializeField] float EnemyBackForce;

    float distanzaattacco;
    float timerattacco;
    public bool isAttacking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animazione = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (GameController.instance.state == GameState.play)
        {
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
                    animazione.SetBool("MeleeAttack", false);
                    print("non ti vedo");
                    Patrol();
                }
                
            }

            var changeAnim = Vector3.Dot(agent.transform.forward, agent.velocity);
            animazione.SetFloat("Blend", changeAnim, 0.16f, Time.deltaTime);
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
        rotateTo(EnemyPaths.position, 4);
        agent.SetDestination(EnemyPaths.position);
    }

    
    private Quaternion _rot;
    private Vector3 _direction;
    /// <summary>
    /// Ruota il gameobject in direzaione di un punto.
    /// </summary>
    /// <param name="t">destinazione</param>
    /// <param name="speed"> velocit� di rotazione</param>
    void rotateTo(Vector3 t,float speed) 
    {
        _direction = (t - agent.transform.position).normalized;
        _rot = Quaternion.LookRotation(_direction);
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, _rot, Time.deltaTime * speed);
    }

    //Codice per l'attacco dei Nemici
    void Attack()
    {
        agent.speed = 10;
        rotateTo(target.position, 4);
        agent.SetDestination(target.position);
        MeleeAttack();
    }

    void MeleeAttack()
    {
        distanzaattacco = Vector3.Distance(transform.position, target.transform.position);
        
        if (distanzaattacco < 2.5f) 
        {
            timerattacco += Time.deltaTime;                    
            if (timerattacco > 2)
            {
                isAttacking = false;
                timerattacco = 0;
            }
            else
            {
                isAttacking = true;
            }
            
        }
        else
        {
            isAttacking = false;
        }

        animazione.SetBool("MeleeAttack", isAttacking);
    }

    bool isFrontOff(float visuale)
    {
        var direzioneTarget = (transform.position - target.transform.position).normalized;
        var angle = Mathf.Acos(Vector3.Dot(transform.forward.normalized, direzioneTarget)) * 100;
        if (angle < visuale)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameController.instance.TakeDemage(EnemyDamage);
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * EnemyBackForce, ForceMode.Impulse);
        }
    }
}


