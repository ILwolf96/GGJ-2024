using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : ComboAttacker
{

    public enum Directions
    {
        North, South, West, East
    }
    public static float[] THRESHOLDS = { -0.62f, -3.33f, -8.57f, 8.5f };
    public static float safeSpace = 0.01f;

    [SerializeField] Transform playerTransform;

    public float pushDistance;
    public float pushVelocity;
    public float speed = 4;
    public Vector3 jumpHeight = new Vector3(0, 2.5f, 0);
    public float invunrabiltyInterval;
    private KeyCode _attackKey = KeyCode.E;
    private bool _isPushed = false;
    private float _pushDirection;
    private bool _isInvunrable = false;
    private MyTimer _invurnebltyT;
    private float _laughMeter;
    private bool _isFalling = false;
    private float _stunDur;
    private bool _isGrounded = true;
    private bool _isJumping = false;
    public float MeterPointsToDecrease;
    
    private Vector3 move;
    private Vector3 groundPos;
    private Vector3 jumpPos;
    private Vector3 gravity = new Vector3(0, -2.5f, 0);
    private Vector3 originalPosition;
    public LaughMeter laughMeter;   



    public bool isAirborne()
    {
        return _isJumping || _isFalling;
    }

    protected override void Start()
    {
        base.Start();
        _comboSize = 3;
        _invurnebltyT = new MyTimer(invunrabiltyInterval);
    }
    protected override void Update()
    {
        if (_isPushed)
        {
            //Pushed left
            if(_pushDirection < 0)
            {
                if (transform.position.x >= THRESHOLDS[(int)Directions.West])
                {

                    if (!(transform.position.x - safeSpace < THRESHOLDS[(int)Directions.West]))
                    {
                        Pushed();
                    }
                    else
                    {
                        _isPushed = false;
                    }
                    
                }
                else
                {
                    transform.position = new Vector3(THRESHOLDS[(int)Directions.West], transform.position.y, transform.position.z);
                    _isPushed = false;

                }

            }
            //Pushed left
            else
            {
                if (transform.position.x <= THRESHOLDS[(int)Directions.East])
                {

                    if (!(transform.position.x + safeSpace > THRESHOLDS[(int)Directions.East]))
                    {
                        Pushed();
                    }
                    else
                    {
                        _isPushed = false;
                    }
                }
                else
                {
                    transform.position = new Vector3(THRESHOLDS[(int)Directions.East], transform.position.y, transform.position.z);
                    _isPushed = false;
                }

            }



           
        }
        else if (_isGrounded)
        {
            Move();

            if (Input.GetKey(KeyCode.Space) && !isAirborne())
            {
                originalPosition = Jump();
                
            }
            if (_isJumping)
            {
                transform.Translate(jumpHeight * Time.deltaTime);

                if (transform.position.y >= originalPosition.y + jumpHeight.y)
                { _isJumping = false; _isFalling = true; }

            }
            if (_isFalling)
            {
                move = gravity;
                transform.Translate(move * Time.deltaTime);

                if (transform.position.y <= originalPosition.y)
                { _isFalling = false; }
            }
        }
        else if (_isJumping)
        {
            transform.Translate(gravity * Time.deltaTime);

            if (jumpPos.y == originalPosition.y)
            {
                _isGrounded = true;
                _isJumping = false;
            }
        }
        // Comabt calculations
        base.Update();

        // Invulnerability timing
        if (_isInvunrable)
        {
            _invurnebltyT.Tick();

            if (_invurnebltyT.IsOver())
            {
                _invurnebltyT.Reset();
                _isInvunrable = false;
            }
        }
    }

    public void Pushed()
    {
        transform.Translate(new Vector3(_pushDirection, 0, 0) * pushVelocity * Time.deltaTime);
        if (Math.Abs(originalPosition.x - transform.position.x) >= pushDistance)
        {
            _isPushed = false;
        }
    }

    public void Move()
    {
        if (!_isJumping && !_isFalling)
        {
            //down left
            if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                CheckMovement(KeyCode.DownArrow, 0.8f);
                CheckMovement(KeyCode.LeftArrow, 0.8f);
            }
            //down right
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                CheckMovement(KeyCode.DownArrow, 0.8f);
                CheckMovement(KeyCode.RightArrow, 0.8f);
            }
            //up right
            else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                CheckMovement(KeyCode.UpArrow, 0.8f);
                CheckMovement(KeyCode.RightArrow, 0.8f);
            }
            //up left
            else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                CheckMovement(KeyCode.UpArrow, 0.8f);
                CheckMovement(KeyCode.LeftArrow, 0.8f);
            }
            //Only up
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                CheckMovement(KeyCode.UpArrow, 1);
            }
            //Only down
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                CheckMovement(KeyCode.DownArrow, 1);
            }
            //Only left
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                CheckMovement(KeyCode.LeftArrow, 1);
            }
            //Only right
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                CheckMovement(KeyCode.RightArrow, 1);
            }

        }
        else
        {
            // only left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                CheckMovement(KeyCode.LeftArrow, 1);
            }
            // only right
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                CheckMovement(KeyCode.RightArrow, 1);
            }
            move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        }

    }
    public Vector3 Jump()
    {
        originalPosition = transform.position;
        _isJumping = true;
        return originalPosition;
    }
    protected override void Attack()
    {
       
        if (Input.GetKeyDown(_attackKey) && !isAirborne())
        {
            base.Attack();
            switch (_comboCounter)
            {
                case 1:
                    _weapon.Attack(_damage, 0);
                    break;
                case 2:
                    _weapon.Attack(_damage * comboMultiplier, 0);
                    break;
                case 3:
                    _weapon.Attack(_damage * comboMultiplier * comboMultiplier, _knockBack);
                    break;
                default:
                    Debug.Log("I fucked up");
                    break;
            }
        }
        else if(Input.GetKeyDown(_attackKey))
        {
            Debug.Log("Airborne attack");
        }
    }
    public void TakeDamage(float damage, float knockback ,Vector3 enemyPosition)
    {
       
        if (!_isInvunrable)
        {
            //Things that happen when not invulnerable

            //knockback handling
            if (knockback != 0)
            {
                Knockback(knockback, enemyPosition);
            }

            laughMeter.loseLaugh(MeterPointsToDecrease);


        }
    }
    private void Knockback(float knockback, Vector3 enemyPosition) 
    { 
        _isPushed = true;
        _isInvunrable = true;
        originalPosition = transform.position;
        _pushDirection = (-(enemyPosition.x - transform.position.x)) / Mathf.Abs(enemyPosition.x - transform.position.x);
    }
    void CheckMovement(KeyCode key, float speedMult)
    {
        switch (key)
        {
            case (KeyCode.UpArrow):
                if (transform.position.y <= THRESHOLDS[(int)Directions.North])
                {

                    if (!(transform.position.y + safeSpace > THRESHOLDS[(int)Directions.North]))
                    {
                        transform.Translate(new Vector3(0, Input.GetAxis("Vertical"), 0) * speed * speedMult * Time.deltaTime);
                    }

                }
                else
                {
                    transform.position = new Vector3(transform.position.x, THRESHOLDS[(int)Directions.North], transform.position.z);

                }
                break;

            case (KeyCode.DownArrow):
                if (transform.position.y >= THRESHOLDS[(int)Directions.South])
                {

                    if (!(transform.position.y - safeSpace < THRESHOLDS[(int)Directions.South]))
                    {
                        transform.Translate(new Vector3(0, Input.GetAxis("Vertical"), 0) * speed * speedMult * Time.deltaTime);
                    }

                }
                else
                {
                    transform.position = new Vector3(transform.position.x, THRESHOLDS[(int)Directions.South], transform.position.z);

                }
                break;
            case (KeyCode.RightArrow):

                if (transform.position.x <= THRESHOLDS[(int)Directions.East])
                {

                    if (!(transform.position.x + safeSpace > THRESHOLDS[(int)Directions.East]))
                    {
                        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, 0) * speed * speedMult * Time.deltaTime);
                    }

                }
                else
                {
                    transform.position = new Vector3(THRESHOLDS[(int)Directions.East], transform.position.y, transform.position.z);

                }
                break;

            case (KeyCode.LeftArrow):

                if (transform.position.x >= THRESHOLDS[(int)Directions.West])
                {

                    if (!(transform.position.x - safeSpace < THRESHOLDS[(int)Directions.West]))
                    {
                        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, 0) * speed * speedMult * Time.deltaTime);
                    }

                }
                else
                {
                    transform.position = new Vector3(THRESHOLDS[(int)Directions.West], transform.position.y, transform.position.z);

                }
                break;

        }
    }




    public void BoostPlayerDamage()
    {
        //player.damage++
    }
    public void BoostPlayerSpeed()
    {
        //player.Speed++
    }
    public void BoostPlayerCrit()
    {
        //player.crit++
    }
}