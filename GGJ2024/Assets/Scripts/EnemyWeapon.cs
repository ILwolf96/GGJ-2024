using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyWeapon : Weapon
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Attacker.IncreaseCombo();
            _successfulHit = true;
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(_damage,_knockback,Attacker.transform.position);
        }
    }
}
