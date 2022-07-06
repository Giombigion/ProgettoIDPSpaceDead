using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public AudioController audioController;
    public MenuController menuController;

    public GameState state;

    public GameObject[] panels;
    [SerializeField] Testi _testi;

    //Variabile per la gestione dei Chip
    public GameObject[] chipUI;

    [SerializeField] public int idlevel; //Variabile per l'assegnazione di un valore ad ogni livello

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

    [SerializeField] GameObject Nemici; 

    [Range(0,15)]
    public float InteractiveOBJDistance;

    public GameObject target;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        initLevel(idlevel);

        //hidemouse();

        //Sceglie lo stato che permette di giocare
        state = GameState.menu;

        //Disattiva all'avvio tutti i panel eccetto quello della stamina
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        if (idlevel == 1)
        {
            PannelMessage(5, 0, true, 2);
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

        if (state == GameState.play && Input.GetKeyDown(KeyCode.Escape))
        {
            menuController.MenuPanels[0].SetActive(true);
            state = GameState.menu;
        }

        if (currentStamina < 0)
        {
            state = GameState.dead;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p">Indice del pannello</param>
    /// <param name="element">Elementi nel pannello</param>
    public void PannelMessage(int p, int element, bool active, int type)
    {
        panels[p].SetActive(active);

        if (type == 1)
        {
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
        if (type == 2)
        {
            panels[p].transform.GetChild(0).GetComponent<Text>().text = _testi.dialoghi[element].narratore;

            try
            {
                panels[p].transform.GetChild(1).GetComponent<Text>().text = _testi.dialoghi[element].dialogo;
            }
            catch
            {
                print("Non esiste l'elemento per il testo");
            }
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
        audioController.PlaySound(audioController.sourceSFX, "RicaricaVita");
        currentStamina += h;
        currentStamina = Mathf.Clamp(currentStamina, 0, 100);
        StaminaData.text = currentStamina.ToString();
    }

    //Metodo danno al player
    public void TakeDemage(int demageAmount)
    {
        audioController.PlaySound(audioController.sourcePlayer, "DannoPlayer");
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
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
    }

    //-----STATI DI GIOCO----------------------------------------------------------------------------------------
    public void _MENU()
    {
        panels[4].SetActive(false);
    }

    //Metodo dedicato allo stato di IDLE
    public void _IDLE()
    {

    }

    //Metodo dedicato allo stato di PLAY
    public void _PLAY()
    {
        Nemici.SetActive(true);
        panels[4].SetActive(true);
    }

    //Metodo dedicato allo stato di DEAD
    public void _DEAD()
    {
        Nemici.SetActive(false);
        state = GameState.menu;
        Debug.Log("Sei morto!");
        Invoke("RealDead", 0.1f);
    }

    void RealDead() 
    {
        target.transform.position = checkPoints[idlevel].position;
        currentStamina = 5;
        panels[6].SetActive(true);
    }

    //Metodo dedicato allo stato di PAUSE
    public void _PAUSE()
    {
        //print("Sono in Pause");
    }
    public void _TAKE()
    {
        //print("Sono in Take");
        audioController.PlaySound(audioController.sourceSFX, "G.U.Ant-0_Voice");

        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gauntlet.gameObject);
            audioController.PlaySound(audioController.sourceSFX, "Testo_Guanto");
            //audioController.Play("TestoGuanto");
            PannelMessage(0, 0, false, 1);
            state = GameState.tutorial;
        }
    }

    public void _TUTORIAL()
    {
        PannelMessage(5, 0, true, 2);

        /*if (Input.GetKeyDown(KeyCode.E))
        {*/
            //audioController.Play("EquipaggiamentoGuanto"); //QUI NON VA BENE QUESTO, PERCHE' VERRA' ESEGUITO OGNI VOLTA CHE SI ESCE DA UN PANNELLO. PERO' FUNZIONA SOLO SE MESSO QUI. DA CONTROLLARE
            //PannelMessage(0, 1, false);

            //Distrugge il collider;
            Destroy(gauntlet2.GetComponent<BoxCollider>());

            gauntlet2.SetActive(true);
            panels[1].SetActive(true);
            panels[4].SetActive(true);

            PlayerController.playercon.weaponEquipped = true;

            state = GameState.play;
        /*}*/
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
            case GameState.menu:
                _MENU();
                break;
        }
    }
}
