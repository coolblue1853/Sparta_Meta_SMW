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

        // ������ ���� ���� �ڷ�ƾ�� ������ �ߴ�
        if (_hideCoroutine != null)
        {
            StopCoroutine(_hideCoroutine);
        }

        // ���ο� �ڷ�ƾ ����
        _hideCoroutine = StartCoroutine(HideAfterDelay(_delay));
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _dialogueUI.SetActive(false);
        _hideCoroutine = null; // �ڷ�ƾ ������� ���
    }
}
