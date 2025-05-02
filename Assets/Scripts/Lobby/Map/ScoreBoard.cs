using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField]
    private string _scoreName;
    [SerializeField]
    private TextMeshProUGUI _scoreTxt;
    private void Awake()
    {
        LobbyScene.OnScoreUpdate += UpdateScoreBoard;
    }

    public void UpdateScoreBoard()
    {
        _scoreTxt.text = PlayerPrefs.GetInt(_scoreName, 0).ToString();
    }
}
