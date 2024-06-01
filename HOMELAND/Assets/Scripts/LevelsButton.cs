using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelsButton : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        int UnlockL1= PlayerPrefs.GetInt("Unlock Level 1", 1);
        for (int i = 0; i < buttons.Length; i++)
         {
            buttons[i].interactable = false;
         }
        for (int i = 0; i < UnlockL1; i++)
        {
            buttons[i].interactable= true;
        }
    }


    public void OpenL1()
    {
        SceneManager.LoadScene(3);
        Debug.Log("Level 1 is opened");
    }

    public void OpenL2() 
    {
        SceneManager.LoadScene (4);
        Debug.Log("Level 2 is opened");
    }

    public void OpenL3()
    {
        SceneManager.LoadScene(5);
        Debug.Log("Level 3 is opened");
    }
}
