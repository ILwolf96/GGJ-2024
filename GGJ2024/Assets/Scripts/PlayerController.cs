using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
[RequireComponent (typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController CharController;

   
    public float speed = 4;
    public Vector3 jumpHeight = new Vector3(0, 2.5f, 0);
    public Vector3 gravity = new Vector3(0, -2.5f, 0);

    private float _laughMeter; // laughMeter
    private float _damage;
    private float _knockBack;
    private float _comboReset;
    private float _stunDur;
    private bool _isGrounded = true;
    private bool _isJumping = false;
    private Vector3 move;
    private Vector3 groundPos;
    private Vector3 jumpPos;

    private Vector3 originalPosition;

    void Update()
    {
        if (_isGrounded)
        {
            Move();
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
            
        }
        else if (_isJumping)
        {
            CharController.Move(gravity * Time.deltaTime);

            if (jumpPos.y == originalPosition.y)
            {
                _isGrounded = true;
                _isJumping = false;
            }
        }


    }
    public void Move()
    {
        // Player movement
        move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        CharController.Move(move * Time.deltaTime * speed);
        move = Vector3.zero;
    }

    public void Jump()
    {
        // Moves the player upwards and allows a jump attack while still in the air
        originalPosition = this.transform.position;
        CharController.Move(jumpHeight);
        
        if (this.transform.position.y >= (originalPosition.y + jumpHeight.y))
        {
            _isJumping = true;
            _isGrounded = false;
            jumpPos = this.transform.position;
        }
        



    }

    public void Attack() 
    {
        // Creates an attack that hits enemies infront of the Character
    }

    public void TakeDamage()
    {
        // if a character has been attacked by an enemy should invoke TakeDamage in the player script (called by enemey script Attack())
    }

    public void IncreaseCombo()
    {
        // increases the combo depending on if combo didn't reset and and the player hit a target 
    }

    

}
