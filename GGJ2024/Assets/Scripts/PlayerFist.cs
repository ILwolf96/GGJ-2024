using UnityEngine;


public class PlayerFist : Weapon
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            
            Debug.Log("Enmey got hit");
            Attacker.IncreaseCombo();
            _successfulHit = true;
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(_damage,_knockback);
        }

    }

    
}
