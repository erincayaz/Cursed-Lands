using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosiveSummonSpawner : MonoBehaviour
{
    [SerializeField] GameObject summon;
    [SerializeField] Transform player;
    public skillScriptableObject explosiveSummonStat;
    float lastSummonTime = 0;

    public List<GameObject> summonList = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        spawnSummon();
    }

    void spawnSummon()
    {
        if (Time.time - lastSummonTime >= explosiveSummonStat.cooldown)
        {
            if (summonList.Count > 0)
            {
                GameObject toSpawn = summonList[summonList.Count - 1];
                toSpawn.GetComponent<explosiveSummon>().unpool();
                summonList.Remove(toSpawn);
            }

            else
            {
                float randX = Random.Range(.5f, 1.5f); float randY = Random.Range(.5f, 1.5f);
                Instantiate(summon, new Vector3(Camera.main.transform.position.x + randX, Camera.main.transform.position.y + randY, 0f), Quaternion.identity);
            }

            lastSummonTime = Time.time;
        }
    }
}
