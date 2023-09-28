using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

   

    public void StartGame()
    {
        SceneManager.LoadScene(sceneManager.MainLevel);
    }

    public void Instructions()
    {
        SceneManager.LoadScene(sceneManager.Instructions);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(sceneManager.MainMenu);
    }


}
