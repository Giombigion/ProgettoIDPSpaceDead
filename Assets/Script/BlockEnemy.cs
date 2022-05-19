using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEnemy : MonoBehaviour
{
    public float StunTime;
    float timer = 0;
    public static BlockEnemy block;
    public bool isHit;

    [SerializeField] BasicAlien _basicAlien;

    private void Awake()
    {
        block = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isHit)
            Stun();
    }

    public void Stun()
    {
        _basicAlien.agent.speed = 0;

        timer += Time.deltaTime;

        if (timer > StunTime)
        {

            timer = 0;

            _basicAlien.agent.speed = 3.5f;

            isHit = false;

        }
    }
}