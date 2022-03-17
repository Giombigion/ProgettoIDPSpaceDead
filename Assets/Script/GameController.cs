using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameState state;

    public Transform[] spawnlevel; //Permette di far spawnare il palyer nello spawn di inzio livello

    [SerializeField] GameObject player;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.play;
        initLevel(PlayerController.playercon.idlevel);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
    }

    void initLevel(int id)
    {
        //ActiveLevel(id);
        player.transform.position = spawnlevel[id].position;
    }

    //Metodo dedicato allo stato di IDLE
    public void _IDLE()
    {
        print("Sono in Idle");
    }

    //Metodo dedicato allo stato di PLAY
    public void _PLAY()
    {
        print("Sono in Play");
    }

    //Metodo dedicato allo stato di DEAD
    public void _DEAD()
    {
        print("Sono in Dead");
    }

    //Metodo dedicato allo stato di PAUSE
    public void _PAUSE()
    {
        print("Sono in Pause");
    }

    void StateMachine()
    {
        switch (state)
        {
            case GameState.idle:
                _IDLE();
                break;
            case GameState.play:
                _PLAY();
                break;
            case GameState.dead:
                _DEAD();
                break;
            case GameState.pause:
                _PAUSE();
                break;
        }
    }
}
