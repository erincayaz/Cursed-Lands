using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    [SerializeField] GameObject levelUpCanvas;

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

}
