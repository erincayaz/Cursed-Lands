using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHurt : MonoBehaviour
{
    float lastHitByGarlic;

    // Start is called before the first frame update
    void Start()
    {
        lastHitByGarlic = Time.time;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Garlic")
        {
            skillScriptableObject skillScriptable = collision.GetComponent<garlicScript>().garlicStats;

            if (Time.time - lastHitByGarlic > skillScriptable.cooldown)
            {
                GetComponent<enemyMovement>().enemyHit(skillScriptable.damage, 2f);
                lastHitByGarlic = Time.time;
            }
        }
    }
}
