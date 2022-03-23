using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xpSpawner : MonoBehaviour
{
    [SerializeField] GameObject xpPrefab;
    List<GameObject> xps = new List<GameObject>();

    private void OnEnable()
    {
        EventManager.OnDeath += spawnXP;
    }

    private void OnDisable()
    {
        EventManager.OnDeath -= spawnXP;
    }

    void spawnXP(Vector3 position)
    {
        GameObject temp = availableXpExists();
        if (temp != null)
        {
            temp.SetActive(true);
            temp.transform.position = position;
        }
        else
        {
            GameObject gameObject = Instantiate(xpPrefab, position, Quaternion.identity);
            xps.Add(gameObject);
        }
    }

    GameObject availableXpExists()
    {
        if (xps.Count > 0)
        {
            foreach (GameObject xp in xps)
            {
                if (xp.transform.position.x == 1015f && xp.transform.position.y == 1015f)
                {
                    return xp;
                }
            }
        }

        return null;
    }
}
