using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int score;

    private void OnEnable()
    {
        GameEvents.OnPlayerDied.AddListener(PlayerDied);
        GameEvents.OnPickUpCollected.AddListener(PickUpCollected);
        GameEvents.OnScoreChanged.AddListener(ScoreChanged);
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDied.RemoveListener(PlayerDied);
        GameEvents.OnPickUpCollected.RemoveListener(PickUpCollected);
        GameEvents.OnScoreChanged.RemoveListener(ScoreChanged);
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void PlayerDied()
    {
        Debug.Log("Player Died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PickUpCollected(PickUp pickup)
    {
        score++;
        Debug.Log(score);
        GameEvents.OnScoreChanged?.Invoke(score);
    }

    private void ScoreChanged(int score)
    {
        Debug.Log("Score changed");
    }

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log($"Scene Loaded: {arg0.name}");
    }
}
