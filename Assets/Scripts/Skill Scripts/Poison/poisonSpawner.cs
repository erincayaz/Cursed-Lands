using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonSpawner : MonoBehaviour
{
    public skillScriptableObject poison;
    [SerializeField] GameObject poisonObject;

    [SerializeField] float spawnTime;
    float lastSpawn;

    void Start()
    {
        lastSpawn = 0;
    }
    void Update()
    {
        spawnPoison();
    }

    void spawnPoison()
    {
        if(Time.time - lastSpawn >= spawnTime && poison.amount != 0)
        {
            Instantiate(poisonObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            lastSpawn = Time.time;
        }
    }
}
