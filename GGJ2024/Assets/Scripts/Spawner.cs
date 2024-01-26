using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
   
    public float spawnTime = 10.5f;
    public float distance = 5f;
    public int maxEnemyAmount;
    public List<GameObject> enemeylist = new List<GameObject>(); // need to change to private

    private bool _spawnOnLeftSide = true;
   

    void Update()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (enemeylist.Count < maxEnemyAmount)
        {
            Vector3 randomPos;
            
            Quaternion randomRotLeft = enemy.transform.rotation;
            Quaternion randomRotRight = enemy.transform.rotation;
            Vector3 vector3Rot = randomRotLeft.eulerAngles;
            GameObject enemyInstance;

            if (_spawnOnLeftSide)
            {
                randomPos = new Vector3(transform.position.x - 10.6f, transform.position.y - 3f, transform.position.z);
                enemyInstance = Instantiate(enemy, randomPos, randomRotLeft);
                enemeylist.Add(enemyInstance);

            }
            else
            {
                randomPos = new Vector3(transform.position.x + 10.6f, transform.position.y, transform.position.z);
                enemyInstance = Instantiate(enemy, randomPos, randomRotRight);
                enemeylist.Add(enemyInstance);
            }
            _spawnOnLeftSide = !_spawnOnLeftSide;
            yield return new WaitUntil(() => Vector3.Distance(transform.position, enemyInstance.transform.position) > distance);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
// notes - change the speed rate of spawns, and create a range where they can spawn on the Y so they don't all come from the same spot
