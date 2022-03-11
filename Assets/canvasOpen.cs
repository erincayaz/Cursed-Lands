using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasOpen : MonoBehaviour
{
    [SerializeField] GameObject upgradeCanvas;
    skillUpgrades upgradeScript;
    RectTransform rectTransform;
    private void Start()
    {
        upgradeScript = FindObjectOfType<skillUpgrades>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            //if(upgradeCanvas.activeInHierarchy)
            //{
            //    foreach(var skillObj in upgradeScript.availableSkills)
            //    {
            //        if (skillObj.transform.position != new Vector3(1000f, 1000f, 0f)) skillObj.transform.position = new Vector3(1000f, 1000f, 0f);
            //    }
            //}
            upgradeCanvas.SetActive(!upgradeCanvas.activeInHierarchy);
        }
    }

    public void deactivateCanvas()
    {
        //foreach (var skillObj in upgradeScript.availableSkills)
        //{
        //    if (skillObj.transform.position != new Vector3(1000f, 1000f, 0f)) skillObj.transform.position = new Vector3(1000f, 1000f, 0f);
        //}
        upgradeCanvas.SetActive(false);
    }
}
