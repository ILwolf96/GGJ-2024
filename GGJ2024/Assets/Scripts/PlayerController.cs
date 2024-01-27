using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//[RequireComponent(typeof(CharacterController))]
[RequireComponent (typeof(CapsuleCollider2D))]
public class PlayerController : ComboAttacker
{
<<<<<<< Updated upstream
    //[SerializeField] CharacterController CharController;
    
=======
   
    public enum Directions
    {
        North, South, West, East
    }
    public static float[] THRESHOLDS = { -0.62f, -3.33f, -8.57f, 8.5f };
    public static  float safeSpace = 0.01f;

    [SerializeField] Transform playerTransform;

>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
        // Player movement
        move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        //CharController.Move(move * Time.deltaTime * speed);
        move = Vector3.zero;
=======
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
            if (Input.GetKey(KeyCode.RightArrow))
            {
                CheckMovement(KeyCode.RightArrow, 1);
            }
            move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
           
        }
        
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======

    void CheckMovement(KeyCode key,float speedMult)
    {
        switch (key)
        {
            case (KeyCode.UpArrow):
                if (transform.position.y <= THRESHOLDS[(int)Directions.North])
                {
                    
                    if (!(transform.position.y + safeSpace  > THRESHOLDS[(int)Directions.North]))
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
                        transform.Translate(new Vector3(Input.GetAxis("Horizontal"),0 , 0) * speed * speedMult * Time.deltaTime); 
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
>>>>>>> Stashed changes
}
