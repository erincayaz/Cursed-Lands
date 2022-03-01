using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] GameObject healthBar;

    public float baseHealth;
    float health;
    private float lastDamageTaken;

    Rigidbody2D rb;

    private void OnEnable()
    {
        EventManager.OnHit += PlayerHitSubscriber;
    }

    private void OnDisable()
    {
        EventManager.OnHit -= PlayerHitSubscriber;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = baseHealth;
        lastDamageTaken = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //transform.position += new Vector3(h * speed * Time.deltaTime, v * speed * Time.deltaTime);
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal"), v = Input.GetAxis("Vertical");

        EventManager.BrodcastOnRotate(h);

        rb.velocity = new Vector2(h, v) * speed;
    }

    void PlayerHitSubscriber(float damage)
    {
        if (Time.time - lastDamageTaken > 0.33f)
        {
            health -= damage;
            Vector3 temp = new Vector3(health / baseHealth, healthBar.transform.localScale.y);
            healthBar.transform.localScale = temp;

            lastDamageTaken = Time.time;
        }
    }
}
