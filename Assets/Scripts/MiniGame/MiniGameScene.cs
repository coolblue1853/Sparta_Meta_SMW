using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class MiniGameScene : MonoBehaviour
{

    public MiniGameState _state = Define.MiniGameState.None;

    public event Action<int> _OnScoreChanged;

    public static MiniGameScene instance;
    public MiniGameUI _gameUI;
    [SerializeField]
    private int _currentScore = 0;
    private int _highScore;
    public bool isCanGoLobby = false;
    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }

    }
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _gameUI = MiniGameUI.instance;
        Time.timeScale = 0;
        _currentScore = 0;
        _gameUI.SetStartUI(true);
        _state = MiniGameState.Init;
        isCanGoLobby = false;
    }
    public void GameStart()
    {
        Time.timeScale = 1;
        _gameUI.SetStartUI(false);
        _gameUI.SetInGameUI(true);
        _state = MiniGameState.InGame;
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void GameEnd()
    {
        _gameUI.SetInGameUI(false);
        _gameUI.SetEndGameUI(true, _currentScore, _highScore);

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

        _state = MiniGameState.End;
        PlayerPrefs.SetString("PrevScene", SceneManager.GetActiveScene().name);
        Invoke("LobbyInvoke", 1f);
    }
    void LobbyInvoke()
    {
        isCanGoLobby = true;
    }

    public void GoLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void AddScore(int score)
    {
        _currentScore += score;
        _OnScoreChanged?.Invoke(_currentScore);
    }
    public int GetScore()
    {
        return _currentScore;
    }
}
