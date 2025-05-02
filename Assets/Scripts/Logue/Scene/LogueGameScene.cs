using System.Collections;
using System.Collections.Generic;
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
    // 게임 시작
    // 라운드 초기화
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
        _roundManager.EndGame();
    }



    // 게임 진행도중
    // 맵 문닫기 -> 맵 라운드 소환, -> 다 잡으면 보상을 주고 방이 열림 -> 방에서 이동하면 다음 라운드 시작.


    // 게임 끝
    //스탯 초기화
}
