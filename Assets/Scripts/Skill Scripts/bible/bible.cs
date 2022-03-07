using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bible : MonoBehaviour
{
    Animator anim;
    [SerializeField] skillScriptableObject bibleStats;

    private void OnEnable()
    {
        EventManager.OnChange += AnimatorChangeSubscriber;
    }

    private void OnDisable()
    {
        EventManager.OnChange -= AnimatorChangeSubscriber;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void AnimatorChangeSubscriber(bool change)
    {
        anim.SetBool("opacity", change);

        GetComponent<BoxCollider2D>().enabled = change ? false : true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<enemyMovement>().enemyHit(bibleStats.damage, 2f);
        }
    }

}
