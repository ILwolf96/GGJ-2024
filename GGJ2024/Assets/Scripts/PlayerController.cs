using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
[RequireComponent (typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController charController;
    [SerializeField] PlayerFist playerFist;

    public float speed = 4;
    public Vector3 jumpHeight = new Vector3(0, 2.5f, 0);

  
    // attack varible
    public float punchTime;
    public float attackInterval;
    public float comboMultiplier;
    public float comboWindow;
    public float invunrabiltyInterval;
    private float _comboCounter = 0;
    private KeyCode _attackKey = KeyCode.E;
    private bool _isInCombo = false;
    private bool _canAttack = true;
    private bool _isInvunrable = false;

    private MyTimer _comboT;
    private MyTimer _attackT;
    private MyTimer _invurnebltyT;

    private float _laughMeter; // laughMeter
    private float _damage;
    private float _knockBack;

    private bool _isFalling;

    private float _stunDur;
    private bool _isGrounded = true;
    private bool _isJumping = false;
    private Vector3 move;
    private Vector3 groundPos;
    private Vector3 jumpPos;
    private Vector3 gravity = new Vector3 (0,-2.5f,0);


    private Vector3 originalPosition;

    private void Start()
    {
        _comboT = new MyTimer(comboWindow);
        _attackT = new MyTimer(attackInterval);
        _invurnebltyT = new MyTimer(invunrabiltyInterval);
    }
    void Update()
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
                charController.Move(jumpHeight * Time.deltaTime);
                
                if (transform.position.y >= originalPosition.y + jumpHeight.y)
                    { _isJumping = false; _isFalling = true;}

                }
                if (_isFalling)
                {
                move = gravity; Debug.Log("im falling inlove tonighttt");
                charController.Move(move * Time.deltaTime);
                
                    if (transform.position.y <= originalPosition.y)
                    { _isFalling = false; }
                }
               

            }
            
        else if (_isJumping)
        {
            //CharController.Move(gravity * Time.deltaTime);

            if (jumpPos.y == originalPosition.y)
            {
                _isGrounded = true;
                _isJumping = false;
            }
        }
        { // Attack interval timing

            //Waiting for the attack interval to end
            if (_attackT.IsOver())
            {
                _attackT.Reset();
                _canAttack = true;
            }
            if (_canAttack)
            {
                // Checking for attack input
                if (Input.GetKeyDown(_attackKey))
                {
                    if (_isInCombo)
                    {
                        _attackT.Reset();
                    }
                    Attack();
                }
            }
            else
            {
                _attackT.Tick();
            }
        }

        { // Combo timing
            if (_isInCombo && _canAttack)
            {
                _comboT.Tick();
                //Missed the window to chain combo
                if (_comboT.IsOver())
                {
                    _comboT.Reset();
                    Debug.Log("at timer");
                    ComboEnd();
                }
            }
        }

        { // Invulnerability timing
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

    }
    public void Move()
    {
        // Player movement
        move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        charController.Move(move * Time.deltaTime * speed);
        move = Vector3.zero;
    }

    public Vector3 Jump()
    {
        originalPosition = transform.position;
        _isJumping = true;
        return originalPosition;
    }

    public void Attack() 
    {
        _canAttack = false;
        switch (_comboCounter)
        {
            case 0:
                playerFist.Attack(_damage, 0);
                Debug.Log("First hit");
                break;
            case 1:
                playerFist.Attack(_damage * comboMultiplier, 0);
                Debug.Log("Second hit");
                break;
            case 2:
                playerFist.Attack(_damage * comboMultiplier * comboMultiplier, _knockBack);
                Debug.Log("Third hit");
                break;
            default:
                Debug.Log("I fucked up");
                break;
        }
    }

    public void TakeDamage()
    {
        if (!_isInvunrable)
        {
            //The rest of the code
            _isInvunrable = true;
        }
    }

    public void IncreaseCombo()
    {
        {
            // increases the combo depending on if combo didn't reset and and the player hit a target 
            Debug.Log("Combo Increased" + _comboCounter);
            _isInCombo = true;
            _comboCounter++;
            if (_comboCounter > 2)
            {
                Debug.Log("at increase");
                ComboEnd();
            }
        }

    }

    public void ComboEnd()
    {
        Debug.Log("Combo Over");
        _comboCounter = 0;
        _isInCombo = false;
    }
}
