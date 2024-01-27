using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerController;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : ComboAttacker
{
    private PlayerController _player;
    public float currentHp = 80;
    public float movementSpeed = 0.001f;
    public float MeterPointToAdd;

    public int Defence = 3; // what is this going to be used for?
    public bool enemyIsDead = false;
    public float rotationSpeed = 5f;
    public float attackModeThreshold = 1f;
    private LaughMeter _laughMeter;

    
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
        distance = Vector3.Distance(_player.transform.position, transform.position);
        
        base.Update();

        direction = _player.transform.position - transform.position;
        direction.Normalize();

        WhereToLookAt();

        if (distance > attackModeThreshold)
        {
                //animator.SetTrigger("Walk1");
                if (transform.position.y < PlayerController.THRESHOLDS[(int)PlayerController.Directions.North])
                {
                    //when in good space
                    if (!(transform.position.y + safeSpace * 5 > THRESHOLDS[(int)PlayerController.Directions.North]))
                    {
                        // when below north threshold
                        NormalMovingBehaviour();
                    }
                    else
                    {   
                    //when above north threshold in the future
                        EdgeMovmentBehaviour();
                    }
                }
                else
                {
                //when in bad space
                    EdgeMovmentBehaviour();

                }
        }

    }
   
    private void WhereToLookAt()
    {
        if(direction.x < 0)
        {
            //look left
            transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));

        }
        else
        {
            //look right
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
    }
   
    private void NormalMovingBehaviour()
    {
        transform.Translate(movementSpeed * Time.deltaTime * direction);
    }

    private void EdgeMovmentBehaviour()
    {
        transform.Translate(new Vector3(direction.x, -1, 0) * movementSpeed * Time.deltaTime);
    }


    public void SetPlayer(PlayerController player)
    {
        _player = player;
    }

    public void SetLaughMeter(LaughMeter laughMeter)
    {
        _laughMeter = laughMeter;
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
                    _weapon.Attack(_damage * comboMultiplier, _knockBack);
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
        Debug.Log("takes damage");
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
        else if (knockback != 0)
        {
            Knockback(knockback);
        }
    }
    private void Knockback(float knockback) //need to check if it needs to change 
    {
        Debug.Log("Enemy Knockback");
        Vector3 newPosition = transform.position;
        float knockDir = (-(_player.transform.position.x - newPosition.x)) / Mathf.Abs(_player.transform.position.x - newPosition.x);
        newPosition.x = transform.position.x + knockback * knockDir;
        transform.position = newPosition;
    }
    private void Die()
    {
        /*animator.ResetTrigger("Walk1");
        FallAnimation();*/
        _laughMeter.gainLaugh(MeterPointToAdd); //Laugth meter handling
        Spawner.enemyCount--;
        Destroy(gameObject);
    }



}