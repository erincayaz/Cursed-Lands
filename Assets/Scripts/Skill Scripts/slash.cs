using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slash : MonoBehaviour
{
    public skillScriptableObject slashStats;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<enemyMovement>().enemyHit(slashStats.damage, 2f);
        }
    }
}
