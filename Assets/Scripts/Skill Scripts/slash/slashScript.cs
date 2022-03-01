using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashScript : MonoBehaviour
{
    [SerializeField] GameObject tempSlash;
    [SerializeField] int maxSlash;
    public skillScriptableObject slashStats;

    Transform player;
    List<GameObject> slashes = new List<GameObject>();
    List<GameObject> activeSlashes = new List<GameObject>();

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.parent.transform;
        startTime = Time.time;

        for (int i = 0; i < maxSlash; i++)
        {
            GameObject tempProjectile = Instantiate(tempSlash, player.GetChild(0));
            slashes.Add(tempProjectile);

            tempProjectile.transform.position = i % 2 == 0 ? 
                new Vector3(tempProjectile.transform.position.x, tempProjectile.transform.position.y + (1f * (i / 2)), tempProjectile.transform.position.z)
                : new Vector3(-tempProjectile.transform.position.x, tempProjectile.transform.position.y + (1f * (i / 2)), tempProjectile.transform.position.z);
            tempProjectile.transform.localScale = i % 2 == 0 ?
                tempProjectile.transform.localScale :
                new Vector3(-tempProjectile.transform.localScale.x, tempProjectile.transform.localScale.y, tempProjectile.transform.localScale.z);


            tempProjectile.SetActive(false);
        }
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (Time.time - startTime < slashStats.duration && !slashes[0].active)
        {
            float tempTime = 0f;
            foreach(GameObject slash in activeSlashes)
            {
                StartCoroutine(WaitAndChange(tempTime, slash, true));
                tempTime += 0.2f;
            }
        }
        else if (Time.time - startTime > slashStats.duration && slashes[0].active)
        {
            float tempTime = 0f;
            foreach(GameObject slash in activeSlashes)
            {
                StartCoroutine(WaitAndChange(tempTime, slash, false));
                tempTime += 0.2f;
            }
        }
        else if (Time.time - startTime > slashStats.cooldown)
        {
            startTime = Time.time;
        }
   
        if (slashStats.amount > activeSlashes.Count)
        {
            slashes[activeSlashes.Count].SetActive(true);
            activeSlashes.Add(slashes[activeSlashes.Count]);

            startTime = Time.time - slashStats.cooldown;
        }
        
    }

    private IEnumerator WaitAndChange(float waitTime, GameObject gameObject, bool change)
    {
        yield return new WaitForSeconds(waitTime);

        gameObject.SetActive(change);
    }
}
