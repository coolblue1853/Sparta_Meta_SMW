using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour, ITimeTriggerable
{
    [SerializeField]
    private string _targetScene;
    [SerializeField]
    private float _waitTime = 1.0f;
    [SerializeField]
    private Transform _pivot;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RoomController controller = collision.GetComponent<RoomController>();
            controller.StartTriggerCountdown(this,_waitTime, _pivot.position);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RoomController controller = collision.GetComponent<RoomController>();
            controller.CancelTriggerCountdown();
        }
    }

    // 콜백으로 호출되는 메서드
    public void OnTriggerTimeComplete()
    {
        SceneManager.LoadScene(_targetScene);
    }
}
