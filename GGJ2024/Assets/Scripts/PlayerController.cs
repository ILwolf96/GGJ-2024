using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent (typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController CharController;
   
    public float speed = 4;
    public float jumpHeight;

    private float _laughMeter; // laughMeter
    private float _damage;
    private float _knockBack;
    private float _comboReset;
    private float _stunDur;
    private bool _isGrounded;

    void Update()
    {
        { // Player movement
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        CharController.Move(move * Time.deltaTime * speed);
            move = Vector3.zero;
        }
    }



   public void Jump()
    {
        // Moves the player upwards and allows a jump attack while still in the air 
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
