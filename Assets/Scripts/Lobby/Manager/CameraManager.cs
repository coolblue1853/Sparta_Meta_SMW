using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _lerpSpeed = 5f;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private GameObject _cameraPivot;
    private float  _minX = 0;
    private float  _maxX = 0;
    private float  _minY = 0;
    private float  _maxY = 0;

    public bool IsFixedY = false;

    private void Awake()
    {
        ChangePivot();
    }
    public void ChangePivot(GameObject targetPivot = null)
    {
        if(targetPivot == null)
        {
            _cameraPivot = LobbyScene.Instance.BaseMap;
        }
        else
        {
            _cameraPivot = targetPivot;
        }


        // ī�޶� ��� ���� �Լ�, ���� ������ �Լ��� ��������
        // �ʷε� ���� �ǹ� ����
        if (_cameraPivot != null)
        {
            GameObject leftUpPivot = _cameraPivot.transform.GetChild(0).gameObject;
            GameObject rightDownPivot = _cameraPivot.transform.GetChild(1).gameObject;

            float camHalfHeight = Camera.main.orthographicSize;
            float camHalfWidth = camHalfHeight * ((float)Screen.width / Screen.height);


            _minX = leftUpPivot.transform.position.x + camHalfWidth;
            _maxX = rightDownPivot.transform.position.x - camHalfWidth;
            _minY = rightDownPivot.transform.position.y + camHalfHeight;
            _maxY = leftUpPivot.transform.position.y - camHalfHeight;
        }
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 destinationPosition = _target.position + _offset;
        Vector3 lerpPosition = Vector3.Lerp(transform.position, destinationPosition,
            _lerpSpeed * Time.deltaTime);

        if (_cameraPivot != null)
        {
            lerpPosition.x = Mathf.Clamp(lerpPosition.x, _minX, _maxX);
            lerpPosition.y = Mathf.Clamp(lerpPosition.y, _minY, _maxY);
        }

        if(!IsFixedY)
            transform.position = lerpPosition;
        else
            transform.position = new Vector3(lerpPosition.x, transform.position.y, transform.position.z);
    }
}
