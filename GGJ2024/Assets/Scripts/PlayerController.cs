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

    private float _hp;
    private float _damage;
    private float _knockBack;
    private float _comboReset;

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        CharController.Move(move * Time.deltaTime * speed);

        //REeset the MoveVector
        move = Vector3.zero;
    }
}
