using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shurikenSpawner : MonoBehaviour
{
    public skillScriptableObject shurikenStats;
    [SerializeField] GameObject shurikenPrefab;
    [SerializeField] int maximumShuriken;

    List<GameObject> shurikens = new List<GameObject>();
    List<GameObject> activeShurikens = new List<GameObject>();

    float projectileLastAttack;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maximumShuriken; i++)
        {
            GameObject tempProjectile = Instantiate(shurikenPrefab, new Vector3(1005f, 1005f), Quaternion.identity);
            tempProjectile.GetComponent<Orbit>().player = transform.parent.parent.transform;
            shurikens.Add(tempProjectile);
            tempProjectile.SetActive(false);
        }

        for(int i = 0; i < shurikenStats.amount; i++)
        {
            activeShurikens.Add(shurikens[i]);
        }

        projectileLastAttack = Time.time;
        player = transform.parent.parent.transform;

        InvokeRepeating("Attack", 2f, shurikenStats.cooldown);
    }

    void Attack()
    {
        if(shurikenStats.amount > activeShurikens.Count)
        {
            activeShurikens.Add(shurikens[activeShurikens.Count]);
        }
        
        float tempTime = 0f;
        foreach (GameObject shuriken in activeShurikens)
        {
            StartCoroutine(WaitAndChange(tempTime, shuriken));
            StartCoroutine(WaitAndPool(tempTime + shurikenStats.duration, shuriken));

            tempTime += 0.5f;
        }
        
    }

    private IEnumerator WaitAndChange(float waitTime, GameObject gameObject)
    {
        yield return new WaitForSeconds(waitTime);

        gameObject.SetActive(true);
        gameObject.transform.position = player.transform.position;
        gameObject.GetComponent<Orbit>().Throw();
    }

    private IEnumerator WaitAndPool(float waitTime, GameObject gameObject)
    {
        yield return new WaitForSeconds(waitTime);

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.transform.position = new Vector3(1005f, 1005f, 0f);
        gameObject.SetActive(false);
    }
}
