using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosingSummon : MonoBehaviour
{
    public skillScriptableObject explodingSummon;
    public Transform player;
    public LayerMask enemyMask;
    Rigidbody2D rb;
    GameObject targetEnemy;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetEnemy = locateEnemy();
        StartCoroutine(explode());
    }

    void Update()
    {
        runAndExplode();
    }
    GameObject locateEnemy()
    {
        GameObject nearestEnemy = new GameObject();
        RaycastHit2D[] enemies = Physics2D.CircleCastAll(transform.position, 5f, Vector2.zero, Mathf.Infinity, enemyMask);
        if(enemies.Length > 0)
        {
            //////////////////////////////////////////////////////////////////
            // Find the closest enemy
            nearestEnemy = enemies[0].transform.gameObject;
            float minDistance = Vector3.Distance(transform.position, nearestEnemy.transform.position);
            foreach (RaycastHit2D enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    nearestEnemy = enemy.transform.gameObject;
                    minDistance = distance;
                }
            }
            ///////////////////////////////////////////////////////////////////
        }
        return nearestEnemy;
    }

    void runAndExplode()
    {
        if(targetEnemy != null)
        {
            if (Vector2.Distance(targetEnemy.transform.position, transform.position) >= .3f)
            {
                Vector2 dir = targetEnemy.transform.position - transform.position;
                rb.velocity = dir;
            }
        } 
    }

    IEnumerator explode()
    {
        yield return new WaitForSeconds(explodingSummon.duration);

        //summon damages enemies here
        Debug.Log("damaging enemy");
        // damaging enemies end here

        pool();
    }

    public void pool()
    {
        transform.position = new Vector2(995f, 995f);
        rb.velocity = Vector2.zero;
        targetEnemy = null;
    }
    public void unpool()
    {
        transform.position = player.transform.position + new Vector3(player.localScale.x * .2f, 0, 0);
        targetEnemy = locateEnemy();
        StartCoroutine(explode());
    }
}
