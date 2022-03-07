using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombSpawner : MonoBehaviour
{
    public skillScriptableObject bombStats;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] int maximumBomb;

    List<GameObject> bombs = new List<GameObject>();
    List<GameObject> activeBombs = new List<GameObject>();

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maximumBomb; i++)
        {
            GameObject tempProjectile = Instantiate(bombPrefab, new Vector3(1010f, 1010f), Quaternion.identity);
            bombs.Add(tempProjectile);
        }

        for (int i = 0; i < bombStats.amount; i++)
        {
            activeBombs.Add(bombs[i]);
        }

        player = transform.parent.parent.transform;
        InvokeRepeating("Attack", 0.5f, bombStats.cooldown);
    }

    void Attack()
    {
        if (bombStats.amount > activeBombs.Count)
        {
            activeBombs.Add(bombs[activeBombs.Count]);
        }

        float tempTime = 0f;
        foreach (GameObject bomb in activeBombs)
        {
            StartCoroutine(WaitAndChange(tempTime, bomb));

            tempTime += 0.1f;
        }
    }

    private IEnumerator WaitAndChange(float waitTime, GameObject gameObject)
    {
        yield return new WaitForSeconds(waitTime);

        gameObject.transform.position = player.transform.position;
        Transform enemyPos;

        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(20f, 15f), 0f, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemy"));
        if (hits.Length != 0)
        {
            int RandomNumber = Random.Range(0, hits.Length - 1);
            enemyPos = hits[RandomNumber].transform;
        }
        else
        {
            enemyPos = new GameObject().transform;
            enemyPos.position = new Vector3(player.transform.position.x + Random.Range(-8, 8), player.transform.position.y + Random.Range(-8, 8));
        }

        gameObject.GetComponent<bombScript>().Throw(player.transform, enemyPos);
    }
}
