using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LogueGameScene : MonoBehaviour
{
    public static LogueGameScene Instance;

    RoundManager _roundManager;
    private int _roundCount = 1;
    [SerializeField] protected Define.RogueState _state = Define.RogueState.None;

    public GameObject CloseDoorMap;
    public GameObject OpenDoorMap;

    [SerializeField] private GameObject[] _resultArr;
    [SerializeField] private Transform _resultPivot;

    GameObject _result;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        _roundManager = GetComponentInChildren<RoundManager>();
    }

    public virtual Define.RogueState State
    {
        get { return _state; }
        set
        {
            _state = value;
            switch (_state)
            {
                case Define.RogueState.Start:
                    UpdateStart();
                    break;
                case Define.RogueState.Result:
                    UpdateResult();
                    break;
                case Define.RogueState.End:
                    UpdateEnd();
                    break;
            }
        }
    }
    public void StartLogueGame()
    {
        _roundCount = 1;
        State = Define.RogueState.Start;
    }

    private void Update()
    {
        switch (State)
        {
            case Define.RogueState.InRound:
                UpdateInRound();
                break;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerPrefs.SetInt("RogueBest", _roundCount);
        }
    }
    private void UpdateStart()
    {
        if (_result != null)
            Destroy(_result);
        CloseDoorMap.SetActive(true);
        OpenDoorMap.SetActive(false);

        _roundManager.StartWave(_roundCount);
        State = Define.RogueState.InRound;
    }
    private void UpdateInRound()
    {
        if(_roundManager.CheckRoundEnd())
        {
            State = Define.RogueState.Result;
        }
    }
    private void UpdateResult()
    {
        CloseDoorMap.SetActive(false);
        OpenDoorMap.SetActive(true);

        _result = Instantiate(_resultArr[Random.Range(0, _resultArr.Length)]);
        _result.transform.position = _resultPivot.transform.position;
        _roundCount++;
    }
    private void UpdateEnd()
    {
        if (PlayerPrefs.GetInt("RogueBest", 0) < _roundCount)
        {
            PlayerPrefs.SetInt("RogueBest", _roundCount);
            LobbyScene.Instance.ShowRougeResult = 1;
        }
        else
        {
            LobbyScene.Instance.ShowRougeResult = 0;
        }

        _roundCount = 0;
        LobbyScene.Instance.UpdateScore();
        _roundManager.EndGame();
    }


}
