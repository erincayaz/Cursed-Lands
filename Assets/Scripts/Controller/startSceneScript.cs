using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("loadStartScreen", 1.5f);
    }

    void loadStartScreen ()
    {
        SceneManager.LoadScene("Start Scene");
    }
}
