using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{

    public void Skippanel()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Level Menu is opened");
    }

}
