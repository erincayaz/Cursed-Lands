using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonScript : MonoBehaviour
{
    public skillScriptableObject poison;
    public LayerMask enemyMask;

    public float hitDuration;

    void Start()
    {
        StartCoroutine(despawnPoison());
    }

    void Update()
    {
        Attack();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transform.position, poison.radius);
    //}

    void Attack()
    {
        RaycastHit2D[] enemies = Physics2D.CircleCastAll(transform.position, poison.radius, Vector2.zero, Mathf.Infinity, enemyMask);
        //damage enemy here
        foreach(var enemy in enemies)
        {
            enemy.transform.GetComponent<enemyMovement>().enemyHit(poison.damage / hitDuration * Time.deltaTime, 0f);
        }
    }
    IEnumerator despawnPoison()
    {
        yield return new WaitForSeconds(hitDuration);
        // change to pool position
        transform.position = new Vector2(10f, 10f);
    }
}
