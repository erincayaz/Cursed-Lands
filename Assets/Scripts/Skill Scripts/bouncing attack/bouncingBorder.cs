using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncingBorder : MonoBehaviour
{
    public int xMult, yMult;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bouncing Attack")
        {
            collision.GetComponent<Rigidbody2D>().velocity *= new Vector2(xMult, yMult);
        }
    }
}
