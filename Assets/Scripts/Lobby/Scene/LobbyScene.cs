using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    public static LobbyScene Instance;
    public LogueGameScene LogueGameScene;
    // 추후 프리팹으로 된 플레이어 생성
    public PlayerController _playerPrefab;
    // 점수를 갱신하는 옵저버
    public static event Action OnScoreUpdate;
    public static event Action MoveSceneSave;
    public static event Action EndGameSave;
    public int _showResult;

    public GameObject baseMap;
    [SerializeField] private GameObject originPivot;

    public WeaponHandler[] weaponArr;

    private void Awake()
    {
        _playerPrefab.CreatWeapon(weaponArr[0]);
        if (Instance == null)
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

    public void StartLogueGame(Transform transform)
    {
        _playerPrefab.CreatWeapon(weaponArr[1]);
        Camera.main.transform.position = transform.position;
        _playerPrefab.transform.position = transform.position;
        LogueGameScene.StartLogueGame();
    }


    public void EndLogueGame()
    {
        _playerPrefab.CreatWeapon(weaponArr[0]);
        CameraManager cameraManager = Camera.main.GetComponent<CameraManager>();
        cameraManager.ChangePivot(baseMap);
        Camera.main.transform.position = originPivot.transform.position;
        _playerPrefab.transform.position = originPivot.transform.position;
        LogueGameScene.State = Define.RogueState.End;
    }

    //테스트용
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerPrefs.DeleteKey("HighScore");
        }
    }

}
