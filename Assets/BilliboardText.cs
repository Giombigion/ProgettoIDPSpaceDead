using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilliboardText : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    bool isEnabled;
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.InteractiveOBJDistance > 0)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < GameController.instance.InteractiveOBJDistance)
            {
                isEnabled = true;
            }
            else
            {
                isEnabled = false;
            }
            transform.LookAt(player.transform);
        }
        else {
            isEnabled = false;
        }

        transform.GetComponent<MeshRenderer>().enabled = isEnabled;
    }
}
