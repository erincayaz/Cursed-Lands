using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] skillScriptableObject shurikenStats;
    [SerializeField] float rotationSpeed;

    public Transform player;
    Vector3 dir;
    Rigidbody2D rb;

    float startMagnitude;
    bool minusStart;
    bool magnitudeCur;

    public void Throw()
    {
        rb = GetComponent<Rigidbody2D>();

        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(15f, 10f), 0f, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemy"));
        if (hits.Length != 0)
        {
            int RandomNumber = Random.Range(0, hits.Length - 1);
            dir = (hits[RandomNumber].transform.position - transform.position).normalized;
            rb.velocity = dir * 10f;
        }
        else
        {
            dir = Vector3.left;
            rb.velocity = dir * 10f;
        }

        startMagnitude = rb.velocity.magnitude;
        minusStart = startMagnitude < 0f ? true : false;
        magnitudeCur = minusStart;
    }

    private void Update()
    {
        if(!magnitudeCur)
        {
            float velX = rb.velocity.x > 0f ? rb.velocity.x - (3f * Time.deltaTime * Mathf.Abs(dir.x)) : rb.velocity.x + (3f * Time.deltaTime * Mathf.Abs(dir.x));
            float velY = rb.velocity.y > 0f ? rb.velocity.y - (3f * Time.deltaTime * Mathf.Abs(dir.y)) : rb.velocity.y + (3f * Time.deltaTime * Mathf.Abs(dir.y));

            rb.velocity = new Vector3(velX, velY);

            magnitudeCur = rb.velocity.magnitude < 8f ? true : false;
        }
        else
        {
            dir = player.transform.position - transform.position;
            dir = dir.normalized;

            if (rb.velocity.magnitude > 8f)
                rb.AddForce(-rb.velocity.normalized * (8f));
            else
                rb.AddForce(dir * 8f);
        }

        transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<enemyMovement>().enemyHit(shurikenStats.damage, 2f);
        }
    }
}
