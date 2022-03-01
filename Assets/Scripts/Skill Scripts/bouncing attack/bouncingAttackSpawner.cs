using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncingAttackSpawner : MonoBehaviour
{
    public skillScriptableObject bouncing;
    public GameObject bouncingAttack;
    float lastSpawn;
    GameObject toSpawn;

    public List<GameObject> bouncingPool = new List<GameObject>();

    private void Start()
    {
        lastSpawn = 0;
    }

    private void Update()
    {
        spawnBouncing();
    }

    void spawnBouncing()
    {
        if(Time.time - lastSpawn >= bouncing.cooldown)
        {
            for(int i = 0; i < bouncing.amount; i++)
            {
                if (bouncingPool.Count > 0)
                {
                    toSpawn = bouncingPool[bouncingPool.Count - 1];
                    toSpawn.GetComponent<bouncingAttack>().unpool(transform.position);
                    bouncingPool.Remove(toSpawn);
                }

                else
                {
                    Instantiate(bouncingAttack, transform.position, Quaternion.identity);
                }
            }
            lastSpawn = Time.time;
        }
    }
}
