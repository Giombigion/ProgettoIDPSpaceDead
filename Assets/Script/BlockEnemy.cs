using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEnemy : MonoBehaviour
{
    public float StunTime;
    float timer = 0;
    public static BlockEnemy block;
    public bool isHit;
    ParticleSystem emission;

    [SerializeField] BasicAlien _basicAlien;

    private void Awake()
    {
        emission = GetComponent<ParticleSystem>();
        block = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        var em = emission.emission;
        em.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit)
            Stun();
    }

    public void Stun()
    {
        var em = emission.emission;
        em.enabled = true;

        _basicAlien.agent.isStopped = true;

        timer += Time.deltaTime;

        if (timer > StunTime)
        {

            timer = 0;

            _basicAlien.agent.isStopped = false;

            em.enabled = false;

            isHit = false;

        }
    }
}