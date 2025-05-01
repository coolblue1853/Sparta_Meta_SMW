using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : MonoBehaviour
{
    // 추후 프리팹으로 된 플레이어 생성
    public GameObject _playerPrefab;
    // 점수를 갱신하는 옵저버
    public static event Action OnScoreUpdate;

    private void Awake()
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
        OnScoreUpdate.Invoke();
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
}
