using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holyWaterSpawner : MonoBehaviour
{
    public skillScriptableObject holyBomb;

    [SerializeField] GameObject holyBombObj;
    [SerializeField] float angle;
    float y_xRatio;
    float screenX, screenY;
    float spawnTime;

    public List<GameObject> bombList = new List<GameObject>();
    private void Start()
    {
        y_xRatio = Mathf.Atan(angle);
        spawnTime = Time.time;

        screenX = 7;
        screenY = 3.5f;
    }

    private void Update()
    {
        if(Time.time - spawnTime >= holyBomb.cooldown && holyBomb.amount != 0)
        {
            for(int i = 0; i < holyBomb.amount; i++)
            {
                spawnBomb();
            }
        }
    }

    void spawnBomb()
    {
        Vector3 targetPos = new Vector3(
            Camera.main.transform.position.x + Random.Range(-screenX, screenX),
            Camera.main.transform.position.y + Random.Range(-screenY, screenY),
            0f);
        
        float spawnY = Camera.main.transform.position.y + (screenY * 5/3);

        float spawnX = targetPos.x + (spawnY - targetPos.y) / y_xRatio;

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

        if (bombList.Count > 1)
        {
            GameObject toSpawn = bombList[bombList.Count - 1];
            toSpawn.GetComponent<holyWater>().unpool(spawnPos, targetPos);
            bombList.Remove(toSpawn);
        }
        else
        {
            GameObject toSpawn = Instantiate(holyBombObj, spawnPos, Quaternion.identity);
            toSpawn.GetComponent<holyWater>().targetPos = targetPos;
        }
        spawnTime = Time.time;
    }
}
