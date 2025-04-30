using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{

    [SerializeField]
    private GameObject barUi;
    [SerializeField]
    private Image progressBar;
     
    public void StartTriggerCountdown(ITimeTriggerable target, float delay)
    {
        StartCoroutine(TriggerCoroutine(target, delay));
    }
    private IEnumerator TriggerCoroutine(ITimeTriggerable target, float delay)
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
    }
}
