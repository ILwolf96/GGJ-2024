using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent (typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController CharController;
    [SerializeField] PlayerFist PlayerFist;
    public float speed = 4;
    public float jumpHeight;
    public float punchTime;
    public float attackInterval;

    [SerializeField] float _comboMultiplier;
    [SerializeField] float _comboWindow;
    [SerializeField] float _invulnerabilityInterval;

    private KeyCode _attackKey = KeyCode.E;
    private float _laughMeter; // laughMeter
    private float _damage;
    private float _knockback;
    private float _comboCounter = 0;
    
    private bool _isInCombo = false;
    
    private float _stunDur;
    private bool _isGrounded;
    
    private bool _canAttack = true;
    private bool _isInvulnerable = false;

    private MyTimer _attackTimer;
    private MyTimer _comboTimer;
    private MyTimer _invulnerabilityTimer;

    void Start()
    {
        _attackTimer = new MyTimer(attackInterval);
        _comboTimer = new MyTimer(_comboWindow);
        _invulnerabilityTimer = new MyTimer(_invulnerabilityInterval);
    }

    void Update()
    {
        { // Player movement
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            CharController.Move(move * Time.deltaTime * speed);
            move = Vector3.zero;
        }

        { // Attack interval timing

            //Waiting for the attack interval to end
            if (_attackTimer.IsOver())
            {
                _attackTimer.Reset();
                _canAttack = true;
            }
            if (_canAttack)
            {
                // Checking for attack input
                if (Input.GetKeyDown(_attackKey))
                {
                    if (_isInCombo)
                    {
                        _comboTimer.Reset();
                    }
                    Attack();
                }
            }
            else
            {
                _attackTimer.Tick();
            }
        }

        { // Combo timing
            if (_isInCombo && _canAttack)
            {
                _comboTimer.Tick();
                //Missed the window to chain combo
                if (_comboTimer.IsOver())
                {
                    _comboTimer.Reset();
                    ComboEnd();
                }
            }
        }

        { // Invulnerability timing
            if (_isInvulnerable)
            {
                _invulnerabilityTimer.Tick();
                
                if (_invulnerabilityTimer.IsOver())
                {
                    _invulnerabilityTimer.Reset();
                    _isInvulnerable = false;
                }
            }
        }
        
    }

   public void Jump()
    {
        // Moves the player upwards and allows a jump attack while still in the air 
    }

    public void Attack() 
    {
        // Creates an attack that hits enemies infront of the Character
        _canAttack = false;
        switch (_comboCounter)
        {
            case 0:
                PlayerFist.Attack(_damage, 0);
                Debug.Log("First hit");
                break;
            case 1:
                PlayerFist.Attack(_damage * _comboMultiplier, 0);
                Debug.Log("Second hit");
                break;
            case 2:
                PlayerFist.Attack(_damage * _comboMultiplier * _comboMultiplier, _knockback);
                Debug.Log("Third hit");
                break;
            default:
                Debug.Log("I fucked up");
                break;
        }
        
    }

    public void TakeDamage()
    {
        // if a character has been attacked by an enemy should invoke TakeDamage in the player script (called by enemey script Attack())
        if (!_isInvulnerable)
        {
            //The rest of the code
            _isInvulnerable = true;
        }
    }

    public void IncreaseCombo()
    {
        // increases the combo depending on if combo didn't reset and and the player hit a target 
        _isInCombo = true;
        _comboCounter++;
        if (_comboCounter > 2)
        {
            ComboEnd();
        }
        
    }

    public void ComboEnd()
    {
        Debug.Log("Combo Over");
        _comboCounter = 0;
        _isInCombo = false;
    }

}
