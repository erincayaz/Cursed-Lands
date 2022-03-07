using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillUpgrades : MonoBehaviour
{
    public List<skillScriptableObject> skillScriptableObjects;

    public List<GameObject> skillUnlockList;
    public List<GameObject> skillUpgradeList;

    public List<GameObject> availableSkills;

    private void Start()
    {
        availableSkills.Clear();
        foreach(var skill in skillScriptableObjects)
        {
            if(skill.amount == 0)
            {
                addObject(skillUnlockList, skill.name);
            }
            else if(skill.amount > 0)
            {
                addObject(skillUpgradeList, skill.name);
            }
        }
    }

    void addObject(List<GameObject> myList, string str)
    {
        foreach(var element in myList)
        {
            if(element.name.IndexOf(str) != -1)
            {
                availableSkills.Add(element);
            }
        }
    }

    public void theFunction()
    {
        print("aaaa");
    }
    public void increaseRadius(string s)
    {
        foreach (var skill in skillScriptableObjects)
        {
            if (skill.name == s)
            {
                skill.level++;
                skill.radius += skill.radius * 10 / 100;
            }
        }
    }

    public void increaseDamage(string s)
    {
        foreach (var skill in skillScriptableObjects)
        {
            if (skill.name == s)
            {
                skill.level++;
                skill.damage += 2;
            }
        }
    }

    public void increaseAmount(string s)
    {
        foreach(var skill in skillScriptableObjects)
        {
            if (skill.name == s)
            {
                skill.level++;
                skill.amount += 1;
            }
        }
    }

    public void decreaseCooldown(string s)
    {
        foreach (var skill in skillScriptableObjects)
        {
            if (skill.name == s)
            {
                skill.level++;
                skill.cooldown -= skill.cooldown * 0.15f;
            }
        }
    }

    public void increaseSpeed(string s)
    {
        foreach (var skill in skillScriptableObjects)
        {
            if (skill.name == s)
            {
                skill.level++;
                skill.speed += skill.speed * .2f;
            }
        }
    }
    public void increaseDuration(string s)
    {
        foreach (var skill in skillScriptableObjects)
        {
            if (skill.name == s)
            {
                skill.level++;
                skill.duration += skill.duration * .2f;
            }
        }
    }
}
