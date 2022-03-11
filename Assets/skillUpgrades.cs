using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillUpgrades : MonoBehaviour
{
    public List<skillScriptableObject> skillScriptableObjects;

    public List<GameObject> skillUnlockList;
    public List<GameObject> skillUpgradeList;

    public List<GameObject> availableSkills;

    private void OnEnable()
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

        List<int> randomNumbers = new List<int>();
        int randomInt;
        for (int i = 0; i < 3; i++)
        {
            do
            {
                randomInt = Random.Range(0, availableSkills.Count);
            } while (randomNumbers.IndexOf(randomInt) != -1);

            randomNumbers.Add(randomInt);
            RectTransform rectTransform = availableSkills[randomInt].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(0f, (1 - i) * 275f, 0f);
        }
    }

    private void OnDisable()
    {
        foreach (var skillObj in availableSkills)
        {
            if (skillObj.GetComponent<RectTransform>().anchoredPosition != new Vector2(1000f, 1000f)) skillObj.transform.position = new Vector3(1000f, 1000f, 0f);
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
