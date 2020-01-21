using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
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
}
