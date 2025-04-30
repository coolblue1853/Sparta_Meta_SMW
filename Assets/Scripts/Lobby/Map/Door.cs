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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RoomController controller = collision.GetComponent<RoomController>();
            controller.StartTriggerCountdown(this,_waitTime);

        }

       
    }
    // �ݹ����� ȣ��Ǵ� �޼���
    public void OnTriggerTimeComplete()
    {
        SceneManager.LoadScene(_targetScene);
    }
}
