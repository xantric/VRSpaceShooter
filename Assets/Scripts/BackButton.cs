using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void BackToMenu(){
        SceneManager.LoadScene("1 Start Scene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
