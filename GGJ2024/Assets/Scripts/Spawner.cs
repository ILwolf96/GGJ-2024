using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float spawnTime = 10.5f;
    [SerializeField] float distance = 5f;

    public int MaxEnemyAmount;
    private bool spawnOnLeftSide = true;
    List<GameObject> enemeylist = new List<GameObject>();

    void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while (enemeylist.Count < MaxEnemyAmount)
        {
            Vector3 randomPos;
            
            Quaternion randomRotLeft = enemy.transform.rotation;
            Quaternion randomRotRight = enemy.transform.rotation;
            Vector3 vector3Rot = randomRotLeft.eulerAngles;
            GameObject enemyInstance;

            if (spawnOnLeftSide)
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
            spawnOnLeftSide = !spawnOnLeftSide;
            yield return new WaitUntil(() => Vector3.Distance(transform.position, enemyInstance.transform.position) > distance);
            //yield return new WaitForSeconds(spawnTime);
        }
    }
}
