using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallSpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject wall;
    [SerializeField] float cooldown;
    [SerializeField] List<Transform> positions = new List<Transform>(8);
    Vector3 currentPos;
    float angle;
    float lastSpawn;

    void Start()
    {
        lastSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        adjustScale();
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        checkPosPlace(dir);
        spawnWall();
    }

    void adjustScale()
    {
        if((player.localScale.x < 0 && transform.localScale.x > 0) || (player.localScale.x > 0 && transform.localScale.x < 0))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    void spawnWall()
    {
        if(Time.time - lastSpawn >= cooldown)
        {
            GameObject newWall = Instantiate(wall, currentPos, Quaternion.Euler(new Vector3(0, 0, angle)));
            lastSpawn = Time.time;
            Destroy(newWall, 3);
        }
    }
    void checkPosPlace(Vector2 dir)
    {
        if(dir.x < -.25 && dir.y == 0)
        {
            currentPos = positions[0].position;
            angle = 0;
        }
        else if (dir.x == 0 && dir.y < -.25)
        {
            currentPos = positions[1].position;
            angle = 90;
        }
        else if(dir.x > 0.25 && dir.y == 0)
        {
            currentPos = positions[2].position;
            angle = 180;
        }
        else if(dir.x == 0 && dir.y > .25)
        {
            currentPos = positions[3].position;
            angle = 270;
        }
        else if (dir.x < -.25 && dir.y < -.25)
        {
            currentPos = positions[4].position;
            angle = 45;
        }
        else if (dir.x > .25 && dir.y < -.25)
        {
            currentPos = positions[5].position;
            angle = 135;
        }
        else if (dir.x > .25 && dir.y > .25)
        {
            currentPos = positions[6].position;
            angle = 225;
        }
        else if (dir.x < -.25 && dir.y > .25)
        {
            currentPos = positions[7].position;
            angle = 315;
        }
    }
}
