using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMenu : MonoBehaviour
{

    public void RetryGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void MenuGame()
    {

        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
