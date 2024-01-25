using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float spawnTime = 1.5f;
    [SerializeField] float distance = 5f;
    private bool spawnOnLeftSide = true;

    void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while (true)
        {
            Vector3 randomPos;
            
            Quaternion randomRotLeft = enemy.transform.rotation;
            Quaternion randomRotRight = enemy.transform.rotation;
            Vector3 vector3Rot = randomRotLeft.eulerAngles;
            GameObject enemyInstance;
            if (spawnOnLeftSide)
            {
                randomPos = new Vector3(transform.position.x - 10.6f, transform.position.y, transform.position.z);
                enemyInstance = Instantiate(enemy, randomPos, randomRotLeft);

            }
            else
            {
                randomPos = new Vector3(transform.position.x + 10.6f, transform.position.y, transform.position.z);
                enemyInstance = Instantiate(enemy, randomPos, randomRotRight);
            }
            spawnOnLeftSide = !spawnOnLeftSide;
            yield return new WaitUntil(() => Vector3.Distance(transform.position, enemyInstance.transform.position) > distance);
            //yield return new WaitForSeconds(spawnTime);
        }
    }
}
