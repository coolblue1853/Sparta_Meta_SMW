using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float lerpSpeed = 5f;
    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private GameObject cameraPivot;
    private float  minX = 0;
    private float  maxX = 0;
    private float  minY = 0;
    private float  maxY = 0;


    private void Awake()
    {
        // ī�޶� ��� ���� �Լ�, ���� ������ �Լ��� ��������
        // �ʷε� ���� �ǹ� ����
        if(cameraPivot != null)
        {
            GameObject leftUpPivot = cameraPivot.transform.GetChild(0).gameObject;
            GameObject rightDownPivot = cameraPivot.transform.GetChild(1).gameObject;

            float camHalfHeight = Camera.main.orthographicSize;
            float camHalfWidth = camHalfHeight * ((float)Screen.width / Screen.height);


            minX = leftUpPivot.transform.position.x + camHalfWidth;
            maxX = rightDownPivot.transform.position.x - camHalfWidth;
            minY = rightDownPivot.transform.position.y + camHalfHeight;
            maxY = leftUpPivot.transform.position.y - camHalfHeight;
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 destinationPosition = target.position + offset;
        Vector3 lerpPosition = Vector3.Lerp(transform.position, destinationPosition,
            lerpSpeed * Time.deltaTime);

        lerpPosition.x = Mathf.Clamp(lerpPosition.x, minX, maxX);
        lerpPosition.y = Mathf.Clamp(lerpPosition.y, minY, maxY);

        transform.position = lerpPosition;
    }
}
