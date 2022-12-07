using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsSceneControl : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
