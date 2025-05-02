using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{

    [SerializeField]
    private GameObject barUi;
    [SerializeField]
    private Image progressBar;
    private Coroutine currentTriggerCoroutine;

    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void StartTriggerCountdown(ITimeTriggerable target, float delay, Vector3 pivot)
    {
        // 중복 방지
        if (currentTriggerCoroutine != null)
        {
            StopCoroutine(currentTriggerCoroutine);
        }

        currentTriggerCoroutine = StartCoroutine(TriggerCoroutine(target, delay, pivot));
    }
    public void CancelTriggerCountdown()
    {
        if (currentTriggerCoroutine != null)
        {
            StopCoroutine(currentTriggerCoroutine);
            currentTriggerCoroutine = null;

            // UI 초기화
            progressBar.fillAmount = 0;
            barUi.SetActive(false);
        }
    }
    private IEnumerator TriggerCoroutine(ITimeTriggerable target, float delay,Vector3 pivot)
    {
        barUi.SetActive(true);
        float elapsed = 0f;

        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            progressBar.fillAmount = elapsed / delay;
            yield return null;
        }

        progressBar.fillAmount = 0;
        barUi.SetActive(false);
        target.OnTriggerTimeComplete();

        Vector3 playerPositon = pivot;
        PlayerPrefs.SetString("PlayerPosition", $"{playerPositon.x},{playerPositon.y},{playerPositon.z}");
        PlayerPrefs.SetString("CameraPosition", $"{mainCamera.transform.position.x},{mainCamera.transform.position.y},{mainCamera.transform.position.z}");
    }
    private void OnApplicationQuit()
    {
       LobbyScene.Instance.EndLogueGame();
        Vector3 playerPositon = transform.position;
        PlayerPrefs.SetString("PlayerPosition", $"{playerPositon.x},{playerPositon.y},{playerPositon.z}");
        PlayerPrefs.SetString("CameraPosition", $"{mainCamera.transform.position.x},{mainCamera.transform.position.y},{mainCamera.transform.position.z}");
    }

}
