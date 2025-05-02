using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Range(1, 100)][SerializeField] private int _health = 10;
    public int Health
    {
        get => _health;
        set => _health = Mathf.Clamp(value, 0, 100);
    }

    [Range(1f, 20f)][SerializeField] private float _speed = 3;
    public float Speed
    {
        get => _speed;
        set => _speed = Mathf.Clamp(value, 0, 20);
    }
}