using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : MonoBehaviour
{
    float timer;
    public float chargeTime = 5f;

    Boss_Controller _bossController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        changeState(timer);
    }

    public void changeState(float timer)
    {
        if (timer > chargeTime)
        {
            timer = 0;
            _bossController.animazione.SetFloat("Chase", 1f, 0.16f, Time.deltaTime);
        }
    }
}
