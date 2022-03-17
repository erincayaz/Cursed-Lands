using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterScript : MonoBehaviour
{
    public skillScriptableObject holyWater;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<enemyMovement>().enemyHit(holyWater.damage / holyWater.duration * Time.deltaTime, 0f);
        }
    }
}
