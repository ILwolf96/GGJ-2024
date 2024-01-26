using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : ComboAttacker
{
    public Transform playerTransform;
    public int currentHp = 80;
    public float movementSpeed = 0.001f;
    public int Damage = 7; // do you mean damage?
    public int Defence = 3; // what is this going to be used for?
    public bool enemyIsDead = false;
    public float rotationSpeed = 5f;

    protected float distance; // Tomer: I need it as a field to check when to attack
    protected override void Start()
    {
        base.Start();
        _comboSize = 2;
    }
    

    // Update is called once per frame
    protected override void Update()
    {
        distance = Vector3.Distance(playerTransform.transform.position, this.transform.position);
        
        base.Update();

        if (currentHp <= 0)
        {
            /*animator.ResetTrigger("Walk1");
            FallAnimation();*/
            //player.UpdateScore(player.scoreCount);
        }
        if (distance > 0.1f)
        {


        }
        if (distance > 2)
        {
            //animator.SetTrigger("Walk1");
            Vector3 direction = playerTransform.position - transform.position;

            transform.Translate(direction * movementSpeed * Time.deltaTime);

            float yDistance = Mathf.Abs(playerTransform.position.y - transform.position.y);

            if (yDistance < 0.5)
            {
                Vector3 newPosition = transform.position;
                newPosition.y = playerTransform.position.y;
                transform.position = newPosition;
            }
        }
        if (enemyIsDead)
        {
            Destroy(gameObject);
            Spawner.enemyCount--;
        }
    }
    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }

    protected override void Attack()
    {
        if (distance <= 2)
        {
            /*animator.ResetTrigger("Walk1");
            AttackAnimation();*/
            base.Attack();
            switch (_comboCounter)
            {
                case 1:
                    _weapon.Attack(Damage, 0);
                    Debug.Log("First hit");
                    break;
                case 2:
                    _weapon.Attack(Damage * comboMultiplier, 0);
                    Debug.Log("Second hit");
                    break;
                default:
                    Debug.Log("I fucked up");
                    break;
            }
        }
    }
    public void TakeDamage(float damage, float knockback)
    {
        // if an enemy has been attacked by a character should invoke TakeDamage in the enemyscript script (called by PlayerController script Attack())
    }




}
