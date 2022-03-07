using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float xpCollectRadius;
    [SerializeField] float xpSpeed;
    [SerializeField] GameObject healthBar;
    [SerializeField] Image xpBar;
    [SerializeField] LayerMask xpMask;

    public float baseHealth;
    public float levelUpXP;
    float health;
    public float xp;
    private float lastDamageTaken;

    Rigidbody2D rb;
    Animator anim;

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
        anim = transform.GetChild(0).GetComponent<Animator>();

        health = baseHealth;
        lastDamageTaken = Time.time;

        xpBar.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        collectXP();
        
        //transform.position += new Vector3(h * speed * Time.deltaTime, v * speed * Time.deltaTime);
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal"), v = Input.GetAxis("Vertical");

        EventManager.BrodcastOnRotate(h);

        rb.velocity = new Vector2(h, v) * speed;

        if (h == 0 && v == 0)
            anim.SetBool("walking", false);
        else
            anim.SetBool("walking", true);
    }

    void collectXP()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, xpCollectRadius, xpMask);
        if (coll.Length > 0)
        {
            foreach (Collider2D col in coll)
            {
                Vector3 dir = transform.position - col.transform.position;
                col.transform.position += dir.normalized * Time.deltaTime * xpSpeed;

                if (Vector3.Distance(transform.position, col.transform.position) < 0.5f)
                {
                    Color temp = col.GetComponent<SpriteRenderer>().color;
                    if (temp.a == 1f)
                    {
                        xp += 1f;
                    }
                    else if (temp.a == 254f / 255f)
                    {
                        xp += 10f;
                    }
                    else
                    {
                        xp += 100f;
                    }

                    if (xp > levelUpXP)
                    {
                        xp = xp - levelUpXP;
                        levelUpXP += 50f;
                    }

                    col.transform.position = new Vector3(1015f, 1015f, 0f);
                    xpBar.fillAmount = xp / levelUpXP;
                }
            }
        }
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
