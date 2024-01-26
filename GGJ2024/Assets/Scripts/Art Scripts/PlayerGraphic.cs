using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphic : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Animation Movement;
    [SerializeField] Animation Attack;
    [SerializeField] Animation Hit;
    [SerializeField] Animation Stun;
    [SerializeField] Animation Jump;

    public void StartMovementAnimation()
    {
        Movement.Play();
    }
    public void StartAttackAnimation()
    {
        Attack.Play();
    }
    public void StartHitAnimation()
    {
        Hit.Play(); 
    }
    public void StartStunAnimation()
    {
        Stun.Play();    
    }
    public void StartJumpAnimation()
    {
        Jump.Play(); 
    }
}
