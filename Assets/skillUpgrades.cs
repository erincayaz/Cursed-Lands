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
}
