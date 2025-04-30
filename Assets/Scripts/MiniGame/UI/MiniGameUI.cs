using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameUI : MonoBehaviour
{

    public static MiniGameUI instance;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        MiniGameScene.instance._OnScoreChanged += UpdateScoreText;

        // 초기 점수 표시
        UpdateScoreText(MiniGameScene.instance.GetScore());
    }

    private void UpdateScoreText(int newScore)
    {
        _scoreText.text = newScore.ToString();
    }


    [SerializeField]
    private GameObject StartUI;

    public void SetStartUI(bool isActive)
    {
        StartUI.SetActive(isActive);
    }

    [SerializeField]
    private GameObject InGameUI;

    public void SetInGameUI(bool isActive)
    {
        InGameUI.SetActive(isActive);
    }

    [SerializeField]
    private GameObject EndGameUI;
    [SerializeField]
    private TextMeshProUGUI EndScoreTxt;

    public void SetEndGameUI(bool isActive, int nowScore, int bestScore)
    {
        EndGameUI.SetActive(isActive);
        EndScoreTxt.text = $"최고 점수 : {bestScore}\n현재 점수 : {nowScore}";
    }
}
