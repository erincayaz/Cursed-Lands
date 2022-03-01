using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Configuration", menuName = "ScriptableObject/Skill Configuration")]
public class skillScriptableObject : ScriptableObject
{
    [Header("Stats")]
    public float radius;
    public float damage;
    public float amount;
    public float cooldown;
    public float speed;
    public float duration;
    public int level;

    [Header("Base Stats")]
    [SerializeField] float baseRadius;
    [SerializeField] float baseDamage;
    [SerializeField] float baseAmount;
    [SerializeField] float baseCooldown;
    [SerializeField] float baseSpeed;
    [SerializeField] float baseDuration;
    [SerializeField] int baseLevel;

    private void OnEnable()
    {
        radius = baseRadius;
        damage = baseDamage;
        amount = baseAmount;
        cooldown = baseCooldown;
        speed = baseSpeed;
        duration = baseDuration;
        level = baseLevel;
    }

    public Sprite sprite;
    public Animator anim;

    #region Stat changers
    public void increaseRadius(float num)
    {
        radius += num;
    }

    public void increaseDamage(float num)
    {
        damage += num;
    }

    public void increaseAmount(int num)
    {
        amount += num;
    }
    public void decraseCooldown(float num)
    {
        cooldown -= num;
    }
    public void increaseSpeed(float num)
    {
        speed += num;
    }
    public void increaseDuration(float num)
    {
        duration += num;
    }
    #endregion
}
