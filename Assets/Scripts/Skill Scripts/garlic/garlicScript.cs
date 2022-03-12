using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class garlicScript : MonoBehaviour
{
    public skillScriptableObject garlicStats;
    float lastAttack;
    [SerializeField] float rotationSpeed;

    private void Start()
    {
        if(garlicStats.amount == 0)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(0f, 0f, (transform.eulerAngles.z % 360) + Time.deltaTime * rotationSpeed);
    }

    public void ActivateGarlic()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }

}
