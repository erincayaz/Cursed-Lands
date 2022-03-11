using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawner : MonoBehaviour
{
    [SerializeField] AnimationCurve spawnControl;
    [SerializeField] List<GameObject> enemyTypes;
    [SerializeField] GameObject enemyPrefab;

    List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
