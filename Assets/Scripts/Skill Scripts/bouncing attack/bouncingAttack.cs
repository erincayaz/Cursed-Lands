using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncingAttack : MonoBehaviour
{
    public skillScriptableObject bouncing;
    float spawnTime;
    Rigidbody2D rb;
    bool pooled;
    void Start()
    {
        spawnTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        setSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spawnTime >= bouncing.duration)
            pool();
    }

    void setSpeed()
    {
        int angle = Random.Range(0, 360);

        rb.velocity = new Vector2(bouncing.speed * Mathf.Cos(angle * Mathf.Deg2Rad), bouncing.speed * Mathf.Sin(angle * Mathf.Deg2Rad));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<enemyMovement>().enemyHit(bouncing.damage, 2f);
        }
    }

    void pool()
    {
        if(!pooled)
        {
            pooled = true;
            transform.position = new Vector2(1001f, 1001f);
            FindObjectOfType<bouncingAttackSpawner>().bouncingPool.Add(gameObject);
        }
    }

    public void unpool(Vector2 pos)
    {
        transform.position = pos;
        pooled = false;
        spawnTime = Time.time;

        setSpeed();
    }
}
