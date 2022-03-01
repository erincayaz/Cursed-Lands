using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holyBomb : MonoBehaviour
{
    public skillScriptableObject holyBombScr;

    [Header("Objects to switch between")]
    public GameObject bomb;
    public GameObject water;
    bool exploded;

    [Header("Travel variables")]
    public Vector3 targetPos;
    Vector3 dir;

    [Header("Speed variables")]
    public AnimationCurve curve;
    [SerializeField] float speed;
    float multiplier, spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        bomb.SetActive(true);
        water.SetActive(false);

        dir = (targetPos - transform.position).normalized;

        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!exploded)
        {
            multiplier = curve.Evaluate(Time.time - spawnTime);

            if (Vector3.Distance(transform.position, targetPos) >= .2f)
            {
                transform.position += dir * Time.deltaTime * speed * multiplier;
            }
            else
            {
                exploded = true;
                switchActives();
                StartCoroutine(pool());
            }
        }
    }

    void switchActives()
    {
        bomb.SetActive(false);
        water.SetActive(true);
    }

    public IEnumerator pool()
    {
        yield return new WaitForSeconds(holyBombScr.duration);

        transform.position = new Vector3(997.5f, 997.5f, 0f);
        bomb.SetActive(true);
        water.SetActive(false);
        FindObjectOfType<holyBombSpawner>().bombList.Add(gameObject);
    }
    public void unpool(Vector3 startPos1, Vector3 targetPos1)
    {
        transform.position = startPos1;
        targetPos = targetPos1;
        exploded = false;
        spawnTime = Time.time;
    }
}
