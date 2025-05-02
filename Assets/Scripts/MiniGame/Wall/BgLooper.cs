using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BgLooper : MonoBehaviour
{
    public int NumBgCount = 5;

    public int ObstacleCount = 0;
    public Vector3 ObstacleLastPosition = Vector3.zero;

    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        ObstacleLastPosition = obstacles[0].transform.position;
        ObstacleCount = obstacles.Length;

        for (int i = 0; i < ObstacleCount; i++)
        {
            ObstacleLastPosition = obstacles[i].SetRandomPlace(ObstacleLastPosition, ObstacleCount);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).bounds.size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * NumBgCount;
            collision.transform.position = pos;
            return;
        }

        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            ObstacleLastPosition = obstacle.SetRandomPlace(ObstacleLastPosition, ObstacleCount);
        }
    }
}