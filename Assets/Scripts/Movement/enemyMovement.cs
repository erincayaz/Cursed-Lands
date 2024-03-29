using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    //public delegate void PlayerHitAction(float damage);
    //public static event PlayerHitAction OnHit;

    public enemyScriptableObject scriptableObject;

    // object variables
    public float health;
    [SerializeField] float hitCooldown;

    GameObject player;
    float speed, attack;
    Rigidbody2D rb;
    Vector3 dir;

    // control variables
    bool hit;
    public bool ded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        speed = scriptableObject.speed;
        attack = scriptableObject.attack;
        health = scriptableObject.health;
        
        hit = false;
        ded = false;

        GetComponent<SpriteRenderer>().sprite = scriptableObject.sprite;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!ded)
        {
            dir = player.transform.position - transform.position;
            dir = dir.normalized;
            if (!hit)
            {
                rb.velocity = (dir * speed);
            }
            else
            {
                rb.AddForce(dir * Time.deltaTime * 75f);
            }

            if (Vector2.Distance(transform.position, player.transform.position) > 40f)
            {
                death();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            EventManager.BrodcastOnHit(attack);
        }
    }

    public void enemyHit(float damage, float knock)
    {
        health -= damage;
        rb.AddForce(-dir * knock, ForceMode2D.Impulse);

        if(health <= 0f)
        {
            death();
        }

        hit = true;
        Invoke("changeHit", hitCooldown);
    }

    private void death()
    {
        ded = true;

        if ((scriptableObject.enemyTier * 0.75f) * Random.Range(0, 100) >= 60)
            EventManager.BroadcastOnDeath(transform.position);

        transform.position = new Vector3(500f, 500f);
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);

        health = scriptableObject.health;
    }

    void changeHit()
    {
        hit = false;
    }
}
