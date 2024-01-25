using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float speedE;

    private float _damageE;
    
    private float _hpE;
    
    private float _stunDurE;

    private float _knockBack;

    private float _rewardChance;

    void Update()
    {
        
    }

    public void Attack()
    {
        // Creates an attack that hits the Player, trigger only when player is in the area
    }
    public void TakeDamage()
    {
        // if an enemy has been attacked by a character should invoke TakeDamage in the enemyscript script (called by PlayerController script Attack())
    }
    public void IncreaseLM()
    {
        // when enemy dies incrase Player LM
    }

}
