using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tornadoSpawner : MonoBehaviour
{
    public skillScriptableObject tornado;
    public GameObject tornadoObj;
    float lastSpawn;
    GameObject toSpawn;

    public List<GameObject> tornadoPool = new List<GameObject>();
    private void Start()
    {
        lastSpawn = 0;
    }

    private void Update()
    {
        spawnTornado();
    }

    void spawnTornado()
    {
        if (Time.time - lastSpawn >= tornado.cooldown)
        {
            for(int i = 0; i < tornado.amount; i++)
            {
                float randX = Random.Range(-5f, 5f);
                float randY = Random.Range(-4f, 4f);

                Vector2 spawnPos = new Vector2(randX, randY);
                if(tornadoPool.Count > 0)
                {
                    toSpawn = tornadoPool[tornadoPool.Count - 1];
                    tornadoPool.Remove(toSpawn);
                    toSpawn.GetComponent<tornado>().unpool(spawnPos);
                }
                else
                {
                    Instantiate(tornadoObj, spawnPos, Quaternion.identity);
                }
            }
            lastSpawn = Time.time;
        }
    }
}
