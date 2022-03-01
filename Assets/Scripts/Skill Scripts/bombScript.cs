using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : MonoBehaviour
{
    [SerializeField] skillScriptableObject bombStats;

    Transform playerPos;
    Transform enemyPos;
    Vector3 point3;

    bool willAttack;
    float count = 0f;

    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {
        willAttack = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (count < 1.0f && willAttack)
        {
            count += 1.0f * Time.deltaTime / 1.25f;

            Vector3 m1 = Vector3.Lerp(playerPos.position, point3, count);
            Vector3 m2 = Vector3.Lerp(point3, enemyPos.position, count);
            transform.position = Vector3.Lerp(m1, m2, count);

            GetComponent<CircleCollider2D>().enabled = false;
        }
        else if(count >= 1.0f && willAttack)
        {
            GetComponent<CircleCollider2D>().enabled = true;

            anim.SetTrigger("explode");
            willAttack = false;

            Invoke("pool", 0.5f);
        }

    }

    void pool()
    {
        transform.position = new Vector3(1010f, 1010f, 0f);
        rb.velocity = Vector2.zero;
    }

    public void Throw(Transform player, Transform enemy)
    {
        playerPos = player; enemyPos = enemy;

        point3 = playerPos.position + (enemyPos.position - playerPos.position) / 2 + Vector3.up * 5.0f;
        count = 0f;
        transform.position = playerPos.position;
        willAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<enemyMovement>().enemyHit(bombStats.damage, 4f);
        }
    }
}
