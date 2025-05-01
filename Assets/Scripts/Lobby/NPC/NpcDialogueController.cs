using System.Collections;
using TMPro;
using UnityEngine;

public class NpcDialogueController : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private float _delay = 3.0f;

    private Coroutine _hideCoroutine;

    private void Awake()
    {
        _dialogueUI.SetActive(false);
    }

    public void ShowText(string text)
    {
        _dialogueText.text = text;
        _dialogueUI.SetActive(true);

        // 기존에 실행 중인 코루틴이 있으면 중단
        if (_hideCoroutine != null)
        {
            StopCoroutine(_hideCoroutine);
        }

        // 새로운 코루틴 시작
        _hideCoroutine = StartCoroutine(HideAfterDelay(_delay));
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _dialogueUI.SetActive(false);
        _hideCoroutine = null; // 코루틴 종료됨을 기록
    }
}
