using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected  ComboAttacker Attacker;
    [SerializeField] protected BoxCollider2D boxCollider;
    [SerializeField] protected Rigidbody2D _rigid;

    protected MyTimer _attackTimer;
    protected float _damage;
    protected float _knockback;
    protected bool _successfulHit = false;
    protected bool _isAttacking = false;

  

    void Awake()
    {
        boxCollider.isTrigger = true;
        _rigid.gravityScale = 0;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider.enabled = false;
        _attackTimer = new MyTimer(Attacker.attackDuration);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_isAttacking)
        {
            _attackTimer.Tick();
        }

        if (_attackTimer.IsOver())
        {
            if (!_successfulHit)
            {
                Attacker.ComboEnd();
            }
            _successfulHit = false;
            _isAttacking = false;
            _attackTimer.Reset();
            boxCollider.enabled = false;
        }

    }

    public void Attack(float damage, float knockback)
    {
        _isAttacking = true;
        _damage = damage;
        _knockback = knockback;
        boxCollider.enabled = true;
    }


    protected abstract void OnTriggerEnter2D(Collider2D collision);
}
