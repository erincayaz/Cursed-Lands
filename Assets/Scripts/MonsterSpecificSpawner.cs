using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpecificSpawner : MonoBehaviour
{
    List<GameObject> enemies = new List<GameObject>();
    [SerializeField] List<int> countForWaves = new List<int>();
    [SerializeField] GameObject spawnedObject;

    private void OnEnable()
    {
        EventManager.OnSpawn += CoroutineStarter;
    }

    private void OnDisable()
    {
        EventManager.OnSpawn -= CoroutineStarter;
    }

    void CoroutineStarter(int i)
    {
        StartCoroutine(EnemySpawnSubscriber(i));
    }

    IEnumerator EnemySpawnSubscriber(int i)
    {
        int count = countForWaves[i];

        int j = 0;
        while(j < count)
        {
            SpawnEnemy(0, 0);

            j++;
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    public void SpawnEnemy(float h, float v)
    {
        GameObject temp = availableEnemyExist();
        if (temp != null)
        {
            Vector3 randomPoint = EnemySpawner.generateRandomPoint(h, v, Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0)));

            temp.SetActive(true);
            temp.GetComponent<enemyMovement>().ded = false;
            temp.transform.position = randomPoint;
        }
        else
        {
            Vector3 randomPoint = EnemySpawner.generateRandomPoint(h, v, Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0)));

            GameObject gameObject = Instantiate(spawnedObject, randomPoint, Quaternion.identity);
            enemies.Add(gameObject);
        }
    }

    GameObject availableEnemyExist()
    {
        if (enemies.Count > 0)
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy.transform.position.x == 500f && enemy.transform.position.y == 500f)
                {
                    return enemy;
                }
            }
        }

        return null;
    }
}
