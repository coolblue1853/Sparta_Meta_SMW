using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class MiniGameScene : MonoBehaviour
{
    public MiniGameState State = Define.MiniGameState.None;

    public event Action<int> OnScoreChanged;

    public static MiniGameScene Instance;
    public MiniGameUI GameUI;
    [SerializeField] private int _currentScore = 0;

    private int _highScore;
    public bool IsCanGoLobby = false;
    private void Awake()
    {

        if(Instance == null)
        {
            Instance = this;
        }

    }
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        GameUI = MiniGameUI.Instance;
        Time.timeScale = 0;
        _currentScore = 0;
        GameUI.SetStartUI(true);
        State = MiniGameState.Init;
        IsCanGoLobby = false;
    }
    public void GameStart()
    {
        Time.timeScale = 1;
        GameUI.SetStartUI(false);
        GameUI.SetInGameUI(true);
        State = MiniGameState.InGame;
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void GameEnd()
    {
        GameUI.SetInGameUI(false);
        GameUI.SetEndGameUI(true, _currentScore, _highScore);

        // 최고점수 갱신
        if(_highScore < _currentScore)
        {
            PlayerPrefs.SetInt("HighScore", _currentScore);
            PlayerPrefs.SetInt("IsHighScore", 1);
        }
        else
        {
            PlayerPrefs.SetInt("IsHighScore", 0);
        }

        State = MiniGameState.End;
        PlayerPrefs.SetString("PrevScene", SceneManager.GetActiveScene().name);
        Invoke("LobbyInvoke", 1f);
    }
    void LobbyInvoke()
    {
        IsCanGoLobby = true;
    }

    public void GoLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void AddScore(int score)
    {
        _currentScore += score;
        OnScoreChanged?.Invoke(_currentScore);
    }
    public int GetScore()
    {
        return _currentScore;
    }
}
