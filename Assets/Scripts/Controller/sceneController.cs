using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneController : MonoBehaviour
{
    [SerializeField] string startScene;
    [SerializeField] string levelScene;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void loadLevelScene()
    {
        SceneManager.LoadScene(levelScene);
    }
    public void loadStartScene()
    {
        SceneManager.LoadScene(startScene);
    }
}
