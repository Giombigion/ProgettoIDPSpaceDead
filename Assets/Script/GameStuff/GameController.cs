using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public AudioController audioController;

    public GameState state;

    public GameObject[] panels;
    [SerializeField] Testi _testi;

    //Variabile per la gestione dei Chip
    public GameObject[] chipUI;

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

    [SerializeField] bool isMouseShowed;

    public Transform[] startspawnlevels; //Permette di far spawnare il palyer nello spawn di inzio livello
    public Transform[] checkPoints;  //Array per i checkpoint 

    //Variaibli per l'attivazione del guanto
    [SerializeField] GameObject gauntlet;
    [SerializeField] public GameObject gauntlet2;

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

        audioController.Play("Rain_Terra"); //Spostare in un metodo.

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

    // Update is called once per frame
    void Update()
    {
        StateMachine();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p">Indice del pannello</param>
    /// <param name="element">Elementi nel pannello</param>
    public void PannelMessage(int p, int element, bool active)
    {
        panels[p].SetActive(active);
        panels[p].transform.GetChild(0).GetComponent<Text>().text = _testi.data[element].titolo;

        try
        {
            panels[p].transform.GetChild(1).GetComponent<Text>().text = _testi.data[element].testo;
        }
        catch
        {
            print("Non esiste l'elemento per il testo");
        }
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
    //Metodo danno al player
    public void TakeDemage(int demageAmount)
    {
        currentStamina -= demageAmount;
        StaminaData.text = currentStamina.ToString();
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

    //-----STATI DI GIOCO----------------------------------------------------------------------------------------
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

            audioController.audioSources[1].Stop();
            audioController.Play("TestoGuanto");
            state = GameState.tutorial;
        }
    }

    public void _TUTORIAL()
    {
        PannelMessage(0, 1, true);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            audioController.Play("EquipaggiamentoGuanto"); //QUI NON VA BENE QUESTO, PERCHE' VERRA' ESEGUITO OGNI VOLTA CHE SI ESCE DA UN PANNELLO. PERO' FUNZIONA SOLO SE MESSO QUI. DA CONTROLLARE
            PannelMessage(0, 1, false);

            //Distrugge il collider;
            Destroy(gauntlet2.GetComponent<BoxCollider>());

            gauntlet2.SetActive(true);
            panels[1].SetActive(true);
            panels[4].SetActive(true);

            PlayerController.playercon.weaponEquipped = true;

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
            case GameState.tutorial:
                _TUTORIAL();
                break;
        }
    }
}
