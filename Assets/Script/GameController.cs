using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameState state;
    public GameObject[] panels;
    [SerializeField] Text StaminaData;
    public int currentStamina;

    public Transform[] spawnlevel; //Permette di far spawnare il palyer nello spawn di inzio livello

    [SerializeField] GameObject player;
    [SerializeField] GameObject gauntlet;
    [SerializeField] GameObject gauntlet2;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.play;

        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        //panels[2].SetActive(true);

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

    public void heal()
    {
        currentStamina = 100;
        StaminaData.text = currentStamina.ToString();
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
    public void _TAKE()
    {
        print("Sono in Take");

        if (Input.GetKeyDown(KeyCode.X))
        {
            Destroy(gauntlet.gameObject);
            gauntlet2.SetActive(true);
            //PlayerController.playercon.take = false;
            panels[0].SetActive(false);
            panels[1].SetActive(true);

            state = GameState.play;
        }
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
            case GameState.take:
                _TAKE();
                break;
        }
    }
}
