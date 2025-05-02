using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    public static LobbyScene Instance;
    public LogueGameScene LogueGameScene;
    // ���� ���������� �� �÷��̾� ����
    public PlayerController PlayerPrefab;
    // ������ �����ϴ� ������
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

        //���� ���� NPC�� ����, ���� UI�� �����־�� �ϴ°�
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

    //�׽�Ʈ��
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerPrefs.DeleteKey("HighScore");
        }
    }

}
