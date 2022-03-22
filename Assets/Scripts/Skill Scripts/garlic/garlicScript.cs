using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class garlicScript : MonoBehaviour
{
    public skillScriptableObject garlicStats;
    [SerializeField] float rotationSpeed;

    float garlicRadius;

    private void Start()
    {
        garlicRadius = garlicStats.radius;

        if(garlicStats.amount == 0)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(0f, 0f, (transform.eulerAngles.z % 360) + Time.deltaTime * rotationSpeed);

        if(garlicStats.radius > garlicRadius)
        {
            transform.localScale += transform.localScale / 10f;
            garlicRadius = garlicStats.radius;
        }
    }

    public void ActivateGarlic()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }

}
