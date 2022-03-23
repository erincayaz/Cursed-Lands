using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpecificSpawner : MonoBehaviour
{
    List<GameObject> enemies = new List<GameObject>();
    //[SerializeField] List<int> countForWaves = new List<int>();
    [SerializeField] GameObject spawnedObject;
    public List<SpawnerEvent> spawner = new List<SpawnerEvent>();

    public enum SpawnTypes
    {
        SpawnRandomly,
        SpawnAsLine,
        SpawnAtOnePoint,
        SpawnAsCircle
    }

    [System.Serializable]
    public struct SpawnerEvent
    {
        public float time;
        public SpawnTypes spawn;

        public int count;
        public float cooldownBetweenSpawns;
    }

    private void OnEnable()
    {
        EventManager.OnSpawn += CoroutineStarter;
    }

    private void OnDisable()
    {
        EventManager.OnSpawn -= CoroutineStarter;
    }

    void CoroutineStarter(int time)
    {
        for(int i = 0; i < spawner.Count; i++)
        {
            if(time == spawner[i].time)
            {
                if (spawner[i].spawn == SpawnTypes.SpawnRandomly)
                    StartCoroutine(EnemySpawnSubscriber(spawner[i].count, spawner[i].cooldownBetweenSpawns));
                else if (spawner[i].spawn == SpawnTypes.SpawnAsLine)
                    StartCoroutine(specialEventSpawner(spawner[i].count, spawner[i].cooldownBetweenSpawns));
                else if (spawner[i].spawn == SpawnTypes.SpawnAtOnePoint)
                    StartCoroutine(SpawnMultipleEnemyAtOnePoint(-1, -1, spawner[i].count));
                else if (spawner[i].spawn == SpawnTypes.SpawnAsCircle)
                    StartCoroutine(SpawnEnemiesAsCircle(spawner[i].count, spawner[i].cooldownBetweenSpawns));
            }

        }
    }

    IEnumerator EnemySpawnSubscriber(int i, float c)
    {
        int count = i;

        int j = 0;
        while(j < count)
        {
            SpawnEnemy(0, 0);

            j++;
            yield return new WaitForSeconds(c);
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

    public IEnumerator SpawnMultipleEnemyAtOnePoint(float h, float v, float c)
    {
        Vector3 randomPoint = EnemySpawner.generateRandomPoint(h, v, Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0)));
        for (int i = 0; i < c; i++)
        {
            GameObject temp = availableEnemyExist();
            if (temp != null)
            {
                temp.SetActive(true);
                temp.GetComponent<enemyMovement>().ded = false;
                temp.transform.position = randomPoint;
            }
            else
            {
                GameObject gameObject = Instantiate(spawnedObject, randomPoint, Quaternion.identity);
                enemies.Add(gameObject);
            }

            yield return new WaitForSeconds(0.25f);
        }
        yield return null;
    }

    public IEnumerator specialEventSpawner(int countForWaves, float cooldown)
    {
        int randomNumber = Random.Range(0, 1);
        if(randomNumber == 0)
        {
            for(int i = 0; i < countForWaves; i++)
            {
                float h = Input.GetAxisRaw("Horizontal"), v = Input.GetAxisRaw("Vertical");

                SpawnEnemiesSpecialEvent(h, v);

                yield return new WaitForSeconds(cooldown);
            }
        }

        yield return null;
    }

    public IEnumerator SpawnEnemiesAsCircle(int countForWaves, float cooldown)
    {
        Vector3 cameraPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0));
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Camera.main.aspect;

        for (int i = 0; i < countForWaves; i++)
        {
            for (float t = 0f; t < 1f; t += 0.01f)
            {
                var center = new Vector2(cameraPos.x + width / 2f, cameraPos.y + height / 2f);
                var radius = width / 2f + 3f; // radius of the circle
                var angle = t * 2f * Mathf.PI; // in radians
                var pointOnCircle = center + radius * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                GameObject temp = availableEnemyExist();
                if (temp != null)
                {
                    temp.SetActive(true);
                    temp.GetComponent<enemyMovement>().ded = false;
                    temp.transform.position = pointOnCircle;
                }
                else
                {
                    GameObject gameObject = Instantiate(spawnedObject, pointOnCircle, Quaternion.identity);
                    enemies.Add(gameObject);
                }
            }

            yield return new WaitForSeconds(cooldown);
        }

        yield return null;
    }

    public void SpawnEnemiesSpecialEvent(float h, float v)
    {
        Vector3 cameraPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0));
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Camera.main.aspect;

        if (v == 0)
        {
            Vector3 spawnLocation = h == 1 ? new Vector3(cameraPos.x + width + 3f, cameraPos.y - 2f) : new Vector3(cameraPos.x - 3f, cameraPos.y - 2f);

            for(float i = cameraPos.y; i < cameraPos.y + height + 4f; i++)
            {
                GameObject temp = availableEnemyExist();
                if (temp != null)
                {
                    temp.SetActive(true);
                    temp.GetComponent<enemyMovement>().ded = false;
                    temp.transform.position = spawnLocation;
                }
                else
                {
                    GameObject gameObject = Instantiate(spawnedObject, spawnLocation, Quaternion.identity);
                    enemies.Add(gameObject);
                }

                spawnLocation = Utils.ChangeY(spawnLocation, spawnLocation.y + 1f);
            }
        }
        else
        {
            Vector3 spawnLocation = v == 1 ? new Vector3(cameraPos.x - 2f, cameraPos.y + 3f + height) : new Vector3(cameraPos.x - 2f, cameraPos.y - 3f);

            for (float i = cameraPos.x; i < cameraPos.x + width + 4f; i++)
            {
                GameObject temp = availableEnemyExist();
                if (temp != null)
                {
                    temp.SetActive(true);
                    temp.GetComponent<enemyMovement>().ded = false;
                    temp.transform.position = spawnLocation;
                }
                else
                {
                    GameObject gameObject = Instantiate(spawnedObject, spawnLocation, Quaternion.identity);
                    enemies.Add(gameObject);
                }

                spawnLocation = Utils.ChangeX(spawnLocation, spawnLocation.x + 1f);
            }
        }
        /*
        else
        {
            Vector3 spawnLocationX = h == 1 ? new Vector3(cameraPos.x + width + 3f, cameraPos.y + (height / 2f)) : new Vector3(cameraPos.x - 3f, cameraPos.y + (height / 2f));
            Vector3 spawnLocationY = v == 1 ? new Vector3(cameraPos.x + (width / 2f), cameraPos.y + height + 3f) : new Vector3(cameraPos.x + (width / 2f), cameraPos.y - 3f);

            for (float i = spawnLocationY.x;;)
            {
                GameObject temp = availableEnemyExist();
                if (temp != null)
                {
                    temp.SetActive(true);
                    temp.GetComponent<enemyMovement>().ded = false;
                    temp.transform.position = spawnLocationY;
                }
                else
                {
                    GameObject gameObject = Instantiate(spawnedObject, spawnLocationY, Quaternion.identity);
                    enemies.Add(gameObject);
                }

                spawnLocationY = Utils.ChangeX(spawnLocationY, spawnLocationY.x + 1f);

                i = h == 1 ? i + 1 : i - 1;
                print(h);

                if (i > cameraPos.x + width + 4f || i < cameraPos.x - 4f)
                    break;
            }

            for (float i = spawnLocationX.y;;)
            {
                GameObject temp = availableEnemyExist();
                if (temp != null)
                {
                    temp.SetActive(true);
                    temp.GetComponent<enemyMovement>().ded = false;
                    temp.transform.position = spawnLocationX;
                }
                else
                {
                    GameObject gameObject = Instantiate(spawnedObject, spawnLocationX, Quaternion.identity);
                    enemies.Add(gameObject);
                }

                spawnLocationX = Utils.ChangeY(spawnLocationX, spawnLocationX.y + 1f);

                i = v == 1 ? i + 1 : i - 1;
                if (i > cameraPos.y + height + 4f || i < cameraPos.y - 4f)
                    break;
            }
        }
        */
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
