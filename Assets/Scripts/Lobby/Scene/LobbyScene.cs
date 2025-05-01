using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    public static LobbyScene Instance;
    // 추후 프리팹으로 된 플레이어 생성
    public GameObject _playerPrefab;
    // 점수를 갱신하는 옵저버
    public static event Action OnScoreUpdate;
    public static event Action MoveSceneSave;
    public static event Action EndGameSave;
    public int _showResult;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

    }

    private void OnEnable()
    {
      
    }
    private void Start()
    {
        Init();
        UpdateScore();
    }

    private void Init()
    {
        LoadData();
    }

    public void UpdateScore()
    {
        OnScoreUpdate?.Invoke();

        //점수 관련 NPC가 성공, 실패 UI를 보여주어야 하는가
        var prevScene = PlayerPrefs.GetString("PrevScene");
        var result = PlayerPrefs.GetInt("IsHighScore", -1);
        if ( prevScene != SceneManager.GetActiveScene().name && result != -1)
        {
            _showResult = result;
        }
        else
        {
            _showResult = -1;
        }
        PlayerPrefs.SetString("PrevScene", SceneManager.GetActiveScene().name);

    }
    private void LoadData()
    {
        SetPositon("PlayerPosition", _playerPrefab.transform);
        SetPositon("CameraPosition", Camera.main.transform);
    }
    private void SetPositon(string name, Transform obj)
    {
        string posString = PlayerPrefs.GetString(name, "0,0,0");
        string[] values = posString.Split(',');
        Vector3 loadedPosition = new Vector3(
            float.Parse(values[0]),
            float.Parse(values[1]),
            float.Parse(values[2])
            );
        obj.transform.position = loadedPosition;
    }

    // 게임이 꺼질때 발동해야 하는 함수

    // 씬이 로드될때 발동해야 하는 함수


    //테스트용
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerPrefs.DeleteKey("HighScore");
        }
    }

    public void LoadSceneSave()
    {
      //  MoveSceneSave?.Invoke();
    }
    private void OnApplicationQuit()
    {
  //      EndGameSave?.Invoke();
    }
}
