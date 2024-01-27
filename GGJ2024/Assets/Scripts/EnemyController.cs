using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerController;

public class EnemyController : ComboAttacker
{
    public Transform playerTransform;
    public float currentHp = 80;
    public float movementSpeed = 0.001f;
    public int Defence = 3; // what is this going to be used for?
    public bool enemyIsDead = false;
    public float rotationSpeed = 5f;
    public float attackModeThreshold = 1f;

    private Vector3 direction;

    protected float distance; // Tomer: I need it as a field to check when to attack
    protected override void Start()
    {
        base.Start();
        _comboSize = 2;
    }


    // Update is called once per frame
    protected override void Update()
    {
        distance = Vector3.Distance(playerTransform.transform.position, transform.position);

        base.Update();
        if (distance > attackModeThreshold)
        {
            //animator.SetTrigger("Walk1");
            direction = playerTransform.position - transform.position;
            direction.Normalize();


            if (transform.position.y < PlayerController.THRESHOLDS[(int)PlayerController.Directions.North])
            {
                //Good Y space
                if (!(transform.position.y + safeSpace * 5 > THRESHOLDS[(int)PlayerController.Directions.North]))
                {
                    // Will not go over
                    NormalMovingBehaviour();
                }
                else
                {   // Will go over
                    EdgeMovmentBehaviour();
                }
            }
            else
            {
                EdgeMovmentBehaviour();

            }
        }
    }


    private void NormalMovingBehaviour()
    {
        transform.Translate(movementSpeed * Time.deltaTime * direction);

        //The teleport for small distance in the Y axis
        /* if (Mathf.Abs(playerTransform.position.y - transform.position.y) < 0.5)
         {
             transform.position = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
         }*/
    }

    private void EdgeMovmentBehaviour()
    {
        transform.Translate(new Vector3(direction.x, -1, 0) * movementSpeed * Time.deltaTime);
    }


    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }

    protected override void Attack()
    {
        if (distance <= attackModeThreshold)
        {
            /*animator.ResetTrigger("Walk1");
            AttackAnimation();*/
            base.Attack();
            switch (_comboCounter)
            {
                case 1:
                    _weapon.Attack(_damage, 0);
                    //Debug.Log("First hit");
                    break;
                case 2:
                    _weapon.Attack(_damage * comboMultiplier, 0);
                    //Debug.Log("Second hit");
                    break;
                default:
                    Debug.Log("I fucked up");
                    break;
            }
        }
    }
    public void TakeDamage(float damage, float knockback)
    {
        currentHp -= damage;
        Debug.Log("Hp was lost");
        if (currentHp <= 0)
        {
            Debug.Log("I AM DEAD");
            Die();
        }
        else if (knockback != 0)
        {
            Knockback();
        }
    }
    public void Knockback() //need to check if it needs to change 
    {
        Vector3 newPosition = transform.position;
        //float knockDir = -(playerTransform.position.x - newPosition.x) / (Mathf.Abs(playerTransform.position.x - newPosition.x));
        if (newPosition.x < playerTransform.position.x)
        {
            newPosition.x = transform.position.x - 1f;
            transform.position = newPosition;
        }
        else if (newPosition.x > playerTransform.position.x)
        {
            newPosition.x = transform.position.x + 1f;
            transform.position = newPosition;
        }

    }
    public void Die()
    {
        /*animator.ResetTrigger("Walk1");
        FallAnimation();*/
        Destroy(gameObject);
        Spawner.enemyCount--;
    }



}