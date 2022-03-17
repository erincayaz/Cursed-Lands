using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    [SerializeField] GameObject levelUpCanvas;


    public int weaponCount;
    public int maxWeaponCount;

    private void Start()
    {
        weaponCount = 1;
        //for(int i = 0; i < maxWeaponCount; i++)
        //{
        //    GameObject toSpawn = Instantiate(UIGrid, UISpawnPos.position + new Vector3(.5f, 0f, 0f) * i, Quaternion.identity);
        //    UIGridList.Add(toSpawn);
        //}
    }
    private void OnEnable()
    {
        EventManager.OnLevelUp += LevelUpOpen;
    }

    private void OnDisable()
    {
        EventManager.OnLevelUp -= LevelUpOpen;
    }

    public void LevelUpClose()
    {
        levelUpCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    void LevelUpOpen()
    {
        levelUpCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void unlockNewWeapon(string name)
    {
        Debug.Log("unlocking new weapon");
        weaponCount += 1;
        Debug.Log("weapon count: " + weaponCount);
        //skillScriptableObject currentSkill = getScriptableObject(name);
    }
}
