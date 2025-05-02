using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{

    [SerializeField] private GameObject _barUi;
    [SerializeField] private Image _progressBar;
    private Coroutine _currentTriggerCoroutine;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void StartTriggerCountdown(ITimeTriggerable target, float delay, Vector3 pivot)
    {
        // 중복 방지
        if (_currentTriggerCoroutine != null)
        {
            StopCoroutine(_currentTriggerCoroutine);
        }

        _currentTriggerCoroutine = StartCoroutine(TriggerCoroutine(target, delay, pivot));
    }
    public void CancelTriggerCountdown()
    {
        if (_currentTriggerCoroutine != null)
        {
            StopCoroutine(_currentTriggerCoroutine);
            _currentTriggerCoroutine = null;

            // UI 초기화
            _progressBar.fillAmount = 0;
            _barUi.SetActive(false);
        }
    }
    private IEnumerator TriggerCoroutine(ITimeTriggerable target, float delay,Vector3 pivot)
    {
        _barUi.SetActive(true);
        float elapsed = 0f;

        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            _progressBar.fillAmount = elapsed / delay;
            yield return null;
        }

        _progressBar.fillAmount = 0;
        _barUi.SetActive(false);
        target.OnTriggerTimeComplete();

        Vector3 playerPositon = pivot;
        PlayerPrefs.SetString("PlayerPosition", $"{playerPositon.x},{playerPositon.y},{playerPositon.z}");
        PlayerPrefs.SetString("CameraPosition", $"{_mainCamera.transform.position.x},{_mainCamera.transform.position.y},{_mainCamera.transform.position.z}");
    }
    private void OnApplicationQuit()
    {
       LobbyScene.Instance.EndLogueGame();
        Vector3 playerPositon = transform.position;
        PlayerPrefs.SetString("PlayerPosition", $"{playerPositon.x},{playerPositon.y},{playerPositon.z}");
        PlayerPrefs.SetString("CameraPosition", $"{_mainCamera.transform.position.x},{_mainCamera.transform.position.y},{_mainCamera.transform.position.z}");
    }

}
