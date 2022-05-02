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

    public Transform[] Paths;
    [SerializeField] int IDPaths;
    float wpdistance;
    float timer;
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

        Patrol();

    }

    void Patrol()
    {
        Transform EnemyPaths = Paths[IDPaths].transform.GetChild(counter);
        wpdistance = Vector3.Distance(transform.position, EnemyPaths.position);

        if (wpdistance < 2)
        {
            nextNode();
        }

        agent.SetDestination(EnemyPaths.position);

    }

    void nextNode() {
        counter += 1;
        if (counter >= Paths[IDPaths].childCount)
        {
            counter = 0;
        }
    }
}
