using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    
    public void Skippanel2()
    {
        SceneManager.LoadScene(2);
        Debug.Log("story2 is opened");

    }

    public void Skippanel3()
    {
        SceneManager.LoadScene(3);
        Debug.Log("story3 is opened");

    }

    public void LEVEL1()
    {
        SceneManager.LoadScene(4);
        Debug.Log("Level1 is opened");

    }

  
}
