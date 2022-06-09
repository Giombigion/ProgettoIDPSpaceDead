using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameState state;
    public GameObject[] panels;
    [SerializeField] Testi _testi;

    [SerializeField] public int idlevel; //Variabile per l'assegnazione di un valore ad ogni livello

    //Variabile per la gestione delle musiche
    public GameObject[] music;

    //Variabile per la gestione dei livelli
    public GameObject[] levels;

    //Variabile per la gestione delle porte
    public bool[] Keys;

    //Variabili per la gestione della stamina
    [SerializeField] public Text StaminaData;
    public int currentStamina;

    //Variabili per la gestione delle munizioni
    [SerializeField] public Text AmmoData;
    public int currentAmmo = 1;

    [SerializeField] bool isMouseShowed;

    public Transform[] startspawnlevels; //Permette di far spawnare il palyer nello spawn di inzio livello
    public Transform[] checkPoints;  //Array per i checkpoint 

    //Variaibli per l'attivazione del guanto
    [SerializeField] GameObject gauntlet;
    [SerializeField] public GameObject gauntlet2;

    //Variabile per la gestione dei Chip
    public GameObject[] chipUI;

    [Range(0,15)]
    public float InteractiveOBJDistance;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        initLevel(idlevel);

        hidemouse();

        //Sceglie lo stato che permette di giocare
        state = GameState.play;

        //Disattiva all'avvio tutti i panel eccetto quello della stamina
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        if (idlevel != 0)
        {
            gauntlet2.SetActive(true);
            panels[1].SetActive(true);
            panels[4].SetActive(true);
            PlayerController.playercon.weaponEquipped = true;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="p">Indice del pannello</param>
    /// <param name="element">Elementi nel pannello</param>
    public void PannelMessage(int p, int element, bool active) {
        panels[p].SetActive(active);
        var t = panels[p].transform.GetChild(0).GetComponent<Text>().text;
        t = _testi.data[element].titolo;
        t = _testi.data[element].testo;
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
    }

    //Metodo per lo spostamento tra livelli
    public void initLevel(int id)
    {
        ActiveLevel(id);
        PlayerController.playercon.transform.position = startspawnlevels[id].position;
    }

    //Metodo per la comparsa/scomparsa dei livelli
    public void ActiveLevel(int id)
    {
        foreach(GameObject level in levels)
        {
            level.SetActive(false);
        }


        levels[id].SetActive(true);
    }

    /// <summary>
    /// Energia Player
    /// </summary>
    /// <param name="h"></param>
    //Metodo che riporta la stamina del player al massimo
    public void heal(int h)
    {
        currentStamina += h;
        currentStamina = Mathf.Clamp(currentStamina, 0, 100);
        StaminaData.text = currentStamina.ToString();
    }

    //Metodo che aumenta le munizioni disponibili del player
    public void ammoUp(int a)
    {
        currentAmmo += a;
        AmmoData.text = currentAmmo.ToString();
    }

    //Metodo che mostra a schermo i chip raccolti
    public void addChip(int chipCounter)
    {
        //Attiva il panel dedicato nel caso non sia ancora attivo
        if (!panels[3].activeInHierarchy)
        {
            panels[3].SetActive(true);
        }

        //Codice che gestisce il numero di chip visibili a schermo
        chipUI[chipCounter].SetActive(true);

    }

    //Metodo dedicato allo stato di IDLE
    public void _IDLE()
    {
        //print("Sono in Idle");
    }

    //Metodo dedicato allo stato di PLAY
    public void _PLAY()
    {
        //print("Sono in Play");
    }

    //Metodo dedicato allo stato di DEAD
    public void _DEAD()
    {
        //print("Sono in Dead");
    }

    //Metodo dedicato allo stato di PAUSE
    public void _PAUSE()
    {
        //print("Sono in Pause");
    }
    public void _TAKE()
    {
        //print("Sono in Take");

        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gauntlet.gameObject);

            state = GameState.tutorial;
        }
    }

    public void _TUTORIAL()
    {

        panels[5].SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panels[5].SetActive(false);

            //Disabilita il collider 
            //gauntlet2.GetComponent<BoxCollider>().enabled = false;

            //Distrugge il collider;
            Destroy(gauntlet2.GetComponent<BoxCollider>());


            gauntlet2.SetActive(true);
            //PlayerController.playercon.take = false;
            panels[0].SetActive(false);
            panels[1].SetActive(true);
            panels[4].SetActive(true);


            PlayerController.playercon.weaponEquipped = true;

            state = GameState.play;

        }
    }

    void hidemouse()
    {
        if (isMouseShowed)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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
            case GameState.tutorial:
                _TUTORIAL();
                break;
        }
    }
}
