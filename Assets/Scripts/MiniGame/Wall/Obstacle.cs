using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    public float HighPosY = 1f;
    public float LowPosY = -1f;

    public float HoleSizeMin = 1f;
    public float HoleSizeMax = 3f;

    public Transform TopObject;
    public Transform BottomObject;

    public float WidthPadding = 4f;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(HoleSizeMin, HoleSizeMax);
        float halfHoleSize = holeSize / 2f;
        TopObject.localPosition = new Vector3(0, halfHoleSize);
        BottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(WidthPadding, 0);
        placePosition.y = Random.Range(LowPosY, HighPosY);

        transform.position = placePosition;

        return placePosition;
    }

}