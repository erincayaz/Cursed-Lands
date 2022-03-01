using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosiveSummon : MonoBehaviour
{
    public skillScriptableObject explodingSummon;
    public Transform player;
    public LayerMask enemyMask;
    Rigidbody2D rb;
    GameObject targetEnemy;
    float spawnTime = 0;
    bool exploded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        locateEnemy();
        //StartCoroutine(explode());
    }

    void Update()
    {
        runAndExplode();
    }
    void locateEnemy()
    {
        RaycastHit2D[] enemies = Physics2D.CircleCastAll(transform.position, 15f, Vector2.zero, Mathf.Infinity, enemyMask);
        if(enemies.Length > 0)
        {
            //////////////////////////////////////////////////////////////////
            // Find the closest enemy
            targetEnemy = enemies[0].transform.gameObject;
            float minDistance = Vector3.Distance(transform.position, targetEnemy.transform.position);
            foreach (RaycastHit2D enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    targetEnemy = enemy.transform.gameObject;
                    minDistance = distance;
                }
            }
            ///////////////////////////////////////////////////////////////////
        }
    }

    void runAndExplode()
    {
        if(targetEnemy != null && !exploded)
        {
            if (Vector2.Distance(targetEnemy.transform.position, transform.position) >= .5f && Time.time - spawnTime < explodingSummon.duration)
            {
                Vector2 dir = targetEnemy.transform.position - transform.position;
                dir = dir.normalized;
                rb.velocity = dir * explodingSummon.speed;
            }
            else
            {
                exploded = true;
                rb.velocity = Vector2.zero;
                Invoke("explode", .5f);
            }
        }
    }
    void explode()
    {
        //summon damages enemies here
        RaycastHit2D[] enemies = Physics2D.CircleCastAll(transform.position, explodingSummon.radius, Vector2.zero, Mathf.Infinity, enemyMask);
        //damage enemy here
        foreach (var enemy in enemies)
        {
            enemy.transform.GetComponent<enemyMovement>().enemyHit(explodingSummon.damage, 0f);
        }
        // damaging enemies end here

        pool();
    }

    public void pool()
    {
        transform.position = new Vector2(995f, 995f);
        rb.velocity = Vector2.zero;
        targetEnemy = null;
        FindObjectOfType<explosiveSummonSpawner>().summonList.Add(gameObject);
    }
    public void unpool()
    {
        exploded = false;
        float randX = Random.Range(.1f, .5f); float randY = Random.Range(.1f, .5f);
        transform.position = new Vector3(randX + player.transform.position.x, randY + player.transform.position.y, 0);
        locateEnemy();
        spawnTime = Time.time;
    }
}
