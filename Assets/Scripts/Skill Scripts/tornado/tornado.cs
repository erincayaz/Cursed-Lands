using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tornado : MonoBehaviour
{
    public skillScriptableObject tornadoScr;
    public LayerMask enemyMask;
    float pullForce;
    float spawnTime;
    bool pooled;

    public AnimationCurve forceCurve;

    private void Start()
    {
        spawnTime = Time.time;
    }

    private void Update()
    {
        pullForce = forceCurve.Evaluate(Time.time - spawnTime);

        if(Time.time - spawnTime < tornadoScr.duration && !pooled)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, tornadoScr.radius, Vector2.zero, Mathf.Infinity, enemyMask);

            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    pullEnemy(hit.transform.gameObject);
                }
            }
        }
        else
        {
            pool();
        }
    }

    void pool()
    {
        if(!pooled)
        {
            pooled = true;
            transform.position = new Vector2(998f, 998f);
            FindObjectOfType<tornadoSpawner>().tornadoPool.Add(gameObject);
        }
    }

    public void unpool(Vector2 pos)
    {
        pooled = false;
        transform.position = pos;
        spawnTime = Time.time;
    }

    void pullEnemy(GameObject enemy)
    {
        float damage = (tornadoScr.damage / tornadoScr.duration) * Time.deltaTime;
        Vector2 dir = (transform.position - enemy.transform.position).normalized;

        enemy.GetComponent<enemyMovement>().enemyHit(damage, 0f);

        enemy.GetComponent<Rigidbody2D>().AddForce(dir * pullForce);
    }
}
