using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameScene : MonoBehaviour
{
    public static MiniGameScene instance;
    [SerializeField]
    private int currentScore = 0;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
    }
}
