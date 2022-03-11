using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeObjectController : MonoBehaviour
{
    private Vector3 stackPos = new Vector3(10000f, 10000f, 0f);
    RectTransform rectTransform;
    void Start()
    {
    //    rectTransform = GetComponent<RectTransform>();
    //    rectTransform.anchoredPosition = stackPos;
    }

    public void stackButton()
    {
        //rectTransform.anchoredPosition = stackPos;
    }
}
