using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEnemy : MonoBehaviour
{
    public float StunTime;
    float timer = 0;
    public static BlockEnemy block;
    public bool isHit;
    ParticleSystem particles;

    [SerializeField] BasicAlien _basicAlien;

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

        _basicAlien.agent.speed = 0;

        timer += Time.deltaTime;

        if (timer > StunTime)
        {

            timer = 0;

            _basicAlien.agent.speed = 3.5f;

            particles.enableEmission = false;

            isHit = false;

        }
    }
}