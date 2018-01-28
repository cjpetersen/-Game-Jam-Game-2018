using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript1 : MonoBehaviour {

    public void ChangeScene(string sceneName)
    {
        Debug.Log("Navigating to " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}

