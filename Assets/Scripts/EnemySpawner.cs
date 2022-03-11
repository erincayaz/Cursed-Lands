using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float[] waveTimes;

    public static int i;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (i < waveTimes.Length)
        {
            if (Time.time > waveTimes[i])
            {
                EventManager.BrodcastOnSpawn(i);
                i++;
            }
        }
    }

    public static Vector3 generateRandomPoint(Vector3 cameraPos)
    {
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Camera.main.aspect;

        List<Vector3> options = new List<Vector3>();

        float randX1 = Random.Range(cameraPos.x - 25f, cameraPos.x - 1f);
        float randX2 = Random.Range(cameraPos.x + width + 1f, cameraPos.x + width + 25f);
        float randX = Random.Range(0, 2) == 0 ? randX1 : randX2;

        float randY1 = Random.Range(cameraPos.y - 15f, cameraPos.y - 1f);
        float randY2 = Random.Range(cameraPos.y + height + 1f, cameraPos.y + height + 15f);
        float randY = Random.Range(0, 2) == 0 ? randY1 : randY2;
        options.Add(new Vector3(randX, randY, 0));

        float randy = Random.Range(-cameraPos.y, cameraPos.y);
        options.Add(new Vector3(randX, randy, 0));

        float randx = Random.Range(-cameraPos.x, cameraPos.x);
        options.Add(new Vector3(randx, randY, 0));

        int randOption = Random.Range(0, 3);

        return options[randOption];
    }
}
