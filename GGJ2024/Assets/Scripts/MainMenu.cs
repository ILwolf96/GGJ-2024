using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    
    public void Options()
    {
        SceneManager.LoadSceneAsync(1);

    }
    public void HowToPlay()
    {
        SceneManager.LoadSceneAsync(2);

    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(3);

    }

   
}