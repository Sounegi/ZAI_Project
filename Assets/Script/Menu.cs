using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("EnterGame");
        SceneManager.LoadScene("MainGame");
    }

    public void GoogleLogin()
    {
        //Implement Google Sign-in and show profilepic&name

        StartGame();
    }
}
