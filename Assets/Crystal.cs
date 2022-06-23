using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public int crystalHealth;
    public float healTime;
    public int healValue;
    float timer;

    public static Crystal cr;

    void Start()
    {
        cr = this;
    }

    private void Update()
    {
        print(crystalHealth);

        timer += Time.deltaTime;
    }

    public void crHeal()
    {
        if(PlayerController.playercon.weaponEquipped && (timer > healTime) && (GameController.instance.currentStamina < 100))
        {
            crystalHealth--;
            GameController.instance.heal(healValue);
            timer = 0;
        }
    }
}