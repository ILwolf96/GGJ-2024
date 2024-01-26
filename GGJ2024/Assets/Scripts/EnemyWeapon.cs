using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyWeapon : Weapon
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Debug.Log("Player got hit");
            Attacker.IncreaseCombo();
            _successfulHit = true;
            //collision.gameObject.GetComponent<PlayerContoller>().TakeDamage(_damage,_knockback);
        }
    }
}
