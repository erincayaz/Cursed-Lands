using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMovement : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnRotate += SpriteRotateSubscriber;
    }

    private void OnDisable()
    {
        EventManager.OnRotate -= SpriteRotateSubscriber;
    }

    void SpriteRotateSubscriber(float h)
    {
        if ((h < 0 && transform.localScale.x < 0) || (h > 0 && transform.localScale.x > 0))
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
