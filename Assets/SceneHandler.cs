using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public void SetScene(int sceneIndex)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(sceneIndex));
    }
}