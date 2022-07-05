using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDialogue : MonoBehaviour
{
    float timer = 0f;
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
        timer += Time.deltaTime;
        
        if(timer > waitTime)
        {
            if (seqNum <= PlayerController.playercon.endDialogue)
            {
                GameController.instance.PannelMessage(5, seqNum, true, 2);
                seqNum++;
                timer = 0;
            }
            else
            {
                GameController.instance.PannelMessage(5, 0, false, 2);
            }
        }
    }
}
