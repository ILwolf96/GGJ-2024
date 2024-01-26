using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerFist : MonoBehaviour
{
    [SerializeField] PlayerController Player;
    [SerializeField] BoxCollider2D BoxCollider2D;
    Timer _punchTimer;
    float _damage;
    bool _enemyHit = false;
    bool _isPunching = false;
    
    public void Attack(float damage,float knockback)
    {
        _isPunching = true;
        _damage = damage;
        BoxCollider2D.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D.enabled = false;
        _punchTimer =new Timer(Player.punchTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isPunching)
        {
            _punchTimer.Tick();
        }
        
        if (_punchTimer.IsOver())
        {
            if (!_enemyHit)
            {
                Player.ComboEnd();
            }
            _enemyHit = false;
            _isPunching = false;
            _punchTimer.Reset();
            BoxCollider2D.enabled = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            
            Debug.Log("Enmey got hit");
            Player.IncreaseCombo();
            _enemyHit = true;
            //collision.gameObject.GetComponent<EnemyScript>().TakeDamage(_damage,knockback);
        }

    }

    
}
