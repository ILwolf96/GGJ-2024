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
        distance = Vector3.Distance(playerTransform.transform.position, this.transform.position);
        
        base.Update();
<<<<<<< Updated upstream

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
=======
        if (distance > attackModeThreshold)
>>>>>>> Stashed changes
        {
            //animator.SetTrigger("Walk1");
            direction = playerTransform.position - transform.position;
            direction.Normalize();

<<<<<<< Updated upstream
            transform.Translate(direction * movementSpeed * Time.deltaTime);

            float yDistance = Mathf.Abs(playerTransform.position.y - transform.position.y);

            if (yDistance < 0.5)
            {
                Vector3 newPosition = transform.position;
                newPosition.y = playerTransform.position.y;
                transform.position = newPosition;
=======

            if (transform.position.y < PlayerController.THRESHOLDS[(int)PlayerController.Directions.North])
            {
                //Good Y space
                if (!(transform.position.y + safeSpace * 5  > THRESHOLDS[(int)PlayerController.Directions.North]))
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

>>>>>>> Stashed changes
            }
        }
        if (enemyIsDead)
        {
            Destroy(gameObject);
            Spawner.enemyCount--;
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
