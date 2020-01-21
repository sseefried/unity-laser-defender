using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score = 0; // for debug only
    
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int objectCount = FindObjectsOfType(GetType()).Length;
        if (objectCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }


}
