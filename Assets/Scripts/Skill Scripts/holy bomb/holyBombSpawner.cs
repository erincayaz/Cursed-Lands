using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holyBombSpawner : MonoBehaviour
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
        if(Time.time - spawnTime >= holyBomb.cooldown)
        {
            spawnBomb();
        }
    }

    void spawnBomb()
    {
        Vector3 targetPos = new Vector3(
            Camera.main.transform.position.x + Random.Range(-screenX, screenX),
            Camera.main.transform.position.y + Random.Range(-screenY, screenY),
            0f);

        print("1 " + Camera.main.transform.position.y);
        print("2 " + screenY * 5 / 3);
        
        float spawnY = Camera.main.transform.position.y + (screenY * 5/3);

        print("3 " + spawnY);
        float spawnX = targetPos.x + (spawnY - targetPos.y) / y_xRatio;

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

        if (bombList.Count > 1)
        {
            GameObject toSpawn = bombList[bombList.Count - 1];
            toSpawn.GetComponent<holyBomb>().unpool(spawnPos, targetPos);
            bombList.Remove(toSpawn);
        }
        else
        {
            GameObject toSpawn = Instantiate(holyBombObj, spawnPos, Quaternion.identity);
            toSpawn.GetComponent<holyBomb>().targetPos = targetPos;
        }
        spawnTime = Time.time;
    }
}
