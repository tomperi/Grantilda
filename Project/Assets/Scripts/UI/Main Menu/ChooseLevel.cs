using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class ChooseLevel {

    internal static void GoToLevel(int id)
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + id;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
