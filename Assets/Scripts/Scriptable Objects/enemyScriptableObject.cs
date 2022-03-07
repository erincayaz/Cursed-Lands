using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Configuration", menuName = "ScriptableObject/Enemy Configuration")]
public class enemyScriptableObject : ScriptableObject
{
    public float health = 10f;
    public float speed = 1f;
    public Sprite sprite;
    public float attack = 5f;
    public int enemyTier;
}
