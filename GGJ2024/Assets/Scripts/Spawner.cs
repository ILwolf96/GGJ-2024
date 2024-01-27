using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    EnemyController enemyController;
    [SerializeField] PlayerController _player;
    [SerializeField] LaughMeter _laughMeter;

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
                enemyController = Instantiate(enemy, randomPos, randomRotLeft).GetComponent<EnemyController>();

            }
            else if (!_spawnOnLeftSide)
            {
                randomPos = new Vector3(transform.position.x + 10.6f, transform.position.y, transform.position.z);

                enemyController = Instantiate(enemy, randomPos, randomRotRight).GetComponent<EnemyController>();
            }
            if (enemyController != null && _player != null && _laughMeter != null)
            {
                enemyController.SetPlayer(_player);
                enemyController.SetLaughMeter(_laughMeter);
            }
            enemyCount++;
            _spawnOnLeftSide = !_spawnOnLeftSide;
            yield return new WaitForSeconds(spawnTime);

        }
    }
}
// notes - change the speed rate of spawns, and create a range where they can spawn on the Y so they don't all come from the same spot