using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public GameObject player;
    public GameObject can;
    public  Slider slider;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (slider.value >= 100)
        {
            can.SetActive(true);
            Time.timeScale = 0; // pause game to select boost
        }
    }

    public void DamageBoost()
    {
        //player.BoostPlayerDamage();
        print("damage boost");
        can.SetActive(false);
        Time.timeScale = 1; // resume game
        slider.value = 50; // xp reset
    }

    public void SpeedBoost()
    {
        //player.BoostPlayerSpeed()
        print("speed boost");
        can.SetActive(false);
        Time.timeScale = 1; // resume game
        slider.value = 50; // xp reset
    }

    public void CritChance()
    {
        //player.BoostPlayerCrit()
        print("crit chance boost");
        can.SetActive(false);
        Time.timeScale = 1; // resume game
        slider.value = 50; // xp reset
    }


}