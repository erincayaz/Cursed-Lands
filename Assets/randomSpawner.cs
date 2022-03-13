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

            enemyScripts[i].SpawnEnemy(h, v);

            if(Time.time > waves[i])
            {
                i++;
            }

            yield return new WaitForSeconds(spawnControl.Evaluate(Time.time / 1800));
        }

        yield return null;
    }
}
