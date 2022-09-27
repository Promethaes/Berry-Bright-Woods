using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{

    public void ToGameScene()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }


    static int index = 0;
    public void ToNextScene()
    {
        SceneManager.LoadSceneAsync(2 + index);
    }

}
