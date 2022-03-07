using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class garlicScript : MonoBehaviour
{
    public skillScriptableObject garlicStats;
    float lastAttack;
    [SerializeField] float rotationSpeed;

    private void Update()
    {
        transform.eulerAngles = new Vector3(0f, 0f, (transform.eulerAngles.z % 360) + Time.deltaTime * rotationSpeed);
    }

}
