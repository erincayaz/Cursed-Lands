using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public delegate void PlayerRotateAction(float h);
    public static event PlayerRotateAction OnRotate;

    public delegate void PlayerHitAction(float damage);
    public static event PlayerHitAction OnHit;

    public delegate void BibleAnimation(bool animControl);
    public static event BibleAnimation OnChange;

    public delegate void EnemySpawnAction(int i);
    public static event EnemySpawnAction OnSpawn;

    public delegate void EnemyDeathAction(Vector3 position);
    public static event EnemyDeathAction OnDeath;

    public static void BroadcastOnDeath(Vector3 position)
    {
        OnDeath?.Invoke(position);
    }

    public static void BrodcastOnRotate(float h)
    {
        OnRotate?.Invoke(h);
    }

    public static void BrodcastOnHit(float h)
    {
        OnHit?.Invoke(h);
    }

    public static void BrodcastOnChange(bool animControl)
    {
        OnChange?.Invoke(animControl);
    }

    public static void BrodcastOnSpawn(int i)
    {
        OnSpawn?.Invoke(i);
    }

}
