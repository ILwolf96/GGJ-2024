using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] private Transform playerPos;
    public int currentHp = 80;
    public float movementSpeed = 0.001f;
    public int Attack = 7;
    public int Defence = 3;
    public bool enemyIsDead = false;
    public float rotationSpeed = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        
    }
=======
    public Transform playerTransform;
    public int currentHp = 80;
    public float movementSpeed = 0.001f;
    public int Attack = 7; // do you mean damage?
    public int Defence = 3; // what is this going to be used for?
    public bool enemyIsDead = false; 
    public float rotationSpeed = 5f;
>>>>>>> Stashed changes

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(playerTransform.transform.position, this.transform.position);
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
            Vector3 direction = playerTransform.position - transform.position;
                       
            transform.Translate(direction * movementSpeed * Time.deltaTime);
                       
            float yDistance = Mathf.Abs(playerTransform.position.y - transform.position.y);
                        
            if (yDistance < 0.5)
            {
                Vector3 newPosition = transform.position;
                newPosition.y = playerTransform.position.y;
                transform.position = newPosition;
            }
        }
        if(enemyIsDead)
        {
            Destroy(gameObject);
            Spawner.enemyCount--;
        }
    }
    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }
}
