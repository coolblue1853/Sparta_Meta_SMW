using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LogueGameScene : MonoBehaviour
{
    public static LogueGameScene instance;

    RoundManager _roundManager;
    private int roundCount = 1;
    [SerializeField]
    protected Define.RogueState _state = Define.RogueState.None;

    public GameObject _closeDoorMap;
    public GameObject _openDoorMap;

    [SerializeField] private GameObject[] _resultArr;
    [SerializeField] private Transform _resultPivot;

    GameObject result;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
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
        roundCount = 1;
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
            PlayerPrefs.SetInt("RogueBest", roundCount);
        }
    }

 
    private void UpdateStart()
    {
        if (result != null)
            Destroy(result);
        _closeDoorMap.SetActive(true);
        _openDoorMap.SetActive(false);

        _roundManager.StartWave(roundCount);
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
        _closeDoorMap.SetActive(false);
        _openDoorMap.SetActive(true);

        result = Instantiate(_resultArr[Random.Range(0, _resultArr.Length)]);
        result.transform.position = _resultPivot.transform.position;
        roundCount++;
    }
    private void UpdateEnd()
    {
        if (PlayerPrefs.GetInt("RogueBest", 0) < roundCount)
        {
            PlayerPrefs.SetInt("RogueBest", roundCount);
            LobbyScene.Instance._showRougeResult = 1;
        }
        else
        {
            LobbyScene.Instance._showRougeResult = 0;
        }

        roundCount = 0;
        LobbyScene.Instance.UpdateScore();
        _roundManager.EndGame();
    }


}
