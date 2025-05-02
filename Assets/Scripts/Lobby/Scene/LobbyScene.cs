using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    public static LobbyScene Instance;
    public LogueGameScene LogueGameScene;
    // 추후 프리팹으로 된 플레이어 생성
    public PlayerController PlayerPrefab;
    // 점수를 갱신하는 옵저버
    public static event Action OnScoreUpdate;
    public static event Action MoveSceneSave;
    public static event Action EndGameSave;
    public int ShowResult;
    public int ShowRougeResult;

    public GameObject BaseMap;
    [SerializeField] private GameObject _originPivot;

    public WeaponHandler[] WeaponArr;

    private void Awake()
    {
        PlayerPrefab.CreatWeapon(WeaponArr[0]);
        ShowRougeResult = -1;
        if (Instance == null)
        {
            Instance = this;
        }
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
            ShowResult = result;
        }
        else
        {
            ShowResult = -1;
        }
        PlayerPrefs.SetString("PrevScene", SceneManager.GetActiveScene().name);

    }
    private void LoadData()
    {
        SetPositon("PlayerPosition", PlayerPrefab.transform);
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
        PlayerPrefab.CreatWeapon(WeaponArr[1]);
        Camera.main.transform.position = transform.position;
        PlayerPrefab.transform.position = transform.position;
        LogueGameScene.StartLogueGame();
    }


    public void EndLogueGame()
    {
        PlayerPrefab.CreatWeapon(WeaponArr[0]);
        CameraManager cameraManager = Camera.main.GetComponent<CameraManager>();
        cameraManager.ChangePivot(BaseMap);
        Camera.main.transform.position = _originPivot.transform.position;
        PlayerPrefab.transform.position = _originPivot.transform.position;
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
