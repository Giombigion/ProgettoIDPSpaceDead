using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManiBoss : MonoBehaviour
{
    [SerializeField] int DannoMeleeAttack;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameController.instance.TakeDemage(DannoMeleeAttack);
            //collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * EnemyBackForce, ForceMode.Impulse);
        }
    }
}
