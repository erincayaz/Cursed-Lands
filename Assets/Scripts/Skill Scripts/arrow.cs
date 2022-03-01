using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    [SerializeField] skillScriptableObject arrowStats;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<enemyMovement>().enemyHit(arrowStats.damage, 2f);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = new Vector3(1000f, 1000f);
        }
    }

}
