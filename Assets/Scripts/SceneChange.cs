using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

	public void ChangeScene(string scene)
    {
        Debug.Log("here");

        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
