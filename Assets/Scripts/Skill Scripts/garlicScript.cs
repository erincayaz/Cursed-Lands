using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class garlicScript : MonoBehaviour
{
    public skillScriptableObject garlicStats;
    float lastAttack;

    // Start is called before the first frame update
    void Start()
    {
        lastAttack = Time.time;
    }

}
