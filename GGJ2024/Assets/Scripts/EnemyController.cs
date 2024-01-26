using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform playerPos;
    public int currentHp = 80;
    public float movementSpeed = 0.001f;
    public int Attack = 7; // do you mean damage?
    public int Defence = 3; // what is this going to be used for?
    public bool enemyIsDead = false; 
    public float rotationSpeed = 0.3f;
    
    private GameObject _player;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        playerPos = _player.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(playerPos.transform.position, this.transform.position);
        if (distance <= 2)
        {
            /*animator.ResetTrigger("Walk1");
            AttackAnimation();*/
        }
        if (currentHp <= 0)
        {
            /*animator.ResetTrigger("Walk1");
            FallAnimation();*/
            //player.UpdateScore(player.scoreCount);
        }
        if (distance > 0.1f )
        {
            
            
        }
        if (distance > 2)
        {
            //animator.SetTrigger("Walk1");
            Vector3 direction = playerPos.position - transform.position;
            transform.Translate(direction * movementSpeed * Time.deltaTime);
        }
    }
}
