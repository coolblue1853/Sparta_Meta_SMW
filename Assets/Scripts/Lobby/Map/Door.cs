using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour, ITimeTriggerable
{
    [SerializeField] private string _targetScene;
    [SerializeField] private float _waitTime = 1.0f;
    [SerializeField] private Transform _pivot;

    //���� ������ �̵��Ҷ�
    public bool IsMoveInSameScene =false;
    [SerializeField] private Transform _targetPositon;
    [SerializeField] private GameObject _targetPivot;
    private CameraManager _camera;

    private void Awake()
    {
        _camera = Camera.main.GetComponent<CameraManager>();
    }
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

    // �ݹ����� ȣ��Ǵ� �޼���
    public  void OnTriggerTimeComplete()
    {
        if(!IsMoveInSameScene)
            SceneManager.LoadScene(_targetScene);
        else
        {
            _camera.ChangePivot(_targetPivot);
            LobbyScene.Instance.StartLogueGame(_targetPositon);
        }
    }
}
