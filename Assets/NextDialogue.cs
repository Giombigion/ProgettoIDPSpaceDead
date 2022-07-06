using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDialogue : MonoBehaviour
{
    public float timer = 0f;
    public float waitTime;
    public int seqNum = 1;

    public static NextDialogue nd;

    private void Awake()
    {
        nd = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; //conta il tempo trascorso dall'ultima interazione
        
        if(timer > waitTime) //controlla se è passato il tempo richiesto
        {
            if (seqNum <= PlayerController.playercon.endDialogue) //controlla se ci sono dialoghi disponibili
            {
                GameController.instance.PannelMessage(5, seqNum, true, 2); //attiva il primo dialogo della serie
                print("Ho aperto il dialogo numero " + seqNum);
                seqNum++; //prepara le variabili per il prossimo ciclo
                print("Preparo il dialogo numero " + seqNum);
                timer = 0; //azzera il timer
            }
            else
            {
                GameController.instance.PannelMessage(5, 0, false, 2); //disattiva il pannello
            }
        }
    }
}
