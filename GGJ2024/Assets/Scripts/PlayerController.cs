using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent (typeof(BoxCollider2D))]
public class PlayerController : ComboAttacker
{
    [SerializeField] Transform _playerTrans;

    private const float _floorThreshold = -0.52785f;
    public float speed = 4;
    public Vector3 jumpHeight = new Vector3(0, 2.5f, 0);
    public float invunrabiltyInterval;
    private KeyCode _attackKey = KeyCode.E;
    private bool _isInvunrable = false;
    private MyTimer _invurnebltyT;
    private float _laughMeter;
    private bool _isFalling;
    private float _stunDur;
    private bool _isGrounded = true;
    private bool _isJumping = false;
    private Vector3 move;
    private Vector3 groundPos;
    private Vector3 jumpPos;
    private Vector3 gravity = new Vector3 (0,-2.5f,0);
    private Vector3 originalPosition;

    protected override void Start()
    {
        base.Start();
        _comboSize = 3;
        _invurnebltyT = new MyTimer(invunrabiltyInterval);
    }
    protected override void Update()
    {
        if (_isGrounded)
        {
            Move();
            if (Input.GetKey(KeyCode.Space) && !_isFalling && !_isJumping)
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
                move = gravity; Debug.Log("im falling in love tonighttt");
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
    public void Move()
    {
        if (!_isJumping && !_isFalling)
        {
            if (_playerTrans.position.y <= _floorThreshold)
            {
                float value = _playerTrans.position.y + 0.001f;
                if (!(value > _floorThreshold))
                {
                    move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                    transform.Translate(move * speed * Time.deltaTime);
                }
            }
            else
            {
                _playerTrans.position = new Vector3(_playerTrans.position.x, _floorThreshold, 0);
            }
                
        }
        else
        {
            move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.Translate(move * speed * Time.deltaTime);
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
        if (Input.GetKeyDown(_attackKey))
        {
            base.Attack();
            switch (_comboCounter)
            {
                case 1:
                    _weapon.Attack(_damage, 0);
                    Debug.Log("First hit");
                    break;
                case 2:
                    _weapon.Attack(_damage * comboMultiplier, 0);
                    Debug.Log("Second hit");
                    break;
                case 3:
                    _weapon.Attack(_damage * comboMultiplier * comboMultiplier, _knockBack);
                    Debug.Log("Third hit");
                    break;
                default:
                    Debug.Log("I fucked up");
                    break;
            }
        }
    }
    public void TakeDamage()
    {
        if (!_isInvunrable)
        {
            //Things that happen when not invulnerable

        }
    }
    void BoostPlayerDamage()
    {
        //player.damage++
    }
    void BoostPlayerSpeed()
    {
        //player.Speed++
    }
    void BoostPlayerCrit()
    {
        //player.crit++
    }
}
