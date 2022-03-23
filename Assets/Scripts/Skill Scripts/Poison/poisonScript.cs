using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonScript : MonoBehaviour
{
    public skillScriptableObject poison;
    public LayerMask enemyMask;

    void Start()
    {
        StartCoroutine(despawnPoison());
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        RaycastHit2D[] enemies = Physics2D.CircleCastAll(transform.position, poison.radius, Vector2.zero, Mathf.Infinity, enemyMask);
        //damage enemy here
        foreach(var enemy in enemies)
        {
            enemy.transform.GetComponent<enemyMovement>().enemyHit(poison.damage / poison.duration * Time.deltaTime, 0f);
        }
    }
    IEnumerator despawnPoison()
    {
        yield return new WaitForSeconds(poison.duration);
        // change to pool position
        //transform.position = new Vector2(10f, 10f);
        Destroy(this.gameObject);
    }
}
