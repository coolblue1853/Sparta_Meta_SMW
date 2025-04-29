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

    private void Awake()
    {
        Screen.SetResolution(1280, 720, false); // 짝수 해상도 사용
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 destinationPosition = target.position + offset;
        Vector3 lerpPosition = Vector3.Lerp(transform.position, destinationPosition,
            lerpSpeed * Time.deltaTime);

        transform.position = lerpPosition;
    }
}
