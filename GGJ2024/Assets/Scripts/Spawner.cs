using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] EnemyController enemyController;
    [SerializeField] Transform playerTransform;
   
    public float spawnTime = 10.5f;
    public static int maxEnemyAmount = 10;
    public static int enemyCount = 0;
    private bool _spawnOnLeftSide = true;

    void Start()
    {
        StartCoroutine(Spawn());
    }
    
    IEnumerator Spawn()
    {
        while (enemyCount < maxEnemyAmount)
        {
            Vector3 randomPos;
            Quaternion randomRotLeft = enemy.transform.rotation;
            Quaternion randomRotRight = enemy.transform.rotation;

            if (_spawnOnLeftSide)
            {
                randomPos = new Vector3(transform.position.x - 10.6f, transform.position.y - 3f, transform.position.z);
                Instantiate(enemy, randomPos, randomRotLeft);

            }
            else if (!_spawnOnLeftSide)
            {
                randomPos = new Vector3(transform.position.x + 10.6f, transform.position.y, transform.position.z);

                Instantiate(enemy, randomPos, randomRotRight);
            }
            if (enemyController != null && playerTransform != null)
            {
                enemyController.SetPlayerTransform(playerTransform);
            }
            enemyCount++;
            _spawnOnLeftSide = !_spawnOnLeftSide;
            yield return new WaitForSeconds(spawnTime);

        }
    }
}
