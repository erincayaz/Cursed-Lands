using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawner : MonoBehaviour
{
    [SerializeField] AnimationCurve spawnControl;
    [SerializeField] List<MonsterSpecificSpawner> enemyScripts;
    [SerializeField] List<GameObject> enemyPrefab;
    [SerializeField] List<int> waves;

    int i;
    int specialEventCounter = 20;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        StartCoroutine("SpawnEnemies");
    }

    IEnumerator SpawnEnemies()
    {
        while(Time.time < 30 * 60)
        {
            float h = Input.GetAxisRaw("Horizontal"), v = Input.GetAxisRaw("Vertical");

            if (Time.time > waves[i])
            {
                i++;
            }

            SpawnFunctionCalls(h, v);

            yield return new WaitForSeconds(spawnControl.Evaluate(Time.time / 1800f));
        }

        yield return null;
    }

    private void SpawnFunctionCalls(float h, float v)
    {
        float spawnRandomNumber = Random.Range(1, 2 + (int)(Time.time / 180));
        StartCoroutine(enemyScripts[i].SpawnMultipleEnemyAtOnePoint(h, v, spawnRandomNumber));

        float randomNumber = Random.Range(0f, 100f);
        if (randomNumber < (Time.time / 36f))
        {
            int c = Random.Range(8, 12 + (int)(Time.time / 100f));
            StartCoroutine(enemyScripts[i].SpawnMultipleEnemyAtOnePoint(h, v, c));
        }

        if ((int)Time.time >= specialEventCounter)
        {
            StartCoroutine(enemyScripts[i].specialEventSpawner(5, 3));
            specialEventCounter += 60;
        }
    }
}
