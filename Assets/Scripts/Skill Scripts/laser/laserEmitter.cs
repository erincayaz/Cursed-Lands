using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserEmitter : MonoBehaviour
{
    [SerializeField] skillScriptableObject laserScriptable;
    GameObject target;

    float lastLaserTime;
    void Start()
    {
        
    }

    void Update()
    {
        locateStrongestEnemy();

        if(Time.time - lastLaserTime >= laserScriptable.cooldown && target != null)
        {
            emitLaser();
        }
    }

    void locateStrongestEnemy()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(15f, 10f), 0f, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemy"));

        if (hits.Length > 0)
        {
            target = hits[0].transform.gameObject;
            foreach (RaycastHit2D hit in hits)
            {
                float health = hit.transform.gameObject.GetComponent<enemyScriptableObject>().health;

                if (health > target.GetComponent<enemyScriptableObject>().health)
                {
                    target = hit.transform.gameObject;
                }
            }
        }
        else
            target = null;
    }

    void emitLaser()
    {

    }
}
