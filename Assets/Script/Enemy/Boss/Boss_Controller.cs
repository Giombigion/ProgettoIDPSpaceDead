using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Controller : MonoBehaviour
{
    public static Boss_Controller BossInstance;

    public NavMeshAgent meshAgentBoss;
    public Animator animatorBoss;

    public BossState bossState;

    [SerializeField] GameObject bossTargert;

    //----VARIBILI--------------------------------------------------------------------------------------------------
    bool setAttack = false;
    float distanzaAttaccoBoss;

    [SerializeField] GameObject manoDestra;
    [SerializeField] GameObject manoSinsitra;

    private void Awake()
    {
        BossInstance = this;
        meshAgentBoss = GetComponent<NavMeshAgent>();
        animatorBoss = GetComponent<Animator>();
    }
    private void Start()
    {
        bossState = BossState.idle;
    }

    private void Update()
    {
        BossStateMachine();

        var changeAnim = Vector3.Dot(meshAgentBoss.transform.forward, meshAgentBoss.velocity);
        animatorBoss.SetFloat("Chase", changeAnim, 0.16f, Time.deltaTime);
    }

    //----STATI DEL BOSS---------------------------------------------------------------------------------------------
    public void _idle()
    {

    }

    public void _normalAttack()
    {
        meshAgentBoss.speed = 5;
        meshAgentBoss.SetDestination(bossTargert.transform.position);
        BossAttack();
    }

    public void _chargeAttack()
    {

    }

    public void _stun()
    {

    }

    public void _dead()
    {

    }

    //----ALTRI METODI----------------------------------------------------------------------------------------------

    public void BossAttack()
    {
        distanzaAttaccoBoss = Vector3.Distance(transform.position, bossTargert.transform.position);
        if (distanzaAttaccoBoss < 2.5f)
        {
            setAttack = true;
        }
        else
        {
            setAttack = false;
        }

        animatorBoss.SetBool("Melee", setAttack);
        
    }

    //----STATE MACHINE---------------------------------------------------------------------------------------------
    void BossStateMachine()
    {
        switch (bossState)
        {
            case BossState.idle:
                _idle();
                break;
            case BossState.normalAttack:
                _normalAttack();
                break;
            case BossState.chargeAttack:
                _chargeAttack();
                break;
            case BossState.stun:
                _stun();
                break;
            case BossState.dead:
                _dead();
                break;
        }
    }
}
