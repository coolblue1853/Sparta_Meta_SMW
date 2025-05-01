using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    public static LobbyScene Instance;
    // ���� ���������� �� �÷��̾� ����
    public GameObject _playerPrefab;
    // ������ �����ϴ� ������
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

        //���� ���� NPC�� ����, ���� UI�� �����־�� �ϴ°�
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

    // ������ ������ �ߵ��ؾ� �ϴ� �Լ�

    // ���� �ε�ɶ� �ߵ��ؾ� �ϴ� �Լ�


    //�׽�Ʈ��
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
