using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
<<<<<<< Updated upstream
    [SerializeField] float spawnTime = 1.5f;
    [SerializeField] float distance = 5f;
    private bool spawnOnLeftSide = true;
=======
    [SerializeField] EnemyController enemyController;
    [SerializeField] Transform playerTransform;
   
    public float spawnTime = 10.5f;
    public static int maxEnemyAmount = 10;
    public static int enemyCount = 0;
    private bool _spawnOnLeftSide = true;
   
>>>>>>> Stashed changes

    void Start()
    {
        StartCoroutine(Spawn());
    }
<<<<<<< Updated upstream
    IEnumerator Spawn()
    {
        while (true)
=======
    
    IEnumerator Spawn()
    {
        
        while (enemyCount < maxEnemyAmount)
>>>>>>> Stashed changes
        {
            Vector3 randomPos;
            Quaternion randomRotLeft = enemy.transform.rotation;
            Quaternion randomRotRight = enemy.transform.rotation;
<<<<<<< Updated upstream
            Vector3 vector3Rot = randomRotLeft.eulerAngles;
            GameObject enemyInstance;
            if (spawnOnLeftSide)
            {
                randomPos = new Vector3(transform.position.x - 10.6f, transform.position.y, transform.position.z);
                enemyInstance = Instantiate(enemy, randomPos, randomRotLeft);

=======

            if (_spawnOnLeftSide)
            {
                randomPos = new Vector3(transform.position.x - 10.6f, transform.position.y - 3f, transform.position.z);
                Instantiate(enemy, randomPos, randomRotLeft);
>>>>>>> Stashed changes
            }
            else if (!_spawnOnLeftSide)
            {
                randomPos = new Vector3(transform.position.x + 10.6f, transform.position.y, transform.position.z);
<<<<<<< Updated upstream
                enemyInstance = Instantiate(enemy, randomPos, randomRotRight);
            }
            spawnOnLeftSide = !spawnOnLeftSide;
            yield return new WaitUntil(() => Vector3.Distance(transform.position, enemyInstance.transform.position) > distance);
            //yield return new WaitForSeconds(spawnTime);
=======
                Instantiate(enemy, randomPos, randomRotRight);
            }
            if (enemyController != null && playerTransform != null)
            {
                enemyController.SetPlayerTransform(playerTransform);
            }
            enemyCount++;
            _spawnOnLeftSide = !_spawnOnLeftSide;
            yield return new WaitForSeconds(spawnTime);
>>>>>>> Stashed changes
        }
    }
    
}
