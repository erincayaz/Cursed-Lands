using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameObject arrow;

    // Update is called once per frame
    void Update()
    {
        if(arrow.activeInHierarchy == true)
            arrow.GetComponent<arrowScript>().Attack();
    }
}
