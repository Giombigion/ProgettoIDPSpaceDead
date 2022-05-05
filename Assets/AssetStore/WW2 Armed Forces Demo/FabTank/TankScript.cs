using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TankScript : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;

    [SerializeField] Transform target;
    [SerializeField] float smoothturret;
    [SerializeField] float smoothgun;
    [SerializeField] float clampGun;
    //[SerializeField] float clampTurret;
    [SerializeField] Transform turret;
    [SerializeField] Transform gun;

    //Variabili per l'attacco n02
    [SerializeField] Vector3[] targetPositions;
    [SerializeField] float IntervalloInseguimento;
    int counterTargetPositions;

    public Transform[] Paths;
    [SerializeField] int IDPaths;
    float wpdistance;
    [SerializeField ]float timer;
    int counter;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var targetdistance = target.transform.position - transform.position;
        
        //Torretta
        Quaternion target_rot = Quaternion.LookRotation(targetdistance);
        target_rot.x = 0;
        target_rot.z = 0;
        //target_rot.y = Mathf.Clamp(target_rot.y, -clampTurret, clampTurret);//y
        turret.rotation = Quaternion.Lerp(turret.rotation, target_rot, Time.deltaTime * smoothturret);

        //Cannone
        Quaternion gun_target_rot = Quaternion.LookRotation(targetdistance);
        gun_target_rot.z = 0;
        gun_target_rot.y = 0;
        gun_target_rot.x = Mathf.Clamp(gun_target_rot.x, -clampGun, clampGun);//x
        gun.localRotation = Quaternion.Lerp(gun.localRotation, gun_target_rot, Time.deltaTime * smoothgun);

        //Patrol();
        Attack2();

    }

    /*void Patrol()//Questo per dargli un percorso
    {
        Transform EnemyPaths = Paths[IDPaths].transform.GetChild(counter);
        wpdistance = Vector3.Distance(transform.position, EnemyPaths.position);

        if (wpdistance < 2)
        {
            nextNode();
        }

        agent.SetDestination(EnemyPaths.position);

    }
    */

    //Questo se vogliamo che segua il palyer
    void Attack()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer = 0;
            agent.SetDestination(target.position);
        }
    }

    //Questo se vogliamo che segua il palyer seguendo le sue tracce
    void Attack2()
    {
        timer += Time.deltaTime;
        if (timer > IntervalloInseguimento)
        {
            timer = 0;
            targetPositions[counterTargetPositions] = target.position;
            counterTargetPositions += 1;
        }

        if (counterTargetPositions >= targetPositions.Length)
        {
            counterTargetPositions = 0;
        }

        agent.SetDestination(targetPositions[counterTargetPositions]);
    }

    void nextNode() {
        counter += 1;
        if (counter >= Paths[IDPaths].childCount)
        {
            counter = 0;
        }
    }
}
