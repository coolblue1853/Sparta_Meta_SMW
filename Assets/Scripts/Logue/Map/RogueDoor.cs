using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueDoor : MonoBehaviour, ITimeTriggerable
{

    [SerializeField] private float _waitTime = 1.0f;
    [SerializeField] private Transform _pivot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RoomController controller = collision.GetComponent<RoomController>();
            controller.StartTriggerCountdown(this, _waitTime, _pivot.position);
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

    public void OnTriggerTimeComplete()
    {
        Camera.main.transform.position = _pivot.transform.position;
        LobbyScene.Instance.PlayerPrefab.transform.position = _pivot.transform.position;
        LogueGameScene.Instance.State = Define.RogueState.Start;
    }
}
