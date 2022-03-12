using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    public skillScriptableObject arrowStats;

    Transform player;
    float projectileLastAttack;
    List<GameObject> arrows = new List<GameObject>();

    private void Start()
    {
        GameObject tempProjectile = Instantiate(projectile, new Vector3(1000f, 1000f), Quaternion.identity);
        arrows.Add(tempProjectile);

        projectileLastAttack = Time.time;
        player = transform.parent.parent.transform;
    }

    private void Update()
    {
        if (Time.time - projectileLastAttack > arrowStats.cooldown)
        {
            int i = 0;
            for (i = 0; i < arrowStats.amount; i++)
            {
                Invoke("Attack", i * 0.2f);
            }

            projectileLastAttack = Time.time + (i * 0.2f);
        }
    }

    public void Attack()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(player.transform.position, new Vector2(25f, 15f), 0f, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemy"));

        if (hits.Length != 0)
        {
            Vector3 minPos = hits[0].transform.position;
            float minDistance = Vector3.Distance(player.transform.position, minPos);
            foreach (RaycastHit2D hit in hits)
            {
                float distance = Vector3.Distance(player.transform.position, hit.transform.position);
                if (distance < minDistance)
                {
                    minPos = hit.transform.position;
                    minDistance = distance;
                }
            }

            Vector2 dir = minPos - player.transform.position;
            dir = dir.normalized;
            float rot_z = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;

            if (!availableArrowExist())
            {
                GameObject tempProjectile = Instantiate(projectile, player.transform.position, Quaternion.Euler(0f, 0f, rot_z));
                tempProjectile.GetComponent<Rigidbody2D>().velocity = dir * arrowStats.speed;

                arrows.Add(tempProjectile);
            }
            else
            {
                foreach (GameObject arrow in arrows)
                {
                    if (poolArrow(arrow, rot_z, dir))
                    {
                        break;
                    }
                }
            }
        }
    }

    bool availableArrowExist()
    {
        foreach(GameObject arrow in arrows)
        {
            if(arrow.transform.position.x == 1000f && arrow.transform.position.y == 1000f)
            {
                return true;
            }

            Renderer renderer = arrow.GetComponent<SpriteRenderer>();
            if (!renderer.isVisible) 
            {
                arrow.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                arrow.transform.position = new Vector3(1000f, 1000f);
            }
        }

        return false;
    }

    bool poolArrow(GameObject arrow, float rot_z, Vector2 dir)
    {
        if(arrow.transform.position.x == 1000f && arrow.transform.position.y == 1000f)
        {
            arrow.transform.position = player.transform.position;
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
            arrow.GetComponent<Rigidbody2D>().velocity = dir * arrowStats.speed;

            return true;
        }

        return false;
    }

}
