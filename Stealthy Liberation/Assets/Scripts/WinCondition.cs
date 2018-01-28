using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour{


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("Win");
        }
    }
}
