using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : MonoBehaviour
{
    // ���� ���������� �� �÷��̾� ����
    public GameObject _playerPrefab;
    // ������ �����ϴ� ������
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
