using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

//[RequireComponent(typeof(CharacterController))]
[RequireComponent (typeof(CapsuleCollider2D))]
public class PlayerController : ComboAttacker
{
    //[SerializeField] CharacterController CharController;
    

    public float speed = 4;
    public Vector3 jumpHeight = new Vector3(0, 2.5f, 0);
    public Vector3 gravity = new Vector3(0, -2.5f, 0);

    
    public float invunrabiltyInterval;
    
    private KeyCode _attackKey = KeyCode.E;
    
    private bool _isInvunrable = false;
   
    
    private MyTimer _invurnebltyT;

    private float _laughMeter; // laughMeter
   
    private float _stunDur;
    private bool _isGrounded = true;
    private bool _isJumping = false;
    private Vector3 move;
    private Vector3 groundPos;
    private Vector3 jumpPos;


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
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
                while (_isJumping == true)
                { //CharController.Move(jumpHeight); _isJumping = false; 
                }
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

        // Comabt calculations
        base.Update();

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
        //CharController.Move(move * Time.deltaTime * speed);
        move = Vector3.zero;
    }

    public void Jump()
    {
        _isJumping = true;
        // Moves the player upwards and allows a jump attack while still in the air
        //originalPosition = this.transform.position;
        //CharController.Move(jumpHeight);
        if (this.transform.position.y >= (originalPosition.y + jumpHeight.y))
        {
            
            _isGrounded = false;
            jumpPos = this.transform.position;
        }
       
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
}
