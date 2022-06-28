using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEnemy : MonoBehaviour
{
    public static BlockEnemy block;

    [SerializeField] BasicAlien _basicAlien;

    public float StunTime;
    float timer = 0;
    public bool isHit;

    ParticleSystem particles;

    private void Awake()
    {
        block = this;
        particles = GetComponent<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        particles.enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit)
            Stun();
    }

    public void Stun()
    {
        particles.enableEmission = true;
        _basicAlien.agent.isStopped = true;
        timer += Time.deltaTime;

        if (timer > StunTime)
        {
            timer = 0;
            _basicAlien.agent.isStopped = false;
            particles.enableEmission = false;
            isHit = false;
        }
    }
}