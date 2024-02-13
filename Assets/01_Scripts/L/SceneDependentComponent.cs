using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDependentComponent : MonoBehaviour
{
    public ChatManager componentA;

    void Awake()
    {
        componentA = GetComponent<ChatManager>();
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Town" || currentSceneName == "Raid")
        {
            componentA.enabled = true;

        }
        else
        {
            componentA.enabled = false;

        }
    }
}
